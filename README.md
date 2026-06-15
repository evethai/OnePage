# QR Product One-Page System

[![.NET 8.0](https://img.shields.io/badge/.NET-8.0-blue.svg)](https://dotnet.microsoft.com/download/dotnet/8.0)
[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](https://opensource.org/licenses/MIT)
[![Database](https://img.shields.io/badge/Database-SQLite%20%7C%20PostgreSQL-orange.svg)]()

A high-performance, single-page product information retrieval system accessed via physical QR codes. Designed with a mobile-first philosophy, it features a secure time-limited session token, an administrative dashboard for product CRUD operations, bulk import/update capabilities via Excel spreadsheets, and dynamic QR code generation.

---

## 📖 Table of Contents
* [Features](#-features)
* [System Architecture](#-system-architecture)
* [Technology Stack](#-technology-stack)
* [Project Structure](#-project-structure)
* [Getting Started (Local Development)](#-getting-started-local-development)
  * [Prerequisites](#prerequisites)
  * [Database Setup](#database-setup)
  * [Running the Application](#running-the-application)
* [Docker Deployment (Production)](#-docker-deployment-production)
* [Database Swapping (SQLite to PostgreSQL)](#-database-swapping-sqlite-to-postgresql)
* [License](#-license)

---

## ✨ Features

### Public Product View (Single-Page App)
* **Zero Authentication for Scanners:** Accessible directly by any mobile device scanning the physical QR code without requiring user login or registration.
* **15-Minute Token Security:** A secure, cryptographically signed HTTP-Only Cookie session token is issued upon scanning. The session automatically expires after 15 minutes, prompting users to re-scan the physical QR code to maintain access, preventing remote link-sharing.
* **Mobile-First SPA:** A modern, lightweight, glassmorphic UI optimized for speed, loading in under 1 second on 3G/4G networks.

### Administrative Dashboard
* **Secure Auth:** Access-restricted dashboard using secure cookies/JWT.
* **Product CRUD:** Full catalog management (name, SKU, description, pricing, inventory, dynamic specifications).
* **Excel Bulk Import/Update:** Fast processing of thousands of products using spreadsheet templates.
* **QR Code Generation:** Automated creation of unique QR codes for new products, downloadable as high-quality PNGs or SVGs for printing.

---

## 🏗️ System Architecture

The project adheres to **Clean Architecture** principles, maintaining a strict separation of concerns:

```
[Presentation Layer: Web API & SPA]
         │
         ▼
[Infrastructure Layer: EF Core, Excel, QR, Local Storage Services]
         │
         ▼
[Domain Layer: Entities & Core Interfaces]
```

* **Domain:** Houses core entities (`Product`, `AdminUser`) and abstractions (`IProductRepository`, etc.).
* **Infrastructure:** Implements databases (SQLite/PostgreSQL), file storage, Excel processing, and QR generation.
* **Api (Presentation):** Provides RESTful API endpoints and serves the static SPA frontend from the `/wwwroot` directory.

---

## 💻 Technology Stack

* **Backend:** ASP.NET Core 8.0 Web API (Clean Architecture)
* **Frontend:** HTML5, CSS3 (Vanilla / Tailwind), Vanilla JavaScript SPA
* **ORM:** Entity Framework Core 8.0
* **Libraries:**
  * `QRCoder`: Dynamic PNG/SVG QR code rendering (cross-platform compatible).
  * `ClosedXML`: High-performance Excel reading/writing.
  * `SixLabors.ImageSharp`: Image optimization and WebP compression.
* **Database:** SQLite (local development), PostgreSQL (production).

---

## 📂 Project Structure

```
e:\OnePage\
├── OnePage.sln                  # .NET Solution File
├── docker-compose.yml           # Production Docker Compose setup
├── .gitignore                   # Git exclusion rules
├── README.md                    # Project documentation
└── src/
    ├── OnePage.Domain/          # Domain Entities & Interfaces
    │   ├── Entities/            # Product.cs, AdminUser.cs
    │   └── Interfaces/          # Core interfaces
    │
    ├── OnePage.Infrastructure/  # EF Core DbContext, Repositories & Services
    │   ├── Data/                # AppDbContext and Migrations
    │   ├── Repositories/        # ProductRepository implementation
    │   └── Services/            # ExcelService, QrCodeService, LocalStorage
    │
    └── OnePage.Api/             # Web API Controllers & Static Frontend Files
        ├── Controllers/         # ProductsController, AuthController, ExcelController
        ├── wwwroot/             # Client-side SPA (index.html, css, js)
        │   ├── uploads/         # Local file storage folder
        │   └── index.html       # Single-page interface
        ├── appsettings.json     # Configuration file
        └── Program.cs           # API entry point & DI configuration
```

---

## 🚀 Getting Started (Local Development)

### Prerequisites
* [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
* A code editor (Visual Studio 2022, VS Code, or Antigravity IDE)

### Database Setup
The application is pre-configured to run with **SQLite** for zero-configuration local testing. 

1. Install the Entity Framework Core CLI tools (if not already installed):
   ```bash
   dotnet tool install --global dotnet-ef
   ```
2. Database migrations are automatically applied on application startup. You do not need to run manual migration commands to start testing.

### Running the Application
To launch the Web API and serve the frontend SPA:

1. Restore NuGet dependencies:
   ```bash
   dotnet restore
   ```
2. Run the Web API project:
   ```bash
   dotnet run --project src/OnePage.Api
   ```
3. Open your browser and navigate to:
   * **Web Application:** `http://localhost:5000` (ASP.NET Core hosts the SPA from `wwwroot` here).
   * **Swagger API Documentation:** `http://localhost:5000/swagger`

---

## 🐋 Docker Deployment (Production)

To containerize and launch the Web API alongside a PostgreSQL database, use Docker Compose:

1. Ensure Docker is running.
2. Build and launch the containers:
   ```bash
   docker-compose up -d --build
   ```
3. The containers will run in the background, serving the application on port `80` (or your configured port).

---

## 💾 Database Swapping (SQLite to PostgreSQL)

To migrate from the local SQLite database to PostgreSQL on production:

1. Open `src/OnePage.Api/appsettings.json`.
2. Configure your connection string in the `ConnectionStrings:PostgresConnection` section.
3. Toggle the database provider switch:
   ```json
   "UsePostgres": true
   ```
4. On the next startup, the application's runtime will automatically select PostgreSQL and run migrations on your remote DB server.

---

## 📄 License

This project is licensed under the MIT License - see the LICENSE file for details.
