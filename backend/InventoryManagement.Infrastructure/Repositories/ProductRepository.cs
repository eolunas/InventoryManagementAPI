using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Interfaces;
using InventoryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Infrastructure.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(InventoryDbContext dbContext) : base(dbContext) { }

        public async Task<Product?> GetByNameAsync(string name)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(p => p.Name.Trim().ToLower() == name.Trim().ToLower());
        }

        public async Task<bool> HasMovementsAsync(int productId)
        {
            return await _dbContext.InventoryMovements.AnyAsync(m => m.ProductId == productId);
        }
    }
}
