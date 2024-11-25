using Account.Domain.Models;
using Account.Infrastructure;

namespace Account.Application.UseCase;

public interface IJwtTokenProvider
{
    AccessJwtTokenResponse GenerateAccessToken(Guid userId);

    Task<Guid> GenerateRefreshsTokenAsync(
        User user,
        Guid refreshId,
        CancellationToken cancellationToken = default);
}