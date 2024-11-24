using Microsoft.AspNetCore.Identity;


namespace Infrastructure.Models;

public class User : IdentityUser<Guid>
{

}

public class Role : IdentityRole<Guid>
{
    
}

