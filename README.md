# Employee Management System

A full-stack Employee Management application built to demonstrate integration between a **Java Spring Boot REST API** and a **C# Windows Forms client**.

The project implements authentication, JWT-based API security, employee CRUD operations, SQL Server persistence, and Swagger/OpenAPI documentation.

---

## Architecture

```text
┌──────────────────────────────┐
│       C# Windows Forms       │
│                              │
│  Login                       │
│  Employee List               │
│  Add / Edit / Delete         │
│  Logout                      │
└──────────────┬───────────────┘
               │
               │ HTTP / JSON
               │ JWT Bearer Token
               ▼
┌──────────────────────────────┐
│    Java Spring Boot API      │
│                              │
│  REST Controllers            │
│  Spring Security             │
│  JWT Authentication          │
│  Spring Data JPA             │
│  Hibernate                   │
└──────────────┬───────────────┘
               │
               │ JDBC
               ▼
┌──────────────────────────────┐
│          SQL Server          │
│                              │
│  employees                   │
│  users                       │
└──────────────────────────────┘
```

---

## Project Structure

```text
employee-management/
│
├── backend/
│   └── employee-api/
│       ├── src/
│       │   └── main/
│       │       └── java/
│       │           └── employee_api/
│       │               ├── config/
│       │               ├── controller/
│       │               ├── dto/
│       │               ├── entity/
│       │               ├── repository/
│       │               └── security/
│       │
│       └── pom.xml
│
├── frontend/
│   └── EmployeeClient/
│       ├── Models/
│       ├── Services/
│       ├── Form1.cs
│       ├── Form2.cs
│       ├── Form3.cs
│       └── EmployeeClient.csproj
│
├── .gitignore
└── README.md
```

---

## Features

### Authentication

- Login using username and password
- Password hashing using BCrypt
- JWT token generation
- JWT Bearer authentication
- Stateless API authentication
- Logout from the Windows Forms application

### Employee Management

- View employee list
- Get employee by ID
- Create employee
- Update employee
- Delete employee
- Delete confirmation dialog
- Automatic refresh after create/update/delete

### API Documentation

Interactive API documentation is provided using Swagger/OpenAPI.

Available endpoints:

```text
POST   /api/auth/login

GET    /api/employees
GET    /api/employees/{id}
POST   /api/employees
PUT    /api/employees/{id}
DELETE /api/employees/{id}
```

---

## Technology Stack

### Backend

- Java 21
- Spring Boot
- Spring Web
- Spring Security
- Spring Data JPA
- Hibernate
- JWT
- Maven
- SQL Server
- Swagger / OpenAPI

### Frontend

- C#
- .NET
- Windows Forms
- HttpClient
- System.Text.Json
- async/await

### Database

- Microsoft SQL Server

---

## Backend Architecture

The backend uses a layered architecture:

```text
Controller
    │
    ▼
Service
    │
    ▼
Repository
    │
    ▼
Database
```

Security components are separated into their own package:

```text
security/
├── DatabaseUserDetailsService
├── JwtAuthenticationFilter
└── JwtService
```

Spring Data JPA is used for database persistence.

---

## Authentication Flow

```text
C# Windows Forms
        │
        │ POST /api/auth/login
        │ username + password
        ▼
Spring Security
        │
        │ authenticate
        ▼
Database User
        │
        │ BCrypt password verification
        ▼
JWT Token
        │
        ▼
C# Windows Forms
        │
        │ Authorization: Bearer <token>
        ▼
Protected API
```

The JWT token is kept in memory by the C# client and attached to protected API requests.

---

## Employee CRUD Flow

Example create flow:

```text
User selects "Add Employee"
          │
          ▼
      Form3.cs
          │
          │ POST /api/employees
          ▼
 EmployeeController
          │
          ▼
 EmployeeRepository
          │
          ▼
      SQL Server
          │
          ▼
     Created Employee
          │
          ▼
    Refresh DataGridView
```

The same client-server architecture is used for update and delete operations.

---

## HTTP Status Codes

The API uses standard HTTP status codes:

```text
200 OK
201 Created
204 No Content
400 Bad Request
401 Unauthorized
403 Forbidden
404 Not Found
500 Internal Server Error
```

For example, a successful employee deletion returns:

```text
204 No Content
```

---

## Error Handling

The Windows Forms client handles API errors and displays user-friendly messages.

Examples:

```text
HTTP 401
Authentication required

HTTP 403
Access denied

HTTP 404
Employee not found

HTTP 5xx
Server-side error
```

---

## Database

The application uses Microsoft SQL Server.

Create the database:

```sql
CREATE DATABASE EmployeeApiDb;
```

The application uses JPA/Hibernate to manage the database schema during development.

Example local configuration:

```properties
spring.datasource.url=jdbc:sqlserver://localhost:61329;databaseName=EmployeeApiDb;encrypt=true;trustServerCertificate=true
spring.datasource.username=<username>
spring.datasource.password=<password>
```

**Do not commit real database credentials to GitHub.**

For local development, credentials should be stored in local configuration or environment variables.

---

## Running the Backend

### Requirements

Install:

- JDK 21
- Maven
- SQL Server

Verify Java:

```bash
java -version
```

Verify Maven:

```bash
mvn -version
```

### Start the Backend

Open a terminal in:

```text
backend/employee-api
```

Run:

```bash
mvn spring-boot:run
```

The API will be available at:

```text
http://localhost:8080
```

---

## Swagger

Once the backend is running, open:

```text
http://localhost:8080/swagger-ui/index.html
```

Swagger can be used to:

1. Login
2. Obtain a JWT token
3. Authorize protected endpoints
4. Test employee CRUD operations

---

## Running the Windows Forms Client

Open the frontend project:

```text
frontend/EmployeeClient
```

in Visual Studio.

Make sure the Spring Boot backend is running:

```text
http://localhost:8080
```

Then run the Windows Forms application.

The client communicates with the backend using HTTP and JSON.

---

## Application Flow

```text
                    ┌───────────────┐
                    │     Login     │
                    └───────┬───────┘
                            │
                            │ JWT
                            ▼
                    ┌───────────────┐
                    │ Employee List │
                    └───────┬───────┘
                            │
              ┌─────────────┼─────────────┐
              │             │             │
              ▼             ▼             ▼
           ┌─────┐       ┌─────┐       ┌────────┐
           │ Add │       │Edit │       │ Delete │
           └──┬──┘       └──┬──┘       └───┬────┘
              │             │              │
              └─────────────┼──────────────┘
                            │
                            ▼
                    ┌───────────────┐
                    │ Refresh List  │
                    └───────┬───────┘
                            │
                            ▼
                       ┌────────┐
                       │ Logout │
                       └────┬───┘
                            │
                            ▼
                          Login
```

---

## Security

The application uses:

- Spring Security
- BCrypt password hashing
- JWT authentication
- Bearer authentication
- Stateless sessions
- Protected employee endpoints

The employee endpoints require a valid JWT:

```http
Authorization: Bearer <JWT_TOKEN>
```

The authentication endpoint is publicly accessible:

```text
POST /api/auth/login
```

---

## Security Considerations

This project is intended as a learning and portfolio project.

For production use, additional security hardening would be required.

Potential improvements include:

- Store JWT secrets in environment variables or a secret manager
- Use HTTPS
- Implement refresh tokens
- Configure appropriate token expiration
- Add role-based authorization
- Add stronger request validation
- Avoid exposing sensitive information in logs
- Configure appropriate CORS policies when required
- Use production-grade database credentials
- Implement centralized exception handling
- Add security monitoring and auditing

---

## Development Lessons

This project was also used to explore practical integration between Java and .NET technologies.

### Java / Spring Boot

- REST Controller
- Dependency Injection
- Spring Security
- JWT authentication
- Spring Data JPA
- Hibernate
- Repository pattern
- DTO
- Entity mapping
- Maven dependency management

### C# / .NET

- Windows Forms
- HttpClient
- HTTP requests
- JSON serialization/deserialization
- async/await
- Event-driven UI
- Client-side authentication state

### Database

- SQL Server
- Primary keys
- Identity columns
- JPA entity mapping
- CRUD persistence

### Integration

- REST API
- HTTP
- JSON
- Bearer authentication
- Client-server architecture

---

## What This Project Demonstrates

This project demonstrates practical understanding of:

- REST API development
- Java Spring Boot
- Spring Security
- JWT authentication
- Spring Data JPA
- Hibernate
- SQL Server
- Swagger/OpenAPI
- C# Windows Forms
- HTTP client integration
- JSON serialization/deserialization
- async/await
- CRUD operations
- Layered architecture
- Authentication and authorization concepts
- Client-server integration
- Cross-technology application development

---

## Future Improvements

Potential improvements for the next iteration:

- Dedicated Service layer
- DTO-based Employee API contracts
- Bean Validation
- Global exception handling with `@ControllerAdvice`
- Role-based authorization
- Pagination
- Sorting
- Employee search and filtering
- Department entity
- Department REST API
- Unit tests
- Integration tests
- Docker support
- Docker Compose
- CI/CD pipeline
- Externalized configuration
- Refresh-token mechanism
- Structured logging

---

## Project Purpose

This project was created as a practical exercise to demonstrate the ability to work across different technology stacks and understand common enterprise application architecture and integration patterns.

The application combines:

```text
Java Spring Boot
        +
Spring Security
        +
JWT
        +
SQL Server
        +
C# / .NET Windows Forms
```

The project focuses on building a complete end-to-end application rather than demonstrating a single isolated technology.

---

## Author

**Ma'ruf Hidayat**

Senior Software Engineer | Enterprise Software | .NET | CTRM | SQL Server

- LinkedIn: https://www.linkedin.com/in/hagemaruf
- GitHub: https://github.com/hagemaruf

---

## License

This project is intended for educational, learning, and portfolio purposes.
