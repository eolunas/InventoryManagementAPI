using InventoryManagement.API.Auth;
using InventoryManagement.API.Middleware;
using InventoryManagement.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add Database Context
builder.Services.AddDbContext<InventoryDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

builder.Services.AddApplicationServices(); // Dependencies
builder.Services.AddSwaggerDocumentation(); // Swagger config

builder.Services.AddAuthorization();
builder.Services.AddScoped<JwtService>();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Apply Migrations at Startup and execute SeedData:
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<InventoryDbContext>();

    try
    {
        dbContext.Database.Migrate(); // Apply migrations
        SeedData.InitializeAsync(services).Wait(); // Execute SeedData
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Database migration failed: {ex.Message}");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Apply global error handling middleware
app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseSwaggerDocumentation();
app.UseAuthentication(); // Enable authentication
app.UseAuthorization();

app.MapControllers();

app.Run();

