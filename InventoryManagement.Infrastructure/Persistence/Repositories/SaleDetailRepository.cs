using Microsoft.EntityFrameworkCore;
using InventoryManagement.Application.Interfaces;
using InventoryManagement.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace InventoryManagement.Infrastructure.Persistence.Repositories
{
    public class SaleDetailRepository : ISaleDetailRepository
    {
        private readonly AppDbContext _context;

        public SaleDetailRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SaleDetail>> GetAllAsync()
        {
            return await _context.SaleDetails.ToListAsync();
        }

        public async Task<SaleDetail> GetByIdAsync(int id)
        {
            return await _context.SaleDetails.FindAsync(id);
        }

        public async Task AddAsync(SaleDetail entity)
        {
            await _context.SaleDetails.AddAsync(entity);
        }

        public void Update(SaleDetail entity)
        {
            _context.SaleDetails.Update(entity);
        }

        public void Delete(SaleDetail entity)
        {
            _context.SaleDetails.Remove(entity);
        }

        // Método específico
        public async Task<IEnumerable<SaleDetail>> GetDetailsBySaleIdAsync(int saleId)
        {
            return await _context.SaleDetails
                                 .Where(d => d.SaleId == saleId)
                                 .Include(d => d.Product)
                                 .ToListAsync();
        }
    }
}
