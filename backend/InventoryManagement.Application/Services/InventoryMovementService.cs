using InventoryManagement.Application.Interfaces;
using InventoryManagement.Application.Validators;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Enums;
using InventoryManagement.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Application.Services
{
    public class InventoryMovementService : IInventoryMovementService
    {
        private readonly IInventoryMovementRepository _movementRepository;
        private readonly IProductRepository _productRepository;

        public InventoryMovementService(IInventoryMovementRepository movementRepository, IProductRepository productRepository)
        {
            _movementRepository = movementRepository;
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<InventoryMovement>> GetMovementsAsync(
            int? productId, string? productName, string? startDate, string? endDate, string? type)
        {
            // Validar fechas y convertirlas a DateTime
            var (parsedStartDate, parsedEndDate) = InventoryMovementFilterValidator.ValidateDates(startDate, endDate);

            // Validar el tipo de movimiento
            var movementType = InventoryMovementFilterValidator.ValidateType(type);

            var query = _movementRepository.GetAll();

            if (productId.HasValue)
            {
                query = query.Where(m => m.ProductId == productId.Value);
            }

            if (!string.IsNullOrWhiteSpace(productName))
            {
                var matchingProducts = (await _productRepository.GetAllAsync())
                    .Where(p => p.Name.Contains(productName, StringComparison.CurrentCultureIgnoreCase))
                    .Select(p => p.Id);

                query = query.Where(m => matchingProducts.Contains(m.ProductId));
            }

            if (parsedStartDate.HasValue)
            {
                query = query.Where(m => m.Date.Date >= parsedStartDate.Value);
            }

            if (parsedEndDate.HasValue)
            {
                query = query.Where(m => m.Date.Date <= parsedEndDate.Value);
            }

            if (movementType.HasValue)
            {
                query = movementType switch
                {
                    InventoryMovementType.In => query.Where(m => m.Quantity > 0),
                    InventoryMovementType.Out => query.Where(m => m.Quantity < 0),
                    _ => query
                };
            }

            return await query.ToListAsync();
        }
    }
}
