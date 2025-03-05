using InventoryManagement.Application.DTOs;
using InventoryManagement.Application.Interfaces;
using InventoryManagement.Application.Validators;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Interfaces;

namespace InventoryManagement.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IInventoryMovementRepository _movementRepository;

        public ProductService(IProductRepository productRepository, IInventoryMovementRepository movementRepository)
        {
            _productRepository = productRepository;
            _movementRepository = movementRepository;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _productRepository.GetByIdAsync(id);
        }

        public async Task<Product> CreateAsync(ProductDto productDto)
        {
            if (string.IsNullOrWhiteSpace(productDto.Name))
                throw new ArgumentException("Product name cannot be empty.");

            var existingProduct = await _productRepository.GetByNameAsync(productDto.Name);
            if (existingProduct != null)
                throw new ArgumentException("A product with this name already exists.");

            if (productDto.Quantity < 0)
                throw new ArgumentException("Quantity cannot be negative.");

            var product = new Product { Name = productDto.Name, Quantity = productDto.Quantity };
            await _productRepository.AddAsync(product);
            return product;
        }

        public async Task<Product> UpdateAsync(int id, ProductDto productDto)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                throw new KeyNotFoundException("Product not found.");

            if (!string.IsNullOrWhiteSpace(productDto.Name))
            {
                var existingProduct = await _productRepository.GetByNameAsync(productDto.Name);
                if (existingProduct != null && existingProduct.Id != id)
                    throw new ArgumentException("A product with this name already exists.");

                product.Name = productDto.Name;
            }

            if (productDto.Quantity != product.Quantity)
                throw new InvalidOperationException("Quantity must be updated through inventory movements.");

            product.Quantity = productDto.Quantity;
            await _productRepository.UpdateAsync(product);
            return product;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                throw new KeyNotFoundException("Product not found.");

            var hasMovements = await _movementRepository.HasMovementsAsync(id);
            if (hasMovements)
                throw new InvalidOperationException("Cannot delete a product with movement history.");

            await _productRepository.DeleteAsync(id);
            return true;
        }

        public async Task RegisterMovementAsync(ProductMovementDto movementDto, string userId)
        {
            var product = await _productRepository.GetByIdAsync(movementDto.ProductId);

            ProductMovementValidator.Validate(movementDto, product);

            var movement = new InventoryMovement
            {
                ProductId = movementDto.ProductId,
                Quantity = movementDto.Quantity,
                Type = movementDto.Quantity > 0 ? "IN" : "OUT",
                UserId = userId
            };

            await _movementRepository.AddAsync(movement);
        }
    }
}
