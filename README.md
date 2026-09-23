# Library Management System

A simple library management application built with C# and Entity Framework Core.

I built this project to practice backend development concepts such as EF Core, LINQ, repositories, services, DTOs, business rules and database relationships.

The application includes features like:

- User registration and login
- Book and category management
- Borrowing and returning books
- Late return penalties
- Reviews
- Wishlist

The project is separated into four parts:

- `LibraryManagement.Domain`
- `LibraryManagement.Application`
- `LibraryManagement.Infrastructure`
- `LibraryManagement.Presentation`

The main technologies and concepts used in this project are:

- C#
- .NET
- Entity Framework Core
- SQL Server
- LINQ
- Repository Pattern
- Service Layer
- DTOs
- Fluent API
- Entity Relationships
- Custom Exceptions

The database connection string is located in:

`LibraryManagement.Infrastructure/Data/AppDbContext.cs`

After updating the connection string, migrations can be applied with:

```powershell
Update-Database
```
Then set LibraryManagement.Presentation as the startup project and run the application.
