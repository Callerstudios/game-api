# 🎮 Game API

A modern RESTful Web API built with **ASP.NET Core 10** that demonstrates clean architecture principles, secure authentication, and backend best practices.

This project was created as a portfolio project to showcase backend development skills using the .NET ecosystem. It focuses on writing maintainable, testable, and production-oriented code rather than implementing a full game backend.

---

# Features

## Authentication

* JWT Bearer Authentication
* User Registration
* User Login
* Password Hashing using ASP.NET Core Identity PasswordHasher
* Protected Endpoints with Authorization

## Player Management

* Create Player
* Retrieve Player
* Update Player
* Delete Player

## API Features

* Pagination
* Filtering
* Sorting
* Request Validation using FluentValidation
* Global Exception Handling
* RFC 9457 Problem Details responses
* OpenAPI Documentation with Scalar

---

# Tech Stack

### Framework

* ASP.NET Core 10
* C#

### Database

* SQLite
* Entity Framework Core

### Authentication

* JWT Bearer Tokens
* ASP.NET Core Identity PasswordHasher

### Validation

* FluentValidation

### API Documentation

* Microsoft.AspNetCore.OpenApi
* Scalar

---

# Architecture

The project follows a layered architecture.

```
Controllers
        │
        ▼
Services
        │
        ▼
Repositories
        │
        ▼
Entity Framework Core
        │
        ▼
SQLite
```

Responsibilities are separated into dedicated layers:

* Controllers handle HTTP requests.
* Services contain business logic.
* Repositories handle data access.
* Models represent domain entities.
* DTOs define API contracts.
* Validators validate incoming requests.
* Middleware provides centralized exception handling.

---

# Project Structure

```
GameApi
│
├── Configuration
├── Controllers
├── Data
├── DTOs
├── Exceptions
├── Middleware
├── Models
├── Repositories
│   ├── Interfaces
│   └── Implementations
├── Services
│   ├── Interfaces
│   └── Implementations
├── Validators
└── Program.cs
```

---

# Authentication Flow

```
Register
    │
    ▼
Password Hashed
    │
    ▼
Player Saved
    │
    ▼
Login
    │
    ▼
Password Verified
    │
    ▼
JWT Generated
    │
    ▼
Protected Endpoints
```

---

# API Endpoints

## Authentication

| Method | Endpoint             | Description           |
| ------ | -------------------- | --------------------- |
| POST   | `/api/auth/register` | Register a new player |
| POST   | `/api/auth/login`    | Authenticate a player |

---

## Players

| Method | Endpoint            | Description        |
| ------ | ------------------- | ------------------ |
| GET    | `/api/players`      | Get all players    |
| GET    | `/api/players/{id}` | Get a player by ID |
| POST   | `/api/players`      | Create a player    |
| PUT    | `/api/players/{id}` | Update a player    |
| DELETE | `/api/players/{id}` | Delete a player    |

---

# Validation

Incoming requests are validated using FluentValidation before reaching the service layer.

Examples include:

* Required fields
* Email validation
* Username length
* Password length

---

# Error Handling

The API uses centralized exception handling middleware.

Errors are returned as RFC 9457 Problem Details responses.

Example:

```json
{
  "type": "https://httpstatuses.com/409",
  "title": "Conflict",
  "status": 409,
  "detail": "Username 'beekay' already exists.",
  "errorCode": "DUPLICATE_USERNAME"
}
```

---

# Pagination

Collection endpoints support pagination.

Example:

```
GET /api/players?pageNumber=1&pageSize=10
```

Filtering and sorting are also supported.

---

# Running the Project

## Clone the repository

```bash
git clone <repository-url>
```

---

## Navigate to the project

```bash
cd GameApi
```

---

## Restore packages

```bash
dotnet restore
```

---

## Apply database migrations

```bash
dotnet ef database update
```

---

## Run the API

```bash
dotnet run
```

---

## Open Scalar

```
http://localhost:5021/scalar/v1
```

---

# Future Improvements

Potential enhancements include:

* Refresh Tokens
* Role-Based Authorization
* Unit Tests
* Integration Tests
* Docker Support
* MySQL Support
* Redis Caching
* API Versioning
* Rate Limiting
* CI/CD Pipeline

---

# Learning Goals

This project was built to strengthen knowledge of:

* ASP.NET Core
* Entity Framework Core
* REST API Design
* Authentication and Authorization
* Dependency Injection
* Repository Pattern
* Unit of Work Pattern
* Clean Architecture Principles
* Error Handling
* API Validation

---

# License

This project is intended for learning and portfolio purposes.
