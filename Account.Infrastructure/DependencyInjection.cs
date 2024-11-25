using Account.Application.UseCase;
using Account.Application.UseCase.LoginUseCase;
using Account.Domain.Models;
using Account.Infrastructure.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared;

namespace Account.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddDInfrastructureDependency(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddTransient<IJwtTokenProvider,JwtTokenGenerate>();
        services.AddDbContext<ApplicationDbContext>();
        
        services.
            Configure<JwtAccessOptions>(configuration
                .GetSection(JwtAccessOptions.JWT));
        
        services.
            Configure<JwtRefreshOptions>(configuration
                .GetSection(JwtRefreshOptions.JwtRefresh));
        
        services
            .AddIdentity<User, Role>(op => { op.User.RequireUniqueEmail = true; })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        
        return services;
    }
}