namespace GameApi.Models;

public class Player
{
    public Guid Id { get; private set; }

    public string Username { get; private set; } = "";

    public int Level { get; private set; }

    public Player(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
        {
            throw new ArgumentException("Username is required.");
        }

        Id = Guid.NewGuid();
        Username = username;
        Level = 1;
    }
}