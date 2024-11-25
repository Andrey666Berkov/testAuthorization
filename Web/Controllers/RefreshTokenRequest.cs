namespace Web.Controllers;

public record RefreshTokenRequest(string AccessToken, Guid RefreshToken);