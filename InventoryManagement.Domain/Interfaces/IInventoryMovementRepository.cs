using InventoryManagement.Domain.Entities;

namespace InventoryManagement.Domain.Interfaces
{
    public interface IInventoryMovementRepository
    {
        Task<IEnumerable<InventoryMovement>> GetAllAsync();
        Task AddAsync(InventoryMovement movement);
    }
}
