using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;
using PetShoop.API.Middleware;
using PetShoop.CrossCutting;
using PetShoop.CrossCutting.IoC;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Dependency Injection
builder.Services.AddInfrastructureAPI(builder.Configuration);

// JWT
builder.Services.AddJwtConfiguration(builder.Configuration);

// Swagger
builder.Services.AddInfrastructureSwagger(builder.Configuration);

// Rate Limiter
builder.Services.AddInfrastructureRateLimiter(builder.Configuration);

// CORS
builder.Services.AddInfrastructureCors(builder.Configuration);

// Memory Cache
builder.Services.AddInfrastructureMemoryCache();


var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();


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

app.UseHttpMetrics();

app.UseCors("AllowFrontend");

app.UseRateLimiter();

app.UseAuthentication();
app.UseAuthorization();

app.MapMetrics("/metrics");
app.MapHealthChecks("/health");

app.MapControllers();

app.Run();
