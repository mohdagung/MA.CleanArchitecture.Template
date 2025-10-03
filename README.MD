# MA.Clean.Template

🚀 A Clean Architecture template for **.NET 8** featuring **CQRS + MediatR**, a rich **Result<T> wrapper**, centralized **BaseController**, **RepositoryBase**, **EntityBase**, and a well-structured solution layout for building new services.

## Solution Structure
```
MA.Clean.Template.sln
src/
  MA.Clean.Template.Domain/           # Domain entities, enums, value objects
  MA.Clean.Template.Application/      # CQRS (Commands/Queries + Handlers), Abstractions
  MA.Clean.Template.Infrastructure/   # Repository base, EF Core, persistence
  MA.Clean.Template.Api/              # ASP.NET Core Web API (BaseController, versioning, middleware)
  MA.Clean.Template.Worker/           # Background service (optional)
  MA.Clean.Template.Shared/           # Result<T>, constants, exceptions, common helpers

tests/
  MA.Clean.Template.Tests/            # xUnit + FluentAssertions
```

## Features
- ✅ .NET 8
- ✅ Clean Architecture layering
- ✅ CQRS with **MediatR**
- ✅ `Result<T>` wrapper (rich errors + static factory methods)
- ✅ Centralized BaseController with **API versioning**
- ✅ RepositoryBase + custom repositories per entity
- ✅ Abstract EntityBase (Id, audit fields)
- ✅ Middleware for error handling & logging
- ✅ Unit test samples using **xUnit** + **FluentAssertions**
- ✅ Packaged as a **dotnet new** template

## Getting Started

### 1. Install the Template

```powershell
git clone https://github.com/mohdagung/MA.CleanArchitecture.Template
dotnet new install .\MA.CleanArchitecture.Template
```

### 2. Create a New Service
```powershell
dotnet new ma-clean -n MA.MyService
cd MA.MyService
dotnet build
```

### 3. Run the API
```powershell
dotnet run --project src/MA.MyService.Api
```
Swagger UI available at:
```
https://localhost:<port>/swagger
```

### 4. Run the Worker
```powershell
dotnet run --project src/MA.MyService.Worker
```

### 5. Run Tests
```powershell
dotnet test
```

## Updating the Template
If you update the repo, users need to reinstall the template to get the latest:
```powershell
dotnet new uninstall .\MA.CleanArchitecture.Template
dotnet new install   .\MA.CleanArchitecture.Template
```

Or if they installed from ZIP:
```powershell
dotnet new uninstall .\MA.CleanArchitecture.Template-main
dotnet new install   .\MA.CleanArchitecture.Template-main
```

---

✍️ Author: **MA**  
🛠 Built with [.NET 8](https://dotnet.microsoft.com/)
