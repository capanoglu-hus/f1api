# 🏎️ F1API – Formula 1 Management API

F1API, **ASP.NET Core Web API** kullanılarak geliştirilmiş bir RESTful servistir.  
Amaç; Formula 1 yarışları, sürücüler ve kullanıcı yönetimini JWT tabanlı authentication ve role-based authorization ile yönetmektir.

---

## 🚀 Kullanılan Teknolojiler

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- JWT Authentication
- Role-Based Authorization (Admin / User)
- FluentValidation
- OpenAPI (Swagger)
- Service & DTO Pattern

---

## 🔐 Authentication & Authorization

- JWT Bearer Token kullanılır
- Kullanıcı rolleri:
  - `User`
  - `Admin`
- Bazı endpoint’ler yalnızca **Admin** rolüne açıktır

Authorization Attribute:
```csharp
[Authorize]
[Authorize(Roles = "Admin")]



CRUD PROCESS

<img width="356" height="207" alt="Ekran görüntüsü 2025-12-01 105900" src="https://github.com/user-attachments/assets/0bebe2f5-d0ae-4a22-a044-2fa6d189e2da" />


<img width="665" height="185" alt="races" src="https://github.com/user-attachments/assets/ca1a675d-5d44-4f43-9803-9d7442ca4f48" />


<img width="632" height="236" alt="teams" src="https://github.com/user-attachments/assets/d804b745-b8f4-4f27-864e-5e4062deac9c" />


