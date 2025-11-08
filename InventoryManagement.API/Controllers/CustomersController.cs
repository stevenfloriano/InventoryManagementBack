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
    public class CustomersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get all active customers.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerListDto>>> GetAllAsync()
        {
            var customers = await _unitOfWork.Customers.GetAllAsync();

            var result = customers.Select(c => new CustomerListDto
            {
                Id = c.Id,
                Identification = c.Identification ?? string.Empty,
                Name = c.Name ?? string.Empty,
                LastName = c.LastName ?? string.Empty,
                Phone = c.Phone ?? string.Empty,
                Email = c.Email ?? string.Empty,
                Address = c.Address ?? string.Empty,
                IsActive = c.IsActive
            });

            return Ok(result);
        }

        /// <summary>
        /// Get a customer by ID.
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CustomerListDto>> GetByIdAsync(int id)
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync(id);

            if (customer == null)
                return NotFound($"Customer with ID {id} not found.");

            var dto = new CustomerListDto
            {
                Id = customer.Id,
                Identification = customer.Identification ?? string.Empty,
                Name = customer.Name ?? string.Empty,
                LastName = customer.LastName ?? string.Empty,
                Phone = customer.Phone ?? string.Empty,
                Email = customer.Email ?? string.Empty,
                Address = customer.Address ?? string.Empty,
                IsActive = customer.IsActive
            };

            return Ok(dto);
        }

        /// <summary>
        /// Create a new customer.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<CustomerListDto>> CreateAsync([FromBody] CustomerCreateDto dto)
        {
            var customer = new Customer
            {
                Identification = dto.Identification,
                Name = dto.Name,
                LastName = dto.LastName,
                Phone = dto.Phone,
                Email = dto.Email,
                Address = dto.Address,
                IsActive = dto.IsActive ?? true,
                CreatedBy = dto.CreatedBy,
                CreatedAt = DateTime.UtcNow,
            };

            await _unitOfWork.Customers.AddAsync(customer);
            await _unitOfWork.SaveChangesAsync();

            var result = new CustomerListDto
            {
                Id = customer.Id,
                Identification = customer.Identification,
                Name = customer.Name,
                LastName = customer.LastName,
                Phone = customer.Phone,
                Email = customer.Email,
                Address = customer.Address,
                IsActive = customer.IsActive
            };

            return Ok(result);
        }

        /// <summary>
        /// Update an existing customer.
        /// </summary>
        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateAsync(int id, [FromBody] CustomerUpdateDto dto)
        {
            if (id != dto.Id)
                return BadRequest("Customer ID mismatch.");

            var customer = await _unitOfWork.Customers.GetByIdAsync(id);
            if (customer == null)
                return NotFound($"Customer with ID {id} not found.");

            // Manual mapping (only update provided fields)
            if (dto.Identification != null) customer.Identification = dto.Identification;
            if (dto.Name != null) customer.Name = dto.Name;
            if (dto.LastName != null) customer.LastName = dto.LastName;
            if (dto.Phone != null) customer.Phone = dto.Phone;
            if (dto.Email != null) customer.Email = dto.Email;
            if (dto.Address != null) customer.Address = dto.Address;
            if (dto.IsActive.HasValue) customer.IsActive = dto.IsActive.Value;
            if (dto.UpdatedBy != null) customer.UpdatedBy = dto.UpdatedBy;

            customer.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.Customers.Update(customer);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Deactivate (soft delete) a customer.
        /// </summary>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync(id);
            if (customer == null)
                return NotFound($"Customer with ID {id} not found.");

            customer.IsActive = false;
            customer.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.Customers.Update(customer);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }
}
