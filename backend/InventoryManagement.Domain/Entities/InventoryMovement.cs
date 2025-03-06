namespace InventoryManagement.Domain.Entities
{
    public class InventoryMovement
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public int Quantity { get; set; }
        public string Type { get; set; } = "IN";
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string UserId { get; set; } = string.Empty;
    }
}
