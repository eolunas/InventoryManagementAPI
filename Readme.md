# InventoryManagement API

## 📌 Project Overview
The **InventoryManagement API** is a **RESTful web service** developed in **.NET 9** with **C#**, using **Entity Framework Core** and **PostgreSQL**. The API enables users to **manage inventory**, track **product movements**, and handle **authentication** securely using **JWT (JSON Web Tokens)**.

## 📂 Project Structure
The solution follows **Domain-Driven Design (DDD)** and **Clean Architecture**, divided into four main layers:

```
📦 InventoryManagement
 ┣ 📂 InventoryManagement.Domain        # Domain Layer (Entities, Interfaces)
 ┃ ┣ 📂 Entities
 ┃ ┃ ┣ 📜 Product.cs
 ┃ ┃ ┣ 📜 InventoryMovement.cs
 ┃ ┃ ┗ 📜 AuditLog.cs
 ┃ ┣ 📂 Interfaces
 ┃ ┃ ┣ 📜 IProductRepository.cs
 ┃ ┃ ┗ 📜 IInventoryMovementRepository.cs
 ┣ 📂 InventoryManagement.Application   # Application Layer (Business Logic, DTOs, Services)
 ┃ ┣ 📂 DTOs
 ┃ ┃ ┣ 📜 ProductDto.cs
 ┃ ┃ ┗ 📜 ProductMovementDto.cs
 ┃ ┣ 📂 Interfaces
 ┃ ┃ ┗ 📜 IProductService.cs
 ┃ ┗ 📂 Services
 ┃ ┃ ┗ 📜 ProductService.cs
 ┣ 📂 InventoryManagement.Infrastructure # Infrastructure Layer (Database, Repositories)
 ┃ ┣ 📂 Data
 ┃ ┃ ┣ 📜 InventoryDbContext.cs
 ┃ ┃ ┗ 📜 SeedData.cs
 ┃ ┣ 📂 Repositories
 ┃ ┃ ┣ 📜 ProductRepository.cs
 ┃ ┃ ┗ 📜 InventoryMovementRepository.cs
 ┣ 📂 InventoryManagement.API           # Presentation Layer (Controllers, Auth, Configuration)
 ┃ ┣ 📂 Controllers
 ┃ ┃ ┣ 📜 ProductController.cs
 ┃ ┃ ┗ 📜 AuthController.cs
 ┃ ┣ 📂 Auth
 ┃ ┃ ┗ 📜 JwtService.cs
 ┃ ┗ 📜 Program.cs
```

## 🎯 Design Patterns Used
- **Repository Pattern**: Encapsulates data access logic in repositories (`ProductRepository`, `InventoryMovementRepository`).
- **Dependency Injection (DI)**: Injects services and repositories into controllers.
- **Unit of Work (via DbContext)**: Ensures atomic operations across entities.
- **Factory Method (JWT Service)**: Generates JWT tokens in `JwtService`.
- **DTO Pattern**: Separates domain models from external API contracts (`ProductDto`, `ProductMovementDto`).

## 🚀 How to Set Up the Project
### **1️⃣ Clone the Repository**
```bash
git clone https://github.com/your-repo/inventory-management.git
cd inventory-management
```

### **2️⃣ Configure the Database (PostgreSQL)**
Ensure you have **PostgreSQL** installed and running. Create the database manually:
```sql
CREATE DATABASE InventoryDB;
```
Or use the automatic migration system.

### **3️⃣ Configure `appsettings.json`**
Update the connection string in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=InventoryDB;Username=postgres;Password=admin"
  }
}
```

### **4️⃣ Install Dependencies**
Run the following command to restore dependencies:
```bash
dotnet restore
```

### **5️⃣ Apply Database Migrations & Seed Data**
```bash
dotnet ef database update --project InventoryManagement.Infrastructure --startup-project InventoryManagement.API
```
This will apply migrations and seed initial data (products) into the database.

### **6️⃣ Run the Application**
```bash
dotnet run --project InventoryManagement.API
```

### **7️⃣ Open Swagger UI for API Documentation**
Visit:
```
http://localhost:5000/swagger
https://localhost:7132/swagger
```
Here you can test endpoints and authenticate via JWT.

---

## 📡 API Endpoints

### **🔐 Authentication**
| Method | Endpoint | Description |
|--------|---------|-------------|
| `POST` | `/auth/login` | Authenticates a user and returns a JWT token |

**Example Request:**
```json
{
  "username": "admin",
  "password": "password"
}
```

**Response:**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI..."
}
```

### **📦 Product Management**
| Method | Endpoint | Description |
|--------|---------|-------------|
| `GET`  | `/products/inventory` | Retrieves all products and their stock |
| `POST` | `/products/movement` | Registers a product movement (in/out stock) |

**Example Request (`POST /products/movement`)**
```json
{
  "productId": 1,
  "quantity": 10
}
```

🔹 **Authentication Required:** Include `Bearer <token>` in the `Authorization` header.

**Example Response (`GET /products/inventory`)**
```json
[
  {
    "id": 1,
    "name": "Laptop",
    "quantity": 15
  },
  {
    "id": 2,
    "name": "Mouse",
    "quantity": 50
  }
]
```

---

## ✅ Best Practices Followed
- **SOLID Principles** applied in service and repository layers.
- **Secure Authentication** using JWT and authorization middleware.
- **Database Migrations & Seeding** to ensure consistent data.
- **Swagger Documentation** to ease API testing.

🚀 **Now you’re ready to use the Inventory Management API!** 😊

