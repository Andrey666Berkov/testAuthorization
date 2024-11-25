using CSharpFunctionalExtensions;
using Account.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Shared.Error;

namespace Account.Application.UseCase.RegistrationUseCase;

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
    public async Task<Result<Guid ,ErrorMy>> Handle(RegistrationCommand registrationCommand)
    {
        var userEmail =await _userManger.FindByEmailAsync(registrationCommand.Email);
        if (userEmail != null)
            return ErrorsMy.General.Conflict("Email");
        
        var role = await _roleManger.FindByNameAsync("userRole");
        
        if (role == null)
        {
            await _roleManger.CreateAsync(Role.Create("userRole"));
            role = Role.Create("userRole");
        }
        
        var user = User.Create(registrationCommand.Name ,registrationCommand.Email, role);
        
        
      var userCreateResult=await _userManger.CreateAsync(user, registrationCommand.Password);
      if(!userCreateResult.Succeeded)
          return ErrorsMy.General.Failure("UserCreateResult");
      
      return user.Id;
    }
}