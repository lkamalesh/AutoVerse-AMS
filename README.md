# AutoVerse – Automobile Management System 

AutoVerse is a full-stack **Automobile Management System** built as a portfolio-grade project to demonstrate clean architecture,
real-world features, and enterprise-style patterns** using ASP.NET Core.

It’s designed to showcase a structured professional project with:
- Layered architecture (Core, Infrastructure, Web, API)
- Repository pattern for data access
- ASP.NET Identity for authentication & roles (Admin, Customer)
- MVC front-end + REST API for integrations
- Rating system for vehicles
- Centralized exception handling and logging
---

## Features - 

### Technologies Used
- ASP.NET Core 8 / MVC
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- ASP.NET Identity
- JWT Authentication
- Repository Pattern
- Serilog (File + Console Logging)
- Bootstrap 5
- 
### User & Role Management
- User registration & login using **ASP.NET Identity**
- Role-based access: **Admin** and **Customer**
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

## Quick Start for Local Setup

**1. Clone**
```bash
git clone https://github.com/lkamalesh/AutoVerse-AMS.git
cd AutoVerse-AMS

**2. Configure Database**

Update connection string in:

/AutoVerse.Web/appsettings.json
/AutoVerse.API/appsettings.json

Then apply migrations: Update-Database

**3. Run MVC**

Set AutoVerse.Web as startup → Run

**4. Run API**

Set AutoVerse.API as startup → Run.

**API Documentation (with Working JSON Examples)**

**Postman Testing Section:**
----------------------------------------------------
**| Method | Endpoint                                 |**
**| ------ | ---------------------------------------  |**

**| POST   | `/api/Auth/register`                     |**

**| POST   | `/api/Auth/login`                        |**

**| GET    | `/api/VehicleApi/GetAll                  |**

**| GET    | `/api/VehicleApi/GetById/{id}            |**

**| GET    | `/api/VehicleApi/GetByBrand/{brandName}  |**

**| POST   | `/api/VehicleApi/Create                  |**

**| PUT    | `/api/VehicleApi/Edit                    |**

**| DELETE | `/api/VehicleApi/Delete/{id}             |**
----------------------------------------------------

**Customer Test Account (browse vehicles)**

Register ->

POST /api/Auth/register
{
  "fullName": "John victor",
  "email": "john@gmail.com",
  "password": "Password123!"
  "confirmpassword": "Password123!"
}

Login ->

POST /api/Auth/login

{
  "email": "john@gmail.com",
  "password": "Password123!"
}

Returns:

{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI..."
}


Use this as Bearer Token in Postman.

**Admin Test Account (For API & MVC)**

Use this test admin account to try Vehicle CRUD:
{
  Email: "administrator@autoverse.com"  
  Password: "iamAdmin@123"
}

**Vehicle API - sample inputs to test:**

Create Vehicle ->

{
  "model": "Tata Nexon EV",
  "brandId": 1,
  "vehicleTypeId": 2,
  "price": 1400000,
  "fuelType": "Electric",
  "transmission": "Automatic",
  "mileage": 312,
  "topSpeed": 120,
  "seatingCapacity": 5
}

Update Vehicle ->

{
  "id": 6,
  "model": "Tata Nexon EV - Long Range",
  "brandId": 1,
  "vehicleTypeId": 2,
  "price": 1550000,
  "fuelType": "Electric",
  "transmission": "Automatic",
  "mileage": 390,
  "topSpeed": 130,
  "seatingCapacity": 5,
}
