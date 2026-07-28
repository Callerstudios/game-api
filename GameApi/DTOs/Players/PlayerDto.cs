namespace GameApi.DTOs.Players;

public record PlayerDto(
    Guid Id,
    string Username,
    string Email,
    int Level,
    int Experience
);
