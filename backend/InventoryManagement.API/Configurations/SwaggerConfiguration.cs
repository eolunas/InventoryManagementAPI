using Microsoft.OpenApi.Models;

public static class SwaggerConfiguration
{
    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Inventory Management API",
                Version = "v1",
                Description = "This API allows users to manage inventory, track product movements, and authenticate via JWT tokens.\n\n" +
                      "**Features:**\n" +
                      "- User authentication with JWT\n" +
                      "- Register product movements (in/out stock)\n" +
                      "- Retrieve inventory status\n" +
                      "- Audit logs for data tracking\n" +
                      "- Supports multi-warehouse operations\n",
                Contact = new OpenApiContact
                {
                    Name = "Support Team",
                    Email = "eolunas@gmail.com",
                    Url = new Uri("https://eolunas.github.io/eolunas-dev/")
                },
                License = new OpenApiLicense
                {
                    Name = "MIT License",
                    Url = new Uri("https://opensource.org/licenses/MIT")
                }
            });

            // Add JWT Authentication to Swagger
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter 'Bearer' [space] and then your token. Example: Bearer abc123"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>() // No roles/policies required for basic JWT validation
                }
            });
        });

        return services;
    }

    public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Inventory Management API v1");
            options.RoutePrefix = string.Empty; // Swagger at root path
        });

        return app;
    }
}
