# InventoryManagement API

## 📌 Project Overview
The **InventoryManagement API** is a **RESTful web service** developed in **.NET 9** with **C#**, using **Entity Framework Core** and **PostgreSQL**. The API enables users to **manage inventory**, track **product movements**, and handle **authentication** securely using **JWT (JSON Web Tokens)**.

🔗 **[🎥 Explanation Video](https://youtu.be/xrOs93DmriE)** 

💻 **Frontend Project:**  
A full **Angular 17** frontend was developed for this API, implementing:
- **User Authentication with JWT**
- **Product Management (CRUD)**
- **Inventory Movements with Filtering & History**
- **Responsive UI with Tailwind CSS**
- **Navigation with Lazy Loading & Route Guards**
- **State Management & HTTP Interceptors**

## 📂 Project Structure
The solution follows **Domain-Driven Design (DDD)** and **Clean Architecture**, divided into four main layers:

```
📦 InventoryManagement
 ┣ 📂 InventoryManagement.Domain        # Domain Layer (Entities, Interfaces)
 ┣ 📂 InventoryManagement.Application   # Application Layer (Business Logic, DTOs, Services)
 ┣ 📂 InventoryManagement.Infrastructure # Infrastructure Layer (Database, Repositories)
 ┣ 📂 InventoryManagement.API           # Presentation Layer (Controllers, Auth, Configuration)
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
Update the connection string in `appsettings.json` [Crete it into InventoryManagement.API]:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=InventoryDB;Username=postgres;Password=admin"
  },
  "Jwt": {
    "Key": "YourSuperSecretKeyWithAtLeast32Characters",
    "Issuer": "InventoryManagementAPI",
    "Audience": "InventoryManagementClients",
    "TokenValidityInMinutes": 60
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


### **8️⃣ Open the Angular project**
Go to frontend file and in terminal execute:
```bash
npm start
```

Visit:
```
http://localhost:4200/
```
Here you can test project from UI, keep in mind that user and password are the same as Authentication section.

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
| `POST` | `/products` | Creates a new product |
| `PUT`  | `/products/{id}` | Updates an existing product |
| `DELETE` | `/products/{id}` | Deletes a product (only if no movements exist) |

🔹 **Authentication Required:** Include `Bearer <token>` in the `Authorization` header.

**Example Request (`POST /products`)**
```json
{
  "name": "Laptop",
  "quantity": 15
}
```

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

### **📦 Inventory Movements**
| Method | Endpoint | Description |
|--------|---------|-------------|
| `POST` | `/products/movement` | Registers a product movement (in/out stock) |
| `GET`  | `/products/movements` | Retrieves filtered movement history |

🔹 **Authentication Required:** Include `Bearer <token>` in the `Authorization` header.

**Filters Available for `/products/movements`**
- `productId` → Filter by product ID
- `productName` → Filter by product name (partial match)
- `startDate` → Filter from a specific date (`yyyy-MM-dd`)
- `endDate` → Filter until a specific date (`yyyy-MM-dd`)
- `type` → Filter by movement type (`In`, `Out`)

**Example Request (`POST /products/movement`)**
```json
{
  "productId": 1,
  "quantity": -5
}
```

---


## 🎨 Frontend Integration
The **frontend is developed in Angular 16** with the following features:
- **Authentication with JWT**: Login and token-based security.
- **CRUD Operations for Products**: Add, edit, delete products.
- **Inventory Movements**: Register and track product stock changes.
- **Filtering & Pagination**: List movements based on product name, date, and type.
- **Responsive UI with Tailwind CSS**: Modern styling with dark/light theme support.
- **Navigation with Lazy Loading & Guards**: Secure routes and structured modules.
- **HTTP Interceptors**: Automatically attach JWT tokens to requests.

## ✅ Best Practices Followed
- **SOLID Principles** applied in service and repository layers.
- **Secure Authentication** using JWT and authorization middleware.
- **Database Migrations & Seeding** to ensure consistent data.
- **Swagger Documentation** to ease API testing.

🚀 **Now you’re ready to use the Inventory Management API!** 😊

