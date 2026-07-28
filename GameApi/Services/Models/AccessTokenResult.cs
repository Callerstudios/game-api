namespace GameApi.Services.Models;

public record AccessTokenResult(
    string AccessToken,
    DateTime ExpiresAt);