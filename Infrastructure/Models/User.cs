using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Infrastructure.Models;

public class User : IdentityUser<Guid>
{
    
}

public class Role : IdentityRole<Guid>
{
    
}

