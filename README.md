# Employee Management System

A full-stack Employee Management application built to demonstrate integration between a **Java Spring Boot REST API** and a **C# Windows Forms client**.

The project implements authentication, JWT-based API security, employee CRUD operations, SQL Server persistence, and Swagger/OpenAPI documentation.

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
│  JWT Authentication         │
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

# Project Structure
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

# Features
Authentication
Login using username and password
Password stored using BCrypt
JWT token generation
JWT Bearer authentication
Stateless API authentication
Logout from the Windows Forms application
Employee Management
View employee list
Get employee by ID
Create employee
Update employee
Delete employee
Confirmation before deletion
Automatic refresh after create/update/delete
