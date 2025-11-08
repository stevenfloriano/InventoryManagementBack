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
    public class SalesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public SalesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get all sales.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SaleDTO>>> GetAllAsync()
        {
            var sales = await _unitOfWork.Sales.GetAllAsync();

            var result = sales.Select(s => new SaleDTO
            {
                Id = s.Id,
                CustomerId = s.CustomerId,
                CustomerName = s.Customer?.Name,
                Date = s.Date,
                Total = s.Total,
                Note = s.Note ?? string.Empty
            });

            return Ok(result);
        }

        /// <summary>
        /// Get a sale by ID including its details.
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<SaleDTO>> GetByIdAsync(int id)
        {
            var sale = await _unitOfWork.Sales.GetSaleWithDetailsAsync(id);
            if (sale == null)
                return NotFound($"Sale with ID {id} not found.");

            var saleDto = new SaleDTO
            {
                Id = sale.Id,
                CustomerId = sale.CustomerId,
                Date = sale.Date,
                Total = sale.Total,
                Note = sale.Note ?? string.Empty,
                SaleDetails = sale.SaleDetails.Select(d => new SaleDetailDTO
                {
                    Id = d.Id,
                    ProductId = d.ProductId,
                    //Product = d.Product?.Name,
                    Quantity = d.Quantity,
                    Value = d.Value
                }).ToList()
            };

            return Ok(saleDto);
        }

        /// <summary>
        /// Create a new sale with details.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<SaleDTO>> CreateAsync([FromBody] CreateSaleDTO dto)
        {
            var sale = new Sale
            {
                CustomerId = dto.CustomerId,
                Date = dto.Date,
                Note = dto.Note ?? string.Empty,
                SaleDetails = dto.SaleDetails.Select(d => new SaleDetail
                {
                    ProductId = d.ProductId,
                    Quantity = d.Quantity,
                    Value = d.Value
                }).ToList()
            };

            await _unitOfWork.Sales.AddAsync(sale);
            await _unitOfWork.SaveChangesAsync();

            var result = new SaleDTO
            {
                Id = sale.Id,
                CustomerId = sale.CustomerId,
                Date = sale.Date,
                Total = sale.Total,
                Note = sale.Note ?? string.Empty,
                SaleDetails = sale.SaleDetails.Select(d => new SaleDetailDTO
                {
                    Id = d.Id,
                    ProductId = d.ProductId,
                    //Product = d.Product?.Name,
                    Quantity = d.Quantity,
                    Value = d.Value
                }).ToList()
            };

            return Ok(result);
        }

        /// <summary>
        /// Update an existing sale.
        /// </summary>
        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateAsync(int id, [FromBody] UpdateSaleDTO dto)
        {
            if (id != dto.Id)
                return BadRequest("Sale ID mismatch.");

            var sale = await _unitOfWork.Sales.GetSaleWithDetailsAsync(id);
            if (sale == null)
                return NotFound($"Sale with ID {id} not found.");

            // Update main sale fields
            sale.CustomerId = dto.CustomerId;
            sale.Date = dto.Date;
            sale.Note = dto.Note ?? string.Empty;

            // Clear existing details and add new ones
            sale.SaleDetails.Clear();
            foreach (var detailDto in dto.SaleDetails)
            {
                sale.SaleDetails.Add(new SaleDetail
                {
                    ProductId = detailDto.ProductId,
                    Quantity = detailDto.Quantity,
                    Value = detailDto.Value
                });
            }

            _unitOfWork.Sales.Update(sale);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Delete a sale.
        /// </summary>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var sale = await _unitOfWork.Sales.GetByIdAsync(id);
            if (sale == null)
                return NotFound($"Sale with ID {id} not found.");

            _unitOfWork.Sales.Delete(sale);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }
}
