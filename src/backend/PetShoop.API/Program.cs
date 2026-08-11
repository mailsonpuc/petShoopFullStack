using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;
using PetShoop.CrossCutting;
using PetShoop.CrossCutting.IoC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Dependency Injection
builder.Services.AddInfrastructureAPI(builder.Configuration);

// JWT
builder.Services.AddJwtConfiguration(builder.Configuration);

// Swagger
builder.Services.AddInfrastructureSwagger(builder.Configuration);

//  CORS
builder.Services.AddInfrastructureCors(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();

    app.UseSwaggerUi(options =>
    {
        options.Path = "";
    });
}


app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
