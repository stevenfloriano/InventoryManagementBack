using InventoryManagement.Domain.Entities;

namespace InventoryManagement.Application.Interfaces
{
    public interface ISaleRepository
    {
        Task<IEnumerable<Sale>> GetAllAsync();
        Task<Sale> GetByIdAsync(int id);
        Task AddAsync(Sale entity);
        void Update(Sale entity);
        void Delete(Sale entity);

        Task<Sale> GetSaleWithDetailsAsync(int id);
        Task<IEnumerable<Sale>> GetSalesByCustomerAsync(int customerId);
    }

    public interface ISaleDetailRepository
    {
        Task<IEnumerable<SaleDetail>> GetAllAsync();
        Task<SaleDetail> GetByIdAsync(int id);
        Task AddAsync(SaleDetail entity);
        void Update(SaleDetail entity);
        void Delete(SaleDetail entity);

        Task<IEnumerable<SaleDetail>> GetDetailsBySaleIdAsync(int saleId);
    }
}
