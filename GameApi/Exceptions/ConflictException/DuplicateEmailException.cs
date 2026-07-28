namespace GameApi.Exceptions.ConflictException;

public sealed class DuplicateEmailException : ConflictException
{
    public DuplicateEmailException(string email)
        : base(
            $"Email '{email}' already exists.",
            "DUPLICATE_EMAIL")
    {
    }
}
