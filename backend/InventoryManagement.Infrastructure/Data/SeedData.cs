using InventoryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryManagement.Infrastructure.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using var context = serviceProvider.GetRequiredService<InventoryDbContext>();

            // Aplicar migraciones automáticamente si no se han aplicado
            await context.Database.MigrateAsync();

            // Verificar si ya hay productos en la base de datos
            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product { Name = "Laptop", Quantity = 15 },
                    new Product { Name = "Mouse", Quantity = 50 },
                    new Product { Name = "Teclado", Quantity = 30 },
                    new Product { Name = "Monitor", Quantity = 20 },
                    new Product { Name = "Impresora", Quantity = 10 }
                );

                await context.SaveChangesAsync();
            }
        }
    }
}
