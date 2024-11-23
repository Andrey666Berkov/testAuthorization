using Infrastructure.Models;
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
        Database.EnsureCreated();
        
       
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("Database"));
    }
}