namespace GameApi.Models;

public class Player
{
    public Guid Id { get; set; }

    public string Username { get; set; } = string.Empty;

    public int Level { get; set; }
}
