using CSharpFunctionalExtensions;
using Account.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Shared.Error;

namespace Account.Application.UseCase.LoginUseCase;

public class LoginUseCase
{
    private readonly IPasswordValidator<User> _passwordValidator;
    private readonly IJwtTokenProvider _jwttoken;
    private readonly UserManager<User> _userManger;
    private readonly RoleManager<Role> _roleManger;

    public LoginUseCase(
        IPasswordValidator<User> passwordValidator,
        IJwtTokenProvider jwttoken,
        UserManager<User> userManger,
        RoleManager<Role> roleManger)
    {
        _passwordValidator = passwordValidator;
        _jwttoken = jwttoken;
        _userManger = userManger;
        _roleManger = roleManger;
    }
    public async Task<Result<jwtTokensHandleResponse ,ErrorMy>> Handle(
        LoginCommand command,
        CancellationToken cancellationToken)
    {
        var user = await _userManger.FindByEmailAsync(command.Email);
        if (user == null)
            return ErrorsMy.General.NotFound(command.Email);
        
        var passValid=await _passwordValidator.ValidateAsync(_userManger,user, command.Password);
        if(!passValid.Succeeded)
            ErrorsMy.General.ValueValidation("password");

        var accessTokenJwtResponse = _jwttoken.GenerateAccessToken(user.Id);
        var refreshToken = await _jwttoken.GenerateRefreshsTokenAsync(
            user,
            accessTokenJwtResponse.Jti,
            cancellationToken);
        
        return new jwtTokensHandleResponse(accessTokenJwtResponse.AccessToken, refreshToken);
    }
}