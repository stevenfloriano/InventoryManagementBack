using InventoryManagement.Application.Interfaces;
using InventoryManagement.Infrastructure.Persistence.Repositories;

namespace InventoryManagement.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public ICustomerRepository Customers { get; }
        public IProductRepository Products { get; }
        public ISaleRepository Sales { get; }
        public IUserRepository Users { get; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;

            Customers = new CustomerRepository(_context);
            Products = new ProductRepository(_context);
            Sales = new SaleRepository(_context);
            Users = new UserRepository(_context);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
