using Account.Application.UseCase;
using Account.Application.UseCase.LoginUseCase;
using Account.Application.UseCase.RefreshTokenUseCase;
using Account.Application.UseCase.RegistrationUseCase;
using Account.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Account.Application;

public static class DapandancyInjection
{
    public static IServiceCollection AddApplicationDependency(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<RegistrationUseCase>();
        services.AddScoped<LoginUseCase>();
        services.AddScoped<RefreshTokenUseCase>();
        
        return services;
    }
}