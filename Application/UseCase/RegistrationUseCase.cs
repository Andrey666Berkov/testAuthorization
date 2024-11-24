using CSharpFunctionalExtensions;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Shared.Error;

namespace Application.UseCase;

public class RegistrationUseCase
{
    private readonly UserManager<User> _userManger;
    private readonly RoleManager<Role> _roleManger;

    public RegistrationUseCase(
        UserManager<User> userManger,
        RoleManager<Role> roleManger)
    {
        _userManger = userManger;
        _roleManger = roleManger;
    }
    public async Task<Result<User ,ErrorMy>> Handle(RegistrationCommand registrationCommand)
    {
        var userResult = _userManger.FindByEmailAsync(registrationCommand.Email);
        if (userResult.IsCompleted)
            return ErrorsMy.General.Conflict("Email");
        
        var role = await _roleManger.FindByNameAsync("userRole");
        
        if (role == null)
        {
            await _roleManger.CreateAsync(Role.Create("userRole"));
            role = Role.Create("userRole");
        }
        
        var user = User.Create(registrationCommand.Email, role);
        
      await _userManger.CreateAsync(user, registrationCommand.Password);
      return user;
    }
}