namespace GameApi.DTOs.Auth;

public record AuthenticatedUserDto(
    Guid Id,
    string Username,
    string Email
);