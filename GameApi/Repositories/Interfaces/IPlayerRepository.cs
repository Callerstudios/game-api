using GameApi.Models;

namespace GameApi.Repositories.Interfaces;

public interface IPlayerRepository
{
    Task<IEnumerable<Player>> GetAllAsync();

    Task<Player?> GetByIdAsync(Guid id);

    Task<Player> AddAsync(Player player);
}
