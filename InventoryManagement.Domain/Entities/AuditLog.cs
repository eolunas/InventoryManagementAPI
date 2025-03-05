namespace InventoryManagement.Domain.Entities
{
    public class AuditLog
    {
        public int Id { get; set; }
        public string TableName { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string? OldValues { get; set; }
        public string? NewValues { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
