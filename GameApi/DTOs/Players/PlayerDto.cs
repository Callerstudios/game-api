namespace GameApi.DTOs.Players;

public record PlayerDto(
    Guid Id,
    string Username,
    int Level
);
