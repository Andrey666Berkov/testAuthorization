using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddDInfrastructureDependency(
        this IServiceCollection services,
        IConfiguration configuration)
    {
       
        
        return services;
    }
}