using Microsoft.Extensions.DependencyInjection;

namespace PetShoop.CrossCutting;

public static class DependencyInjectionMemoryCache
{
    public static IServiceCollection AddInfrastructureMemoryCache(
        this IServiceCollection services)
    {
        services.AddMemoryCache();

        return services;
    }
}
