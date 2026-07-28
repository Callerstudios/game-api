using GameApi.Common;
using GameApi.Data;
using GameApi.Models;
using GameApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GameApi.Repositories.Implementations;

public class PlayerRepository : IPlayerRepository
{
    private static readonly Dictionary<string, Expression<Func<Player, object>>> SortSelectors =
    new(StringComparer.OrdinalIgnoreCase)
    {
        ["username"] = player => player.Username,
        ["level"] = player => player.Level,
        ["experience"] = player => player.Experience
    };

    private readonly AppDbContext _context;

    public PlayerRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResult<Player>> GetAllAsync(
    PlayerQueryParameters query)
    {
        IQueryable<Player> players = _context.Players;

        if (!string.IsNullOrWhiteSpace(query.Username))
        {
            players = players.Where(player => player.Username.Contains(query.Username));
        }

        var totalCount = await players.CountAsync();

        if (!SortSelectors.TryGetValue(query.SortBy, out var selector))
        {
            selector = player => player.Username;
        }

        players = query.Descending ? players.OrderByDescending(selector) : players.OrderBy(selector);

        players = players .Skip((query.Page - 1) * query.PageSize) .Take(query.PageSize);

        var items = await players.ToListAsync();

        return new PagedResult<Player>(items, totalCount);
    }

    public async Task<Player?> GetByIdAsync(Guid id)
    {
        return await _context.Players.FindAsync(id);
    }

    public async Task<bool> ExistsAsync(string username)
    {
        return await _context.Players
            .AnyAsync(p => p.Username == username);
    }

    public async Task<bool> UsernameExistsAsync(
    string username,
    Guid? excludePlayerId = null)
    {
        return await _context.Players.AnyAsync(p =>
            p.Username == username &&
            (!excludePlayerId.HasValue || p.Id != excludePlayerId));
    }

    public async Task AddAsync(Player player)
    {
        await _context.Players.AddAsync(player);
    }

    public void Update(Player player)
    {
        _context.Players.Update(player);
    }

    public void Delete(Player player)
    {
        _context.Players.Remove(player);
    }

    public async Task<Player?> GetByEmailAsync(string email)
    {
        return await _context.Players
            .FirstOrDefaultAsync(player => player.Email == email);
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        return await _context.Players
            .AnyAsync(player => player.Email == email);
    }
}