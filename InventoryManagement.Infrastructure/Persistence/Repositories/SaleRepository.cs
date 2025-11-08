using Microsoft.EntityFrameworkCore;
using InventoryManagement.Application.Interfaces;
using InventoryManagement.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryManagement.Infrastructure.Persistence.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly AppDbContext _context;

        public SaleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Sale>> GetAllAsync()
        {
            return await _context.Sales.ToListAsync();
        }

        public async Task<Sale> GetByIdAsync(int id)
        {
            return await _context.Sales.FindAsync(id);
        }

        public async Task AddAsync(Sale entity)
        {
            await _context.Sales.AddAsync(entity);
        }

        public void Update(Sale entity)
        {
            _context.Sales.Update(entity);
        }

        public void Delete(Sale entity)
        {
            _context.Sales.Remove(entity);
        }

        // Métodos específicos
        public async Task<Sale> GetSaleWithDetailsAsync(int id)
        {
            return await _context.Sales
                                 .Include(s => s.SaleDetails)
                                 .ThenInclude(d => d.Product)
                                 .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Sale>> GetSalesByCustomerAsync(int customerId)
        {
            return await _context.Sales
                                 .Where(s => s.CustomerId == customerId)
                                 .ToListAsync();
        }
    }
}
