using PetShoop.CrossCutting.IoC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddInfrastructureAPI(builder.Configuration);
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    System.Console.WriteLine("Ambiente de desenvolvimento");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
