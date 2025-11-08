using InventoryManagement.Domain.Entities;

namespace InventoryManagement.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);

        Task<User?> GetUserByEmailAsync(string email);
        Task AddAsync(User entity);
        void Update(User entity);
        void Delete(User entity);
    }
}
