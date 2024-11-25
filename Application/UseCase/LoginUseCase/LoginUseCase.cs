using CSharpFunctionalExtensions;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Shared.Error;

namespace Application.UseCase.LoginUseCase;

public class LoginUseCase
{
    private readonly IPasswordValidator<User> _passwordValidator;
    private readonly JwtToken _jwttoken;
    private readonly UserManager<User> _userManger;
    private readonly RoleManager<Role> _roleManger;

    public LoginUseCase(
        IPasswordValidator<User> passwordValidator,
        JwtToken jwttoken,
        UserManager<User> userManger,
        RoleManager<Role> roleManger)
    {
        _passwordValidator = passwordValidator;
        _jwttoken = jwttoken;
        _userManger = userManger;
        _roleManger = roleManger;
    }
    public async Task<Result<string ,ErrorMy>> Handle(
        LoginCommand command)
    {
        var user = await _userManger.FindByEmailAsync(command.Email);
        if (user == null)
            return ErrorsMy.General.NotFound(command.Email);
        
        var passValid=await _passwordValidator.ValidateAsync(_userManger,user, command.Password);
        if(!passValid.Succeeded)
            ErrorsMy.General.ValueValidation("password");

        var jwtToken = _jwttoken.GenerateJwtToken(user);
        
        return jwtToken;
    }
}