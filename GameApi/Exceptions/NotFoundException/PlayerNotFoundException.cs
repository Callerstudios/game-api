using System.Net;

namespace GameApi.Exceptions.NotFoundException;

public sealed class PlayerNotFoundException : NotFoundException
{
    public PlayerNotFoundException(Guid id)
        : base(
            $"Player '{id}' was not found.",
            "PLAYER_NOT_FOUND")
    {
    }
}