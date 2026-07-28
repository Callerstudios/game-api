namespace GameApi.Models;

public class Player
{
    public Guid Id { get; private set; }

    public string Username { get; private set; } = string.Empty;

    public string Email { get; private set; } = string.Empty;

    public string PasswordHash { get; private set; } = string.Empty;

    public int Level { get; private set; }

    public int Experience { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime UpdatedAt { get; private set; }

    private Player() { } // Required by EF Core

    public Player(
        string username,
        string email,
        string passwordHash)
    {
        SetUsername(username);
        SetEmail(email);

        PasswordHash = passwordHash;

        Level = 1;
        Experience = 0;

        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateUsername(string username)
    {
        SetUsername(username);
        Touch();
    }

    public void UpdateEmail(string email)
    {
        SetEmail(email);
        Touch();
    }

    public void UpdatePasswordHash(string passwordHash)
    {
        PasswordHash = passwordHash;
        Touch();
    }

    public void AddExperience(int amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Experience must be positive.");
        }

        Experience += amount;

        Level = (Experience / 1000) + 1;

        Touch();
    }

    private void SetUsername(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
        {
            throw new ArgumentException("Username is required.");
        }

        Username = username.Trim();
    }

    private void SetEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("Email is required.");
        }

        Email = email.Trim().ToLowerInvariant();
    }

    private void Touch()
    {
        UpdatedAt = DateTime.UtcNow;
    }
}