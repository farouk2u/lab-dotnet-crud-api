# .NET CRUD API

A simple CRUD (Create, Read, Update, Delete) API built with .NET that demonstrates RESTful API development, middleware implementation, and repository pattern.

## Features

- RESTful API endpoints for user management
- Custom middleware for request logging
- API key authentication
- OpenAPI/Swagger documentation in development environment
- Repository pattern for data access

## Tech Stack

- .NET 8+
- ASP.NET Core
- OpenAPI/Swagger

## Getting Started

### Prerequisites

- .NET SDK 8.0 or later
- Visual Studio, VS Code, or your preferred IDE

### Installation

1. Clone the repository
   ```bash
   git clone https://github.com/farouk2u/lab-dotnet-crud-api
   cd dotnet-crud-api
   ```

2. Build the project
   ```bash
   dotnet build
   ```

3. Run the application
   ```bash
   dotnet run
   ```

The API will be accessible at `https://localhost:5001` and `http://localhost:5000` by default.

## API Documentation

When running in development mode, you can access the OpenAPI/Swagger documentation at `/swagger`.

### Authentication

The API uses API key authentication. Include your API key in the request header:

```
X-API-KEY: your-api-key
```

### Endpoints

The API provides endpoints for user management. Check the OpenAPI documentation for detailed information about the available endpoints and request/response models.

## Project Structure

- **Controllers/**: Contains API controllers
- **Middleware/**: Custom middleware components (Request logging, API key authentication)
- **Models/**: Data models and DTOs
- **Repositories/**: Data access layer using repository pattern

## Development

### Logging

The application uses the .NET logging providers for Console, Debug, and EventSource logging.

### Error Handling

In production, errors are handled by the default error handler middleware and redirected to `/error`.

## License

[MIT](LICENSE)
