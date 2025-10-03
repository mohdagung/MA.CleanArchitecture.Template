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

### Install the Template
Install directly from GitHub (default branch = `main`):
```powershell
dotnet new install https://github.com/<org>/<repo>
```
If the template lives in a subfolder:
```powershell
dotnet new install https://github.com/<org>/<repo>::MA.Clean.Template::main
```

### Create a New Service
```powershell
dotnet new ma-clean -n MA.MyService
cd MA.MyService
dotnet build
```

### Run the API
```powershell
dotnet run --project src/MA.MyService.Api
```
Swagger UI available at:
```
https://localhost:<port>/swagger
```

### Run the Worker
```powershell
dotnet run --project src/MA.MyService.Worker
```

### Run Tests
```powershell
dotnet test
```

## Updating the Template
If new changes are pushed to the repo, reinstall the template to get the latest version:
```powershell
dotnet new uninstall https://github.com/<org>/<repo>
dotnet new install   https://github.com/<org>/<repo>
```

## Distribution Options
- **GitHub (default)**: Users install directly from repo / branch / subfolder.  
- **NuGet (recommended for updates)**: Publish to NuGet.org so consumers can update easily:
```powershell
dotnet new --update-check
dotnet new --update-apply
```

---

✍️ Author: **MA**  
🛠 Built with [.NET 8](https://dotnet.microsoft.com/)
