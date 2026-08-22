

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PetShoop.CrossCutting;

public static class DependencyInjectionRateLimiter
{
    public static IServiceCollection AddInfrastructureRateLimiter(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRateLimiter(rateLimiterOptions =>
        {
            rateLimiterOptions.AddFixedWindowLimiter(policyName: "fixedwindow", options =>
            {
                options.PermitLimit = 80;  // apenas 80 requests permitidos a cada
                options.Window = TimeSpan.FromSeconds(10); // 10 segundos
                options.QueueLimit = 0;
            });


            rateLimiterOptions.AddFixedWindowLimiter(policyName: "loginRateLimit", options =>
            {
                options.PermitLimit = 3; // apenas 3 requests permitidos a cada 1 minutos
                options.Window = TimeSpan.FromMinutes(1);
                options.QueueLimit = 0;
            });

            rateLimiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
        });

        return services;
    }
}