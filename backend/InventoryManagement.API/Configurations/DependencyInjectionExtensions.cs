using InventoryManagement.Application.Interfaces;
using InventoryManagement.Application.Services;
using InventoryManagement.Domain.Interfaces;
using InventoryManagement.Infrastructure.Repositories;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {

        // Aplication services:
        services.AddScoped<IProductService, ProductService>();

        // Infraestructure repositories:
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IInventoryMovementRepository, InventoryMovementRepository>();

        return services;
    }
}
