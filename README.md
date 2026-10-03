# Medical Laboratory Management System API

A RESTful API for managing patients, doctors' referrals, appointments, requested lab tests, and lab results at a medical laboratory. This is the second iteration of the MLMS domain — the first was built as an ASP.NET Core MVC application; this version was deliberately redesigned around API-first concerns (REST conventions, DTOs, JWT authentication, role-based authorization) rather than translated line-by-line from the MVC controllers.

This project is the API-based second iteration of my Medical Laboratory Management System. The first iteration was built with ASP.NET Core MVC; this version was redesigned around REST API principles, DTOs, JWT authentication, role-based authorization, and API-focused business rules.

## Features

- **Role-based access**: three roles (Admin, Receptionist, Technician), each with distinct permissions enforced through JWT-based authorization, not just UI conventions.
- **Appointment lifecycle and business rules**: an appointment moves through `Scheduled → Processing → Completed` (or `Cancelled`), but its status is never set directly: it's *derived* from the state of its requested lab tests rather than allowing arbitrary status changes.
- **Domain invariants enforced at the model level, not just the service layer**: `RequestedLabTest` exposes `Create()`, `Process()`, `Cancel()`, and `CompleteWithResult()` instead of public setters, so a result can never exist without a request, and a test can never be marked complete without one.
- **Soft-delete distinct from business status**: `IsDeleted` is kept separate from business status, allowing records such as cancelled or completed appointments to be archived without changing their actual business state.
- **Patients created only through appointment booking**: patients are created during appointment booking and an existing patient is reused when the same unique phone number is provided.
- **Business-rule violations return `409 Conflict` with a clear message**: (e.g. editing a completed appointment, processing an already-completed lab test), distinct from `404 Not Found` and validation errors.

## Tech Stack

- **Backend:** ASP.NET Core Web API (.NET 8, C# 12).
- **Data access:** Entity Framework Core, Code-First, SQL Server.
- **Authentication:** ASP.NET Core Identity, JWT Bearer authentication.
- **Authorization:** Role-based authorization.
- **Documentation & testing:** Swagger / OpenAPI with Bearer token support built into the UI.
- **Validation:** Data Annotations and server-side business-rule validation.

## Architecture Notes

- **Reusable projections**: entity-to-DTO projection expressions are defined once and reused across queries that require the same DTO shape.
- **EF Core global query filters**: soft-deleted `Appointment`, `LabTest`, and `RequestedLabTest` records are automatically excluded from normal queries. `IgnoreQueryFilters()` is used deliberately for the admin-only deleted-record views.
- **Custom exceptions per business rule**: (`AppointmentNotEditableException`, `RequestedLabTestNotProcessableException`, etc.) are thrown from the service layer and mapped to `409 Conflict` in the controller keeping `this request is malformed` (400) distinct from `this request is valid but conflicts with the resource's current state` (409).
- **DTOs are shaped per use case, not per entity**: create and update operations use dedicated DTOs rather than exposing entities directly. A PATCH request accepts a fully-optional DTO (every field nullable, no `[Required]`) so clients can update only the values they provide, while a POST/create DTO requires what it genuinely needs; the two are never the same type.

## Roles & Permissions

| Action | Admin | Receptionist | Technician |
|---|---|---|---|
| Manage laboratory test catalog | Yes | No | No |
| Create / view appointments | Yes | Yes | Yes |
| Edit / cancel appointments | Yes | Yes | Yes |
| Delete appointments (soft) | Yes | No | No |
| Process requested tests / enter results | Yes | No | Yes |
| View deleted records | Yes | No | No |
| Register new staff accounts | Yes | No | No |

## Getting Started

1. Clone the repository.
2. Update `appsettings.json` with your SQL Server connection string.
3. Configure the JWT signing key and seed administrator credentials using local development configuration or User Secrets. Do not commit real credentials or signing keys to the repository.
Example development configuration:
   ```json
   {
     "JWT": { "Key": "<your-own-signing-key>" },
     "SeedAdmin": {
       "UserName": "admin",
       "Email": "admin@example.com",
       "PhoneNumber": "...",
       "Password": "<your-own-password>"
     }
   }
   ```
4. Apply the database migrations:
   ```
   dotnet ef database update
   ```
5. Run the application:
   ```
   dotnet run
   ```

On the first run, the application automatically seeds the three roles:

- Admin
- Receptionist
- Technician

It also creates the initial administrator account using the configured seed credentials.

6. Log in via `POST /api/account/login` with the seeded admin credentials to get a JWT, then use Swagger's "Authorize" button to attach it to subsequent requests.

## Project Status

**v1.0 - API version complete**

This project represents the second stage of the Medical Laboratory Management System. The MVC version demonstrated server-rendered CRUD and EF Core fundamentals, while this API version focuses on REST API design, JWT authentication, role-based authorization, DTOs, and enforcing business rules around appointments and requested laboratory tests.
