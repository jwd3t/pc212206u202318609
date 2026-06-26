# PC212206.U202318609.Capbase.Platform

## Overview

This project is a RESTful API built with ASP.NET Core and .NET 10 for the Capbase case study.
The application is designed following Domain-Driven Design principles, bounded contexts, layered architecture, and CQRS-oriented organization.

The main business goal of the API is to manage Covenant registration through the `/api/v1/covenants` endpoint.

## Main Features

- RESTful API with controller-based endpoints
- OpenAPI documentation with Swagger
- MySQL persistence through Entity Framework Core
- Shared infrastructure for auditing, repositories, unit of work, and error handling
- Localization support for English and Spanish responses
- Database naming conventions using snake_case and plural table names
- Kebab-case route naming convention for endpoints

## Technology Stack

- C# 14
- .NET 10
- ASP.NET Core
- Entity Framework Core
- MySQL
- Swashbuckle.AspNetCore

## Project Structure

The solution is organized by bounded contexts and shared technical components.

- `Shared`: reusable cross-cutting infrastructure, persistence base classes, localization resources, and HTTP pipeline helpers
- `Paperwork`: business bounded context for Covenant-related domain logic, application services, persistence mappings, and REST interfaces

## Local Setup

### Prerequisites

- .NET 10 SDK
- MySQL Server

### Configuration

Update the connection string in `appsettings.Development.json`.

Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "server=localhost;user=root;password=your_password;database=clerky_wa"
  }
}
```

## Running the Application

1. Restore dependencies.
2. Build the project.
3. Run the API from Rider or with `dotnet run`.
4. Open Swagger in the browser using the generated local URL.

## API Documentation

Swagger UI is enabled for development and provides interactive documentation for the API endpoints.

## Author

- Student Code: `202318609`
- Author: `Juan`  
  Update this line with your full name if your submission requires the exact author name.
