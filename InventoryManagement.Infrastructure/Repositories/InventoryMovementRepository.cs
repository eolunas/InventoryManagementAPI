using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Interfaces;
using InventoryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Infrastructure.Repositories
{
    public class InventoryMovementRepository : IInventoryMovementRepository
    {
        private readonly InventoryDbContext _dbContext;

        public InventoryMovementRepository(InventoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<InventoryMovement>> GetAllAsync()
        {
            return await _dbContext.InventoryMovements.Include(m => m.Product).ToListAsync();
        }

        public async Task AddAsync(InventoryMovement movement)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                _dbContext.InventoryMovements.Add(movement);

                var product = await _dbContext.Products.FindAsync(movement.ProductId);
                if (product != null)
                {
                    product.Quantity += movement.Quantity;
                }

                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
