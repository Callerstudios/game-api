using GameApi.Data;
using GameApi.Models;
using GameApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameApi.Repositories.Implementations;

public class PlayerRepository : IPlayerRepository
{
    private readonly AppDbContext _context;

    public PlayerRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Player>> GetAllAsync()
    {
        return await _context.Players
            .AsNoTracking()
            .ToListAsync();
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
}