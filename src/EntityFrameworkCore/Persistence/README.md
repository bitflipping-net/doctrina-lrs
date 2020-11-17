# Persistence Layer

## Migrations

1. Create migration
```
dotnet ef migrations add <MigrationName> --context DoctrinaDbContext --output-dir Persistence/Migrations
```

2. Update database with migration(s)
```
dotnet ef database update --context DoctrinaDbContext
```
