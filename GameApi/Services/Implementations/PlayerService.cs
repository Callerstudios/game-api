using GameApi.DTOs.Players;
using GameApi.Models;
using GameApi.Repositories.Interfaces;
using GameApi.Services.Interfaces;

namespace GameApi.Services.Implementations;

public class PlayerService : IPlayerService
{
    private readonly IPlayerRepository _playerRepository;

    public PlayerService(IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }

    public async Task<IEnumerable<PlayerDto>> GetAllAsync()
    {
        var players = await _playerRepository.GetAllAsync();

        return players.Select(MapToDto);
    }

    public async Task<PlayerDto> CreateAsync(CreatePlayerDto dto)
    {
        var player = new Player
        {
            Id = Guid.NewGuid(),
            Username = dto.Username,
            Level = 1
        };

        await _playerRepository.AddAsync(player);

        return MapToDto(player);
    }

    private static PlayerDto MapToDto(Player player)
    {
        return new PlayerDto
        {
            Id = player.Id,
            Username = player.Username,
            Level = player.Level
        };
    }
}