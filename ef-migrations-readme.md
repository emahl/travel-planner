https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/applying?tabs=dotnet-core-cli

**Prerequisites:**
VS > Package Manager Console > Default project: src\Infrastructure

**Create new migration:**
dotnet ef migrations add <migration_name> --project .\Infrastructure\Infrastructure.csproj --startup-project .\WebApp\WebApp.csproj -o .\Persistence\Migrations

**Run migrations against local db:**
dotnet ef database update --project .\Infrastructure\Infrastructure.csproj --startup-project .\WebApp\WebApp.csproj

**Generate SQL scripts to run against prod db:** 
dotnet ef migrations script --idempotent --project .\Infrastructure\Infrastructure.csproj --startup-project .\WebApp\WebApp.csproj