using GameApi.Models;
using GameApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace GameApi.Services.Implementations;

public class PasswordHasherService : IPasswordHasher
{
    private readonly PasswordHasher<object> _passwordHasher = new();

    public string Hash(string password)
    {
        return _passwordHasher.HashPassword(null!, password);
    }

    public bool Verify(string passwordHash, string password)
    {
        var result = _passwordHasher.VerifyHashedPassword(
            null!,
            passwordHash,
            password);

        return result != PasswordVerificationResult.Failed;
    }
}