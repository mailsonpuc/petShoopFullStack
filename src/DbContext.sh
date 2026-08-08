Create Migrations
dotnet ef migrations add Inicial --project PetShoop.Infrastructure --startup-project PetShoop.API --context AppDbContext

Apply Migratioins
dotnet ef database update --project PetShoop.Infrastructure --startup-project PetShoop.API --context AppDbContext

run api
dotnet run  --project  PetShoop.API
