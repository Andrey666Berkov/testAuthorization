namespace Account.Application;

public record AccessJwtTokenResponse(string AccessToken, Guid Jti);