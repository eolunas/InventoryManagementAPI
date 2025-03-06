using InventoryManagement.Application.DTOs;
using InventoryManagement.Domain.Entities;

namespace InventoryManagement.Application.Validators
{
    public static class ProductMovementValidator
    {
        public static void Validate(ProductMovementDto movementDto, Product? product)
        {
            if (movementDto.Quantity == 0)
            {
                throw new ArgumentException("The quantity must be greater or less than zero.");
            }

            if (product == null)
            {
                throw new KeyNotFoundException("The product does not exist.");
            }

            if (movementDto.Quantity < 0 && Math.Abs(movementDto.Quantity) > product.Quantity)
            {
                throw new InvalidOperationException($"Not enough stock. Available: {product.Quantity}, Requested: {Math.Abs(movementDto.Quantity)}");
            }
        }
    }
}
