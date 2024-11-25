using Account.Application.UseCase;
using Microsoft.AspNetCore.Authorization;

namespace Account.Infrastructure.Permission;

public class PermissionHandler : AuthorizationHandler<PermissionAttribute>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionAttribute requirement)
    {
        var userClaims = context.User.Claims
            .Where(c => c.Type == ClaimTypesMy.Role)
            .Select(c => c.Value)
            .ToList();

        if (userClaims.Any(c => c == requirement.Role))
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }
        return Task.CompletedTask;
       
    }
}