using Microsoft.AspNetCore.Authorization;

namespace Account.Infrastructure.Permission;

public class PermissionAttribute :AuthorizeAttribute, IAuthorizationRequirement
{
    public string Role { get; }

    public PermissionAttribute(string role) :  base(policy:role)
    {
        Role = role;
    }
}