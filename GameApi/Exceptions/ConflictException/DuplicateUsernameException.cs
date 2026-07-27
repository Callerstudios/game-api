using System.Net;

namespace GameApi.Exceptions.ConflictException;

public sealed class DuplicateUsernameException : ConflictException
{
    public DuplicateUsernameException(string username)
        : base(
            $"Username '{username}' already exists.",
            "DUPLICATE_USERNAME")
    {
    }
}