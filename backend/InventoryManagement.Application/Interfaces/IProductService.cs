using InventoryManagement.Application.DTOs;
using InventoryManagement.Domain.Entities;

namespace InventoryManagement.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task<Product> CreateAsync(ProductDto productDto);
        Task<Product> UpdateAsync(int id, ProductDto productDto);
        Task<bool> DeleteAsync(int id);
        Task RegisterMovementAsync(ProductMovementDto movementDto, string userId);
    }
}
