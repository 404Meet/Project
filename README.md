# Stock / Portfolio REST API (.NET 8) 📈🔐

[![.NET](https://img.shields.io/badge/.NET-8.0-purple)](#)
[![API](https://img.shields.io/badge/API-ASP.NET%20Core-blue)](#)
[![DB](https://img.shields.io/badge/DB-SQL%20Server-informational)](#)
[![Auth](https://img.shields.io/badge/Auth-JWT%20%2B%20Identity-green)](#)

A backend **ASP.NET Core Web API** for managing **stocks, portfolios, and comments**, with **JWT authentication** and **ASP.NET Identity**. Includes filtering/sorting/pagination for stock listings and role support (**Admin/User**) seeded via EF Core migrations.

---

## Features
- ✅ **JWT Auth** (login/register) using **ASP.NET Identity**
- ✅ **Role-based access** (roles seeded: `Admin`, `User`)
- ✅ **Stocks API**: CRUD + **PATCH partial updates**
- ✅ **Query support**: filter/sort/paginate stocks
- ✅ **Portfolios API**: add/remove stocks by symbol (per user)
- ✅ **Comments API**: CRUD + link comments to stock + user
- ✅ Swagger UI with **Bearer token** support

---

## Tech Stack
- **.NET 8 / ASP.NET Core**
- **Entity Framework Core 8**
- **SQL Server**
- **ASP.NET Identity**
- **JWT Bearer Auth**
- **Swagger / OpenAPI**

---

## Getting Started

### 1) Prerequisites
- **.NET SDK 8**
- **SQL Server** (local or remote)

### 2) Configure environment variables (recommended)
This repo uses `appsettings.json`, but you should override values via env vars.

---

**Required**
- `ConnectionStrings__DefaultConnection`
- `JWT__Issuer`
- `JWT__Audience`
- `JWT__SigningKey`
- 
---

#### macOS / Linux
-`export ConnectionStrings__DefaultConnection="Server=localhost;Database=StockComment;User Id=sa;Password=StrongPass1234;TrustServerCertificate=true"`
-`export JWT__Issuer="http://localhost:5043"`
-`export JWT__Audience="http://localhost:5043"`
-`export JWT__SigningKey="CHANGE_THIS_TO_A_LONG_RANDOM_SECRET"`

---

### Windows
-`$env:ConnectionStrings__DefaultConnection="Server=localhost;Database=StockComment;User Id=sa;Password=StrongPass1234;TrustServerCertificate=true"`
-`$env:JWT__Issuer="http://localhost:5043"`
-`$env:JWT__Audience="http://localhost:5043"`
-`$env:JWT__SigningKey="CHANGE_THIS_TO_A_LONG_RANDOM_SECRET"`

---

###
1) Restore + Run
-`cd MyWebApi`
-`dotnet restore`
-`dotnet run`

---

###
2) Apply EF Core migrations
-If your DB is empty, run:
-`dotnet tool install --global dotnet-ef`
-`dotnet ef database update`

---

###
3) Authentication
-Password rules (Identity)
-Requires digit
-Requires lowercase
-Requires non-alphanumeric
-Minimum length 12

---

###
4) Register POST: /api/account/Register
-curl -X POST "https://localhost:5043/api/account/Register" \
-  -H "Content-Type: application/json" \
-  -d '{"username":"meet","email":"meet@email.com","password":"StrongPass!1234"}'

---

###
5) Login POST /api/account/login
-curl -X POST "https://localhost:5043/api/account/login" \
-  -H "Content-Type: application/json" \
-  -d '{"username":"meet","password":"StrongPass!1234"}'
-Copy the token from the response and use: -H "Authorization: Bearer <TOKEN>"

---

###
6) API Endpoints 
Stocks
-Base: /api/stock (Authorized)
-GET /api/stock
-GET /api/stock/{id}
-POST /api/stock
-PUT /api/stock/{id}
-PATCH /api/stock/{id}
-DELETE /api/stock/{id}

Portfolio
-Base: /api/portfolio (Authorized)
-GET /api/portfolio → current user portfolio
-POST /api/portfolio?symbol=AAPL → add by symbol
-DELETE /api/portfolio?symbol=AAPL → remove by symbol

Comments
-Base: /api/comment (Authorized)
-GET /api/comment
-GET /api/comment/{id}
-POST /api/comment/{stockId}
-PUT /api/comment/{id}
-DELETE /api/comment/{id}

---
