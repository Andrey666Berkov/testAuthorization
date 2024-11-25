using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shared;

namespace Application.UseCase;

public class JwtToken
{
    private readonly IConfiguration _configuration;

    public JwtToken(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateJwtToken(User user)
    {
        var claims = new List<Claim> { new Claim(ClaimTypes.Name, "person.Email") };
        // создаем JWT-токен
        var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(),
                SecurityAlgorithms.HmacSha256));
        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        return encodedJwt;
    }
    /*var jwtOptions = _configuration.GetSection(JwtOPtions.JWT).Get<JwtOPtions>();

   // var userRoleClaims=user.Roles.Select(r => new Claim(nameof(ClaimTypesMy.Role), r.Name));

    var claims = new List<Claim>()
    {
        new Claim(nameof(ClaimTypesMy.Name), user.UserName!),
        new Claim(nameof(ClaimTypesMy.Email),user.Email! )
    };
   // claims.Concat(userRoleClaims);

    var jwtTokenAccess = new JwtSecurityToken(
        issuer: "aaa",
        audience: "aaa",
        claims: claims,
        expires:DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
        signingCredentials:new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes("dsfsdfsdfsdfsd324324234234234fsdfsdfsdfsdsddfdsf")),
            SecurityAlgorithms.HmacSha256)
    );
    var token = new JwtSecurityTokenHandler().WriteToken(jwtTokenAccess);

    return token;*/
}