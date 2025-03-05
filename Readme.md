
## Estructura de Carpetas

📦 InventoryAPI
 ┣ 📂 Inventory.Domain
 ┃ ┣ 📂 Entities
 ┃ ┃ ┗ 📜 Product.cs
 ┃ ┣ 📂 Interfaces
 ┃ ┃ ┗ 📜 IProductRepository.cs
 ┃ ┗ 📜 DomainAssembly.cs
 ┣ 📂 Inventory.Application
 ┃ ┣ 📂 DTOs
 ┃ ┃ ┗ 📜 ProductDto.cs
 ┃ ┣ 📂 Services
 ┃ ┃ ┗ 📜 ProductService.cs
 ┃ ┗ 📜 ApplicationAssembly.cs
 ┣ 📂 Inventory.Infrastructure
 ┃ ┣ 📂 Data
 ┃ ┃ ┗ 📜 InventoryDbContext.cs
 ┃ ┣ 📂 Repositories
 ┃ ┃ ┣ 📜 RepositoryBase.cs (Repositorio Genérico)
 ┃ ┃ ┗ 📜 ProductRepository.cs
 ┃ ┗ 📜 InfrastructureAssembly.cs
 ┣ 📂 Inventory.Presentation (API)
 ┃ ┣ 📂 Controllers
 ┃ ┃ ┗ 📜 ProductController.cs
 ┃ ┣ 📂 Auth
 ┃ ┃ ┗ 📜 JwtService.cs
 ┃ ┗ 📜 PresentationAssembly.cs
 ┣ 📜 appsettings.json
 ┣ 📜 Program.cs
 ┗ 📜 InventoryAPI.sln
