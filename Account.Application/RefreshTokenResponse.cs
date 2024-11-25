namespace Account.Infrastructure;

public record RefreshTokenResponse(string RefreskTokenString, Guid RefreshId);