namespace GameApi.DTOs.Players;

public class PlayerDto
{
    public Guid Id { get; set; }

    public string Username { get; set; } = string.Empty;

    public int Level { get; set; }
}
