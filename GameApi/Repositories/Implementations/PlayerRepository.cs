using GameApi.Models;
using GameApi.Repositories.Interfaces;

namespace GameApi.Repositories.Implementations;

public class PlayerRepository : IPlayerRepository
{
    private readonly List<Player> _players = new();

    public Task<IEnumerable<Player>> GetAllAsync()
    {
        return Task.FromResult(_players.AsEnumerable());
    }

    public Task<Player?> GetByIdAsync(Guid id)
    {
        var player = _players.FirstOrDefault(p => p.Id == id);

        return Task.FromResult(player);
    }

    public Task<Player> AddAsync(Player player)
    {
        _players.Add(player);

        return Task.FromResult(player);
    }
}