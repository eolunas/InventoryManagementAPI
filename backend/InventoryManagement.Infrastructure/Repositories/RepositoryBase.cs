using InventoryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryManagement.Infrastructure.Repositories
{
    public abstract class RepositoryBase<T> where T : class
    {
        protected readonly InventoryDbContext _dbContext;

        protected RepositoryBase(InventoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await _dbContext.Set<T>().ToListAsync();

        public async Task<T?> GetByIdAsync(int id) => await _dbContext.Set<T>().FindAsync(id);

        public async Task AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);
            if (entity != null)
            {
                _dbContext.Set<T>().Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
