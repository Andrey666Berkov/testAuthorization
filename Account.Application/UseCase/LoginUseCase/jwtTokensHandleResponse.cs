namespace Account.Application.UseCase.LoginUseCase;

public record jwtTokensHandleResponse(string AccessToken, Guid RefreshToken );