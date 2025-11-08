namespace InventoryManagement.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerRepository Customers { get; }

        IProductRepository Products { get; }

        ISaleRepository Sales { get; }

        IUserRepository Users { get; }

        Task<int> SaveChangesAsync();
    }
}
