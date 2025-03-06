using InventoryManagement.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.API.Controllers
{
    [Route("movements")]
    [ApiController]
    [Authorize]
    public class InventoryMovementController : ControllerBase
    {
        private readonly IInventoryMovementService _movementService;

        public InventoryMovementController(IInventoryMovementService movementService)
        {
            _movementService = movementService;
        }

        /// <summary>
        /// Obtiene la lista de movimientos de inventario con filtros opcionales.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetMovements(
            [FromQuery] int? productId,
            [FromQuery] string? productName,
            [FromQuery] string? startDate,
            [FromQuery] string? endDate,
            [FromQuery] string? type)
        {
            var movements = await _movementService.GetMovementsAsync(productId, productName, startDate, endDate, type);
            return Ok(movements);
        }
    }
}
