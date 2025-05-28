# Douce Project - ScentifyWebApp

ScentifyWebApp is an e-commerce platform built with ASP.NET Core MVC, designed for selling perfumes. It features a customer-facing storefront and an administrative backend for managing products and orders.

-----

## Table of Contents

  * [Features](https://www.google.com/search?q=%23features)
  * [Project Architecture](https://www.google.com/search?q=%23project-architecture)
  * [Prerequisites](https://www.google.com/search?q=%23prerequisites)
  * [Setup Instructions](https://www.google.com/search?q=%23setup-instructions)
  * [Running the Project](https://www.google.com/search?q=%23running-the-project)
  * [Database](https://www.google.com/search?q=%23database)
  * [Configuration](https://www.google.com/search?q=%23configuration)

-----

## Features

  * **Customer Facing:**
      * Browse and search for perfumes.
      * View product details, including ingredients, notes, and pricing by size.
      * Add products to the shopping cart.
      * Checkout process with customer information collection.
      * Static pages like Contact Us, Policy, and Commitment.
      * Email order confirmation.
  * **Admin Panel:**
      * Secure login.
      * Dashboard overview.
      * **Perfume Management:** Create, Read, Update, and Delete perfumes, including managing images, pricing, ingredients, and descriptions.
      * **Invoice Management:** View and update invoice statuses.
      * **Contact Us Management:** Update contact information displayed on the site.
      * **Email Template Management:** Upload and manage email templates.

-----

## Project Architecture

The project follows a standard ASP.NET Core MVC structure with some key architectural choices:

  * **Technology Stack:**
      * **Backend:** ASP.NET Core MVC (.NET - looks like version 9 based on migrations), C\#
      * **Database:** SQL Server
      * **ORM:** Entity Framework Core (EF Core)
      * **Frontend:** HTML, CSS (SCSS compiled), JavaScript (likely jQuery with Bootstrap), Bootstrap 5.3.3
      * **Authentication:** Cookie-based authentication for the Admin area.
      * **Services:** Dependency Injection is used to manage services for cart, invoices, email, configuration, etc.
      * **Mapping:** AutoMapper is used for object-to-object mapping (e.g., Entities to DTOs).
  * **Project Structure:**
      * **`ScentifyWebApp`**: The main ASP.NET Core project.
          * **`Areas/Admin`**: Contains controllers and views specific to the administration panel.
          * **`Controllers`**: Contains controllers for the customer-facing site (Home, Products, Cart, Checkout, etc.).
          * **`DAL/DB`**: Contains the `ApplicationDbContext` for database interactions.
          * **`Migrations`**: EF Core database migration files.
          * **`Models`**: Includes `Entities` (database tables), `Dtos` (Data Transfer Objects), `ViewModels`, and `Requests`.
          * **`Services`**: Contains interfaces (`Contracts`) and implementations for business logic (Cart, Invoice, Email, etc.).
          * **`Infrastructure`**: Holds constants, configuration models, and infrastructure services like Email.
          * **`Views`**: Razor views for both customer and admin areas.
          * **`wwwroot`**: Static assets (CSS, JS, images, data files).
          * **`Helper`**: Utility classes for various tasks like Image Optimization, Session Management, etc..
  * **Database:**
      * Uses EF Core Code-First approach (with migrations).
      * Stores `Perfume` data (including JSON for complex properties like `PriceInfo` and `Ingredients`) and `Invoice` data.

-----

## Prerequisites

  * **.NET SDK**: (Version 9.0 or compatible, based on project files)
  * **SQL Server**: (Local or remote instance)
  * **Visual Studio 2022** or later (Recommended) or a text editor with .NET CLI support.

-----

## Setup Instructions

1.  **Clone the Repository:**
    ```bash
    git clone <repository_url>
    cd <project_directory>/ScentifyWebApp
    ```
2.  **Configure Database:**
      * Ensure you have a SQL Server instance running.
      * Open `ScentifyWebApp/appsettings.json`.
      * Modify the `DefaultConnection` string to point to your SQL Server instance and database. It's recommended to create a database named `DOUCE_DB`.
        ```json
        "ConnectionStrings": {
          "DefaultConnection": "Server=YOUR_SERVER;Database=DOUCE_DB;Trusted_Connection=True;TrustServerCertificate=True;Integrated Security=False;Persist Security Info=False;User ID=YOUR_USER;Password=YOUR_PASSWORD;"
        },
        ```
3.  **Apply Database Migrations:**
      * Open a terminal or Package Manager Console in the `ScentifyWebApp` directory.
      * Run the EF Core update command:
        ```bash
        dotnet ef database update
        ```
        or in Package Manager Console:
        ```bash
        Update-Database
        ```
4.  **Seed Database (Optional but Recommended):**
      * You can use a SQL management tool to run the provided `export-db-prod/export_douce_db_28_05_2025.sql` script against your `DOUCE_DB`. This will restore the database with existing product data, which is crucial for testing and running the application as intended.
      * Alternatively, if an `insert_product.sql` script is available (mentioned in the old README), you can run that.
5.  **Review Configuration:**
      * Check `appsettings.json` for other settings:
          * `Momo`: Configuration for MoMo payment gateway (requires a test or production account).
          * `AdminAccount`: Set the admin username and password.
          * `EmailSettings`: Configure your SMTP server details for sending order confirmation emails.
6.  **Install Frontend Dependencies (if needed):**
      * The project uses `libman.json` to manage Bootstrap. Visual Studio usually handles this automatically. If not, you might need to restore client-side libraries.
7.  **Build the Project:**
    ```bash
    dotnet build
    ```

-----

## Running the Project

1.  **Run from Visual Studio:**
      * Open the solution file (`.sln`) in Visual Studio.
      * Select `ScentifyWebApp` as the startup project.
      * Press `F5` or click the "Run" button (select the `https` profile for the best experience).
2.  **Run from .NET CLI:**
      * Navigate to the `ScentifyWebApp` directory in your terminal.
      * Run the command:
        ```bash
        dotnet run
        ```
3.  **Access the Application:**
      * Open your web browser and navigate to the URL provided in the console output (e.g., `https://localhost:7159`).
4.  **Access Admin Panel:**
      * Navigate to `/Admin` (e.g., `https://localhost:7159/Admin`).
      * Log in using the credentials defined in `appsettings.json` (default might be `admin`/`computer`).

-----

## Database

  * The project uses **Entity Framework Core** to manage the database schema.
  * Migrations are located in the `ScentifyWebApp/Migrations` folder.
  * The main entities are `Perfume` and `Invoice`.
  * Complex data within entities (like `PriceInfo`, `Ingredients`, `CustomerInfo`) is stored as **JSON strings** in the database. The `JsonHelpers` class assists in parsing this data.
  * An SQL export `export_douce_db_28_05_2025.sql` is provided to set up the database with production-like data.

-----

## Configuration

  * All major configurations are handled through `appsettings.json`.
  * **`ConnectionStrings`**: Database connection details.
  * **`Jwt`**: Settings for JWT (though the primary auth seems to be Cookies).
  * **`AdminAccount`**: Credentials for the admin login.
  * **`EmailSettings`**: SMTP details for sending emails.
  * **`Momo`**: Credentials for the MoMo payment gateway.
  * **Session**: Session timeout and cookie settings.
