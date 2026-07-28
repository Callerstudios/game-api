namespace GameApi.Services.Models;

public record LoginToken(
    string AccessToken,
    DateTime ExpiresAt);