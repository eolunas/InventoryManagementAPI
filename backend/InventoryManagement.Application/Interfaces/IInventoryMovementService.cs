using InventoryManagement.Domain.Entities;

namespace InventoryManagement.Application.Interfaces
{
    public interface IInventoryMovementService
    {
        Task<IEnumerable<InventoryMovement>> GetMovementsAsync(
            int? productId, string? productName, string? startDate, string? endDate, string? type);
    }
}
