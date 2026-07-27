using GameApi.Models;

namespace GameApi.Repositories.Interfaces;

public interface IPlayerRepository
{
    Task<IEnumerable<Player>> GetAllAsync();

    Task<Player?> GetByIdAsync(Guid id);

    Task<bool> ExistsAsync(string username);

    Task<bool> UsernameExistsAsync(
    string username,
    Guid? excludePlayerId = null);

    Task AddAsync(Player player);

    void Update(Player player);

    void Delete(Player player);
}