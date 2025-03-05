using InventoryManagement.Application.DTOs;
using InventoryManagement.Domain.Entities;

namespace InventoryManagement.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task RegisterMovementAsync(ProductMovementDto movementDto, string userId);
    }
}
