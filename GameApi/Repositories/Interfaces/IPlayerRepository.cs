using GameApi.Common;
using GameApi.Models;

namespace GameApi.Repositories.Interfaces;

public interface IPlayerRepository
{
    Task<PagedResult<Player>> GetAllAsync(PlayerQueryParameters query);

    Task<Player?> GetByIdAsync(Guid id);

    Task<bool> ExistsAsync(string username);

    Task<bool> UsernameExistsAsync(string username, Guid? excludePlayerId = null);

    Task<Player?> GetByEmailAsync(string email);

    Task<bool> EmailExistsAsync(string email);

    Task AddAsync(Player player);

    void Update(Player player);

    void Delete(Player player);
}