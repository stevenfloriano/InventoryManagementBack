using InventoryManagement.Application.DTOs;
using InventoryManagement.Application.Interfaces;
using InventoryManagement.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get all active products.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductListDto>>> GetAllAsync()
        {
            var products = await _unitOfWork.Products.GetAllAsync();

            var result = products.Select(p => new ProductListDto
            {
                Id = p.Id,
                SKU = p.SKU ?? string.Empty,
                Name = p.Name ?? string.Empty,
                Description = p.Description ?? string.Empty,
                Price = p.Price,
                Stock = p.Stock,
                IsActive = p.IsActive
            });

            return Ok(result);
        }

        /// <summary>
        /// Get a product by ID.
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductListDto>> GetByIdAsync(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);

            if (product == null)
                return NotFound($"Product with ID {id} not found.");

            var dto = new ProductListDto
            {
                Id = product.Id,
                SKU = product.SKU ?? string.Empty,
                Name = product.Name ?? string.Empty,
                Description = product.Description ?? string.Empty,
                Price = product.Price,
                Stock = product.Stock,
                IsActive = product.IsActive
            };

            return Ok(dto);
        }

        /// <summary>
        /// Create a new product.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ProductListDto>> CreateAsync([FromBody] ProductCreateDto dto)
        {
            var product = new Product
            {
                SKU = dto.SKU,
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Stock = dto.Stock,
                IsActive = dto.IsActive ?? true,
                CreatedBy = dto.CreatedBy,
                CreatedAt = DateTime.UtcNow,
            };

            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();

            var result = new ProductListDto
            {
                Id = product.Id,
                SKU = product.SKU,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                IsActive = product.IsActive
            };

            return Ok(result);
        }

        /// <summary>
        /// Update an existing product.
        /// </summary>
        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateAsync(int id, [FromBody] ProductUpdateDto dto)
        {
            if (id != dto.Id)
                return BadRequest("Product ID mismatch.");

            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product == null)
                return NotFound($"Product with ID {id} not found.");

            // Manual mapping (only update provided fields)
            if (dto.SKU != null) product.SKU = dto.SKU;
            if (dto.Name != null) product.Name = dto.Name;
            if (dto.Description != null) product.Description = dto.Description;
            if (dto.Price.HasValue) product.Price = dto.Price.Value;
            if (dto.Stock.HasValue) product.Stock = dto.Stock.Value;
            if (dto.IsActive.HasValue) product.IsActive = dto.IsActive.Value;
            if (dto.UpdatedBy != null) product.UpdatedBy = dto.UpdatedBy;

            product.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.Products.Update(product);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Deactivate (soft delete) a product.
        /// </summary>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product == null)
                return NotFound($"Product with ID {id} not found.");

            product.IsActive = false;
            product.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.Products.Update(product);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }
}
