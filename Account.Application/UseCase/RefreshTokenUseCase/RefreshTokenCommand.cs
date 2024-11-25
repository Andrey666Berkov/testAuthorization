namespace Account.Application.UseCase.RefreshTokenUseCase;

public record RefreshTokenCommand(string accessToken, Guid refreshToken);