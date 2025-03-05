using InventoryManagement.Application.DTOs;
using InventoryManagement.Application.Interfaces;
using InventoryManagement.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.API.Controllers
{
    [Authorize]
    [Route("products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("movement")]
        public async Task<IActionResult> RegisterMovement([FromBody] ProductMovementDto movementDto)
        {
            var userId = User.Identity?.Name;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { Message = "User not authenticated" });
            }

            await _productService.RegisterMovementAsync(movementDto, userId);
            return Ok(new { Message = "Inventory movement registered" });
        }


        [HttpGet("inventory")]
        public async Task<IActionResult> GetInventory()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }
    }
}
