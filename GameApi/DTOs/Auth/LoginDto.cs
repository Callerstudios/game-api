namespace GameApi.DTOs.Auth;

public record LoginDto(
    string Email,
    string Password
);