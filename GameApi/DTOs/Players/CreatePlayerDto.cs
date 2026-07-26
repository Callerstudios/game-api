using System.ComponentModel.DataAnnotations;

namespace GameApi.DTOs.Players;

public record CreatePlayerDto(
    [Required]
    [MinLength(3)]
    [MaxLength(20)]
    string Username
);
