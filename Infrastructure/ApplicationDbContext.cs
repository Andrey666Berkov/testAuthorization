using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure;

public class ApplicationDbContext : IdentityDbContext<User,Role, Guid>
{
    private readonly IConfiguration _configuration;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IConfiguration configuration) : base(options)
    {
         _configuration = configuration;
    
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql(_configuration.GetConnectionString("Database"))
            .UseCamelCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.Entity<User>().ToTable("users");

        builder.Entity<User>()
            .HasMany<Role>()
            .WithMany()
            .UsingEntity<IdentityUserRole<Guid>>();
        
        builder.Entity<Role>().ToTable("roles");
        
       /*builder.Entity<Role>()
            .HasMany<User>()
            .WithMany()
            .UsingEntity<IdentityUserRole<Guid>>();*/
        
        builder.Entity<IdentityRoleClaim<Guid>>().ToTable("role_claims");
        
        builder.Entity<IdentityUserRole<Guid>>().ToTable("user_roles");
        
        builder.Entity<IdentityUserClaim<Guid>>().ToTable("user_claims");
        
        builder.Entity<IdentityUserLogin<Guid>>().ToTable("user_logins");
        
        builder.Entity<IdentityUserToken<Guid>>().ToTable("user_tokens");
    }
    
    // "Database": "Server=server;Port=port;Database=database;User Id=userid;Password=password;",
}