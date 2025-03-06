using InventoryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace InventoryManagement.Infrastructure.Data
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<InventoryMovement> InventoryMovements { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<InventoryMovement>().ToTable("InventoryMovements")
                    .Property(m => m.UserId)
                    .IsRequired();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var auditEntries = new List<AuditLog>();

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is AuditLog || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;

                var audit = new AuditLog
                {
                    TableName = entry.Entity.GetType().Name,
                    Action = entry.State.ToString(),
                    Timestamp = DateTime.UtcNow
                };

                if (entry.State == EntityState.Added)
                {
                    audit.NewValues = JsonSerializer.Serialize(entry.CurrentValues.ToObject());
                }
                else if (entry.State == EntityState.Modified)
                {
                    audit.OldValues = JsonSerializer.Serialize(entry.OriginalValues.ToObject());
                    audit.NewValues = JsonSerializer.Serialize(entry.CurrentValues.ToObject());
                }
                else if (entry.State == EntityState.Deleted)
                {
                    audit.OldValues = JsonSerializer.Serialize(entry.OriginalValues.ToObject());
                }

                auditEntries.Add(audit);
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            if (auditEntries.Count > 0)
            {
                AuditLogs.AddRange(auditEntries);
                await base.SaveChangesAsync(cancellationToken);
            }

            return result;
        }

    }
}
