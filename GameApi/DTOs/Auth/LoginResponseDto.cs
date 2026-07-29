namespace GameApi.DTOs.Auth;

public record LoginResponseDto(
    string AccessToken,
    DateTime ExpiresAt,
    AuthenticatedUserDto User
);