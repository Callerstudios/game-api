namespace GameApi.Models;

public class Player
{
    public Guid Id { get; private set; }

    public string Username { get; private set; } = string.Empty;

    public int Level { get; private set; }

    public int Experience { get; private set; }

    private Player()
    {
    }

    public Player(string username)
    {
        SetUsername(username);

        Id = Guid.NewGuid();
        Level = 1;
        Experience = 0;
    }
    private void SetUsername(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
        {
            throw new ArgumentException(
                "Username is required.",
                nameof(username));
        }

        if (username.Length > 20)
        {
            throw new ArgumentException(
                "Username cannot exceed 20 characters.",
                nameof(username));
        }

        Username = username.Trim();
    }
    public void UpdateUsername(string username)
    {
        SetUsername(username);
    }
    public void AddExperience(int amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(amount),
                "Experience must be positive.");
        }

        Experience += amount;

        while (Experience >= ExperienceRequiredForNextLevel())
        {
            Experience -= ExperienceRequiredForNextLevel();
            Level++;
        }
    }
    public void LevelUp()
    {
        Level++;
    }
    private int ExperienceRequiredForNextLevel()
    {
        return Level * 100;
    }
}