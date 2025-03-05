﻿namespace InventoryManagement.Application.DTOs
{
    public class ProductMovementDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string UserId { get; set; } = string.Empty;
    }
}
