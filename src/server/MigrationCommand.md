# Migrations là một công cụ giúp quản lý các thay đổi trong cấu trúc cơ sở dữ liệu (Database Schema) theo sự thay đổi của mô hình dữ liệu (Model) trong ứng dụng. Nó giúp đồng bộ hóa giữa code-first models và database schema một cách dễ dàng.

## 1. Using the CLI

### Add a migration
```bash
dotnet ef migrations add FixMovieModel --project MovieTheater.Data --startup-project MovieTheater.API --context MovieTheaterDbContext --output-dir Migrations
dotnet ef migrations add [MigrationName] --project MovieTheater.Data --startup-project MovieTheater.API --context StorageDbContext --output-dir Migrations/Storage
```

### Update the database
```bash
dotnet ef database update --project MovieTheater.Data --startup-project MovieTheater.API --context MovieTheaterDbContext
dotnet ef database update --project MovieTheater.Data --startup-project MovieTheater.API --context StorageDbContext
```

### Roll Back a migration
```bash
dotnet ef database update InitialCommonAndSecuritySchemaAndConfig --project MovieTheater.Data --startup-project MovieTheater.API --context MovieTheaterDbContext
```

### Drop the database
```bash
dotnet ef database drop --project MovieTheater.API --startup-project MovieTheater.API --context MovieTheaterDbContext
dotnet ef database drop --project MovieTheater.Data --startup-project MovieTheater.API --context StorageDbContext
```

### Remove a migration
```bash
dotnet ef migrations remove --project MovieTheater.Data --startup-project MovieTheater.API --context MovieTheaterDbContext
dotnet ef migrations remove --project MovieTheater.Data --startup-project MovieTheater.API --context StorageDbContext
```

## 2. Using the Package Manager Console
### Add a migration
```bash
Add-Migration [MigrationName] -Project MovieTheater.Data -StartupProject MovieTheater.API -Context MovieTheaterDbContext -OutputDir MovieTheater.Data/Migrations
```

### Update the database
```bash
Update-Database -Project MovieTheater.Data -StartupProject MovieTheater.API -Context MovieTheaterDbContext
```

### Roll back a migration
```bash
dotnet ef database update [MigrationName] --project QuizApp.Data --startup-project QuizApp.WebAPI --context QuizAppDbContext
```

### Remove a migration
```bash
Remove-Migration -Project MovieTheater.Data -StartupProject MovieTheater.API -Context MovieTheaterDbContext
```

[]: # Path: README.md
