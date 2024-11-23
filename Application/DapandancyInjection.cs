using Application.UseCase;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DapandancyInjection
{
    public static IServiceCollection AddApplicationDependency(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<RegistrationUseCase>();
        
        return services;
    }
}