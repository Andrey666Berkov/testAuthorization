using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Account.Application;
using Account.Application.UseCase;
using Account.Domain;
using Account.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shared;

namespace Account.Infrastructure;

public class JwtTokenGenerate : IJwtTokenProvider
{
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _dbContext;
    private readonly UserManager<User> _userManager;
    private readonly JwtRefreshOptions _jwtRefreshOptions;
    private readonly JwtAccessOptions _jwtAccessOptions;

    public JwtTokenGenerate(
        IConfiguration configuration,
        ApplicationDbContext dbContext,
        UserManager<User> userManager,
        IOptions<JwtAccessOptions> jwtAccessOptions,
            IOptions<JwtRefreshOptions> jwtRefreshOptions)
    {
        _configuration = configuration;
        _dbContext = dbContext;
        _userManager = userManager;
        _jwtRefreshOptions = jwtRefreshOptions.Value;
        _jwtAccessOptions = jwtAccessOptions.Value;
    }

    public AccessJwtTokenResponse GenerateAccessToken(Guid userId)
    {
        var users = _userManager.Users;

        var user = users.FirstOrDefault(u => u.Id == userId);

        var userRoles = users.Where(u => u.Id == userId)
            .Include(r => r.Roles)
            .SelectMany(c => c.Roles)
            .ToList();

        var claimRoles = userRoles
            .Select(r => new Claim(ClaimTypesMy.Role, r.Name!))
            .ToList();

        var jti = new Guid();

        var claims = new List<Claim>()
        {
            new Claim(nameof(ClaimTypesMy.JtiId), jti.ToString()),
            new Claim(nameof(ClaimTypesMy.Name), user.UserName!),
            new Claim(nameof(ClaimTypesMy.Email), user.Email!),
            new Claim(nameof(ClaimTypesMy.UserId), user.Id.ToString())
        };

        claims.AddRange(claimRoles);

        var jwt = new JwtSecurityToken(
            issuer: _jwtAccessOptions.Issuer,
            audience: _jwtAccessOptions.Audience,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(_jwtAccessOptions.Expires)),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_jwtAccessOptions.Key)),
                SecurityAlgorithms.HmacSha256));
        var accessTokenString = new JwtSecurityTokenHandler().WriteToken(jwt);

        return new AccessJwtTokenResponse(accessTokenString, jti);
    }

    public async Task<Guid> GenerateRefreshsTokenAsync(
        User user,
        Guid jti,
        CancellationToken cancellationToken = default)
    {
        var refreshToken = Guid.NewGuid();

        var refreshSession = new RefreshSession()
        {
            UserId = user.Id,
            User = user,
            JTi = jti,
            Refresh = refreshToken,
            DateBegin = DateTime.Now,
            DateEnd = DateTime.Now.AddDays(_jwtRefreshOptions.Expires)
        };
        
        await _dbContext.RefreshTokens.AddAsync(refreshSession, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return refreshToken;
    }
}