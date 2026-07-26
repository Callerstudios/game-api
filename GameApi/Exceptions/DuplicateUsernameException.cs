namespace GameApi.Exceptions;

public sealed class DuplicateUsernameException : Exception
{
    public DuplicateUsernameException(string username)
        : base($"Username '{username}' already exists.")
    {
    }
}