using InventoryManagement.Application.DTOs;
using InventoryManagement.Application.Interfaces;
using InventoryManagement.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UsersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get all active users.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserListDto>>> GetAllAsync()
        {
            var users = await _unitOfWork.Users.GetAllAsync();

            var result = users.Select(c => new UserListDto
            {
                Id = c.Id,
                Identification = c.Identification ?? string.Empty,
                Name = c.Name ?? string.Empty,
                Email = c.Email ?? string.Empty,
                IsActive = c.IsActive
            });

            return Ok(result);
        }

        /// <summary>
        /// Get a user by ID.
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserListDto>> GetByIdAsync(int id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);

            if (user == null)
                return NotFound($"User with ID {id} not found.");

            var dto = new UserListDto
            {
                Id = user.Id,
                Identification = user.Identification ?? string.Empty,
                Name = user.Name ?? string.Empty,
                Email = user.Email ?? string.Empty,
                IsActive = user.IsActive
            };

            return Ok(dto);
        }

        /// <summary>
        /// Create a new user.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<UserListDto>> CreateAsync([FromBody] UserCreateDto dto)
        {
            var passwordHasher = new PasswordHasher<User>();

            var user = new User
            {
                Identification = dto.Identification,
                Name = dto.Name,
                Email = dto.Email,
                IsActive = dto.IsActive ?? true,
                CreatedBy = dto.CreatedBy,
                CreatedAt = DateTime.UtcNow,
            };

            user.Password = passwordHasher.HashPassword(user, dto.Password);

            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            var result = new UserListDto
            {
                Id = user.Id,
                Identification = user.Identification,
                Name = user.Name,
                Email = user.Email,
                IsActive = user.IsActive
            };

            return Ok(result);
        }

        /// <summary>
        /// Update an existing user.
        /// </summary>
        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateAsync(int id, [FromBody] UserUpdateDto dto)
        {
            if (id != dto.Id)
                return BadRequest("User ID mismatch.");

            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user == null)
                return NotFound($"User with ID {id} not found.");

            // Manual mapping (only update provided fields)
            if (dto.Identification != null) user.Identification = dto.Identification;
            if (dto.Name != null) user.Name = dto.Name;
            if (dto.Email != null) user.Email = dto.Email;
            if (dto.Password != null)
            {
                var passwordHasher = new PasswordHasher<User>();
                user.Password = passwordHasher.HashPassword(user, dto.Password);
            }
            if (dto.IsActive.HasValue) user.IsActive = dto.IsActive.Value;
            if (dto.UpdatedBy != null) user.UpdatedBy = dto.UpdatedBy;

            user.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.Users.Update(user);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Deactivate (soft delete) a user.
        /// </summary>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user == null)
                return NotFound($"User with ID {id} not found.");

            user.IsActive = false;
            user.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.Users.Update(user);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }
}
