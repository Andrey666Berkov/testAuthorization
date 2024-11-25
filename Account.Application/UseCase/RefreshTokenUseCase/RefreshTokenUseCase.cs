using System.Security.Claims;
using Account.Application.UseCase.LoginUseCase;
using Account.Domain;
using Account.Domain.Models;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting.Internal;
using Shared.Error;

namespace Account.Application.UseCase.RefreshTokenUseCase;

public class RefreshTokenUseCase
{
    private readonly IPasswordValidator<User> _passwordValidator;
    private readonly IJwtTokenProvider _jwttoken;
    private readonly UserManager<User> _userManger;
    private readonly RoleManager<Role> _roleManger;
    private readonly ITokenManager _tokenManager;

    public RefreshTokenUseCase(
        IPasswordValidator<User> passwordValidator,
        IJwtTokenProvider jwttoken,
        UserManager<User> userManger,
        RoleManager<Role> roleManger,
        ITokenManager tokenManager)
    {
        _passwordValidator = passwordValidator;
        _jwttoken = jwttoken;
        _userManger = userManger;
        _roleManger = roleManger;
        _tokenManager = tokenManager;
    }
    public async Task<Result<jwtTokensHandleResponse ,ErrorMy>> Handle(
        RefreshTokenCommand command,
        CancellationToken cancellationToken)
    {
        var oldRefreshSession = await _tokenManager.GetAccessToken(command.refreshToken);
        if(oldRefreshSession.IsFailure)
            return oldRefreshSession.Error;

        
        var userClaims =await _tokenManager.GetUserClaims(command.accessToken);
        if (userClaims.IsFailure)
            return userClaims.Error;

        var userId = userClaims.Value.FirstOrDefault(c => c.Type == ClaimTypesMy.JtiId).Value;

        if (userId != oldRefreshSession.Value.UserId.ToString())
            return ErrorsMy.General.Conflict("User");
        
        var jtiSession=userClaims.Value.FirstOrDefault(c => c.Type == ClaimTypesMy.JtiId).Value;
        
        if(jtiSession != oldRefreshSession.Value.JTi.ToString())
            return ErrorsMy.General.Conflict("Jti");

        var delResult = await _tokenManager.DeletSession(oldRefreshSession.Value, cancellationToken);

           
        
        if (Guid.TryParse(userId, out Guid userIdGuid)==false)
        {
            return ErrorsMy.General.Conflict("UserId");
        }
        
        var newAccessToken = _jwttoken.GenerateAccessToken(userIdGuid);
        var user=await _userManger.FindByIdAsync(userId);
        if(user==null)
            return ErrorsMy.General.NotFound("User");


        var newRefeshToken = _jwttoken.GenerateRefreshsTokenAsync(user, newAccessToken.Jti, cancellationToken)
            .Result;

        return new jwtTokensHandleResponse(newAccessToken.AccessToken, newRefeshToken);
    }
}

public interface ITokenManager
{
    Task<Result<RefreshSession,ErrorMy>> GetAccessToken(Guid jwtId);
    Task<Result<List<Claim>,ErrorMy>> GetUserClaims(string accessToken);
    
    Task<Result<bool>> DeletSession(RefreshSession refreshSession, CancellationToken cancellationToken);
}