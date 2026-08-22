using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PetShoop.CrossCutting;

public static class DependencyInjectionCors
{
    public static IServiceCollection AddInfrastructureCors(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowFrontend", policy =>
            {

                policy
                    .WithOrigins(
                        "http://localhost:5100",                  //back de launchSettings.json
                        "https://localhost:7081",                 //back launchSettings.json
                        "http://localhost:5100",                  //back launchSettings.json
                        "http://127.0.0.1:5000",                  //back  de produçao no localhost
                        "http://localhost:5173",                  //front de produçao no localhost
                        "https://pet-shoop-full-stack.vercel.app" //front de produçao
                    )
                    .AllowAnyMethod()
                    .AllowAnyHeader();

                /*
                  policy
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                     .AllowAnyHeader();
                 */
            });
        });

        return services;
    }
}