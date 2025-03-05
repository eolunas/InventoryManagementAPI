using InventoryManagement.Application.DTOs;
using InventoryManagement.Application.Interfaces;
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

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _productRepository.GetByIdAsync(id);
        }

        public async Task RegisterMovementAsync(ProductMovementDto movementDto, string userId)
        {
            var movement = new InventoryMovement
            {
                ProductId = movementDto.ProductId,
                Quantity = movementDto.Quantity,
                Type = movementDto.Quantity > 0 ? "IN" : "OUT",
                UserId = userId
            };

            await _movementRepository.AddAsync(movement);
        }

        public async Task AddProductAsync(ProductDto productDto)
        {
            var product = new Product
            {
                Name = productDto.Name,
                Quantity = productDto.Quantity
            };
            await _productRepository.AddAsync(product);
        }

        public async Task UpdateProductAsync(int id, ProductDto productDto)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return;

            product.Name = productDto.Name;
            product.Quantity = productDto.Quantity;

            await _productRepository.UpdateAsync(product);
        }

        public async Task DeleteProductAsync(int id)
        {
            await _productRepository.DeleteAsync(id);
        }
    }
}
