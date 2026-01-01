# AutoVerse – Powered by TopSpeed Motors 

**Live Demo (Azure)**

 `MVC Customer Website:`

https://autoverse-web-dmdzhxcbgaf4b8f7.southeastasia-01.azurewebsites.net

 `Web API (Swagger):`

https://autoverse-api-arfxgvdkhcc5a7e2.southeastasia-01.azurewebsites.net/swagger

Deployed using Azure Database and Azure App Service.

**Overview**

AutoVerse is a full-stack **Automobile Management System** built as a portfolio-grade project to demonstrate clean architecture,
real-world features, and enterprise-style patterns** using ASP.NET Core.

It’s designed to showcase a structured professional project with:
- N-tier architecture (Core, Infrastructure, Web, API)
- Repository pattern for data access
- ASP.NET Identity for authentication & roles (Admin, Customer)
- MVC front-end + REST API for integrations
- Rating system for vehicles
- Centralized exception handling and logging
- Hosted on Microsoft Azure
---

## Features - 

### Technologies Used
- ASP.NET Core 8 / MVC
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- ASP.NET Identity
- JWT & Cookie Authentication
- Repository Pattern
- Serilog (File + Console Logging)
- Azure App Service (Web + API)
- Azure SQL Database
- Bootstrap 5
  
### User & Role Management
- User registration & login using **ASP.NET Identity**
- Role-based access: **Admin** and **Customer**
- Admin can create/assign new roles
- Admin-only pages for managing vehicles and brands
- Customers can browse, view details, and rate vehicles

###  Vehicle Management(Admin)
- Add / Edit / Delete vehicles 
- Vehicle attributes:
  - Brand, Type
  - Model
  - Price
  - Fuel type
  - Transmission
  - Mileage
  - Top speed
  - Seating capacity
  - Image upload (stored under `wwwroot/images/vehicles`)
- Clean list + details views for customers and admins

### Rating System
- Customers can rate vehicles from **1 - 5 stars**
- Simple `Rating` entity tied to `Vehicle`
- **RatingService**:
  - Saves ratings
  - Calculates the **average rating** per vehicle
  - Persists the average rating on the Vehicle record 

### Search & Browse
- Customer-facing **Home page** to browse vehicles
- Simple filters/search:
  - Brand
  - Vehicle type
  - Price range
- Card-based UI with Bootstrap 5

### REST API (for integration & mobile apps)
- **Auth API** for login/registration with JWT token generation
- **Vehicle API** for CRUD operations
- **Rating API** for submitting and fetching ratings
- All API endpoints follow clean, RESTful patterns and reuse the service & repository layer

### Architecture & Patterns
- **Projects:**
  - `AutoVerse.Core` → Entities + Interfaces
  - `AutoVerse.Infrastructure` → EF Core DbContext, Repositories, Services (data access)
  - `AutoVerse.Web` → MVC app (views, controllers, filters, middleware)
  - `AutoVerse.API` → REST API endpoints
  - 
- **Repository pattern**:
  - `IGenericRepository<T>` with `GenericRepository<T>`
  - `IVehicleRepository`, `IBrandRepository` for specific queries
  - 
- **Service layer**:
  - `IVehicleService`, `VehicleService`
  - `IRatingService`, `RatingService`

### Exception Handling & Logging
- Custom global **`CustomExceptionFilter`**:
  - Handles unhandled exceptions for both MVC and API
  - Returns JSON errors for API calls
  - Returns a friendly error page for MVC
- Centralized logging (designed to work with **Serilog** for file/console logging)

------

##  API – Quick Usage Guide

**API Documentation (with Working JSON Examples)**

**Postman Testing Section:**
----------------------------------------------------
**| Method | Endpoint                                 |**

**| POST   | `/api/Auth/register`                     |**

**| POST   | `/api/Auth/login`                        |**

**| GET    | `/api/VehicleApi/GetAll`                 |**

**| GET    | `/api/VehicleApi/GetById/{id}`           |**

**| GET    | `/api/VehicleApi/GetByBrand/{brandName}` |**

**| POST   | `/api/RatingApi/AddRating`               |**

**| POST   | `/api/VehicleApi/Create`                 |** (Admin only)

**| PUT    | `/api/VehicleApi/Edit`                   |** (Admin only)

**| DELETE | `/api/VehicleApi/Delete/{id}`            |** (Admin only)


-------------

**Customer sample Test Account (browse and rate)**

`Register`

POST /api/Auth/register

{

  "fullName": "John victor",
  "email": "john@gmail.com",
  "password": "Password123!",
  "confirmpassword": "Password123!"
  
}

`Login`

POST /api/Auth/login

{

  "email": "john@gmail.com",
  "password": "Password123!"
  
}

Returns:

{

  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI..."
  
}

Use the Bearer Token in Postman.

---------

