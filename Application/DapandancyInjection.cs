using Application.UseCase;
using Application.UseCase.LoginUseCase;
using Application.UseCase.RegistrationUseCase;
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
        services.AddScoped<LoginUseCase>();
        services.AddScoped<JwtToken>();
        
        return services;
    }
}