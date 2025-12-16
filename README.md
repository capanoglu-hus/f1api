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
Authorize
Authorize(Roles = "Admin")



<img width="665" height="185" alt="races" src="https://github.com/user-attachments/assets/f7d885c6-a359-4a55-8d5c-451b1335bb5c" />



<img width="632" height="236" alt="teams" src="https://github.com/user-attachments/assets/1ba418ea-43e2-4c68-a697-8ce52c1dcef3" />

<img width="356" height="207" alt="Ekran görüntüsü 2025-12-01 105900" src="https://github.com/user-attachments/assets/7be71c48-87aa-4f6c-b1d9-69812d9905a7" />


