# HrSystem

A .NET solution with clean layering:

- HrSystem.Domain: Domain models and core logic
- HrSystem.Application: Application services/use-cases; depends on Domain
- HrSystem.Infrastructure: Persistence and external services; depends on Application and Domain
- HrSystem.Api: ASP.NET Core Web API; depends on Application and Infrastructure

## Build

```powershell
# from repo root
 dotnet build HrSystem.sln -v minimal
```

## Run the API

```powershell
# from repo root
 dotnet run --project src/HrSystem.Api/HrSystem.Api.csproj
```

The API will start on the port shown in the console. Swagger is enabled by default in Development.

## Project references

- Application → Domain
- Infrastructure → Application, Domain
- Api → Application, Infrastructure

## Structure

```
HrSystem.sln
src/
  HrSystem.Domain/
  HrSystem.Application/
  HrSystem.Infrastructure/
  HrSystem.Api/
```
