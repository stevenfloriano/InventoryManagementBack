using InventoryManagement.Domain.Entities;

namespace InventoryManagement.Application.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer> GetByIdAsync(int id);
        Task AddAsync(Customer entity);
        void Update(Customer entity);
        void Delete(Customer entity);
    }
}
