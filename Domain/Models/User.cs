using Microsoft.AspNetCore.Identity;

namespace Domain.Models;

public class User : IdentityUser<Guid>
{
    
    private List<Role> _roles=[];
    public IReadOnlyList<Role> Roles => _roles;

    private User()
    { }
    private User(string name, string email, Role role)
    {
        base.UserName = name;
        Email = email;
        _roles.Add(role);
    }

    public static User Create(string name,string email, Role role)
    {
        return new User(name,email, role);
    }
    
}

public class Role : IdentityRole<Guid>
{
    private Role()
    { }

    public List<User> Users { get; set; }

    private Role(string nameRole)
    {
        Name = nameRole;
    }

    public static Role Create(string nameRole)=>new(nameRole);
   
}





