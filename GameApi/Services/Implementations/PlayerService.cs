using GameApi.DTOs.Players;
using GameApi.Mappings;
using GameApi.Models;
using GameApi.Repositories.Interfaces;
using GameApi.Services.Interfaces;
using GameApi.Exceptions;

namespace GameApi.Services.Implementations;

public class PlayerService : IPlayerService
{
    private readonly IPlayerRepository _playerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PlayerService(
        IPlayerRepository playerRepository,
        IUnitOfWork unitOfWork)
    {
        _playerRepository = playerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<PlayerDto>> GetAllAsync()
    {
        var players = await _playerRepository.GetAllAsync();

        return players.Select(PlayerMappings.ToDto);
    }

    public async Task<PlayerDto> CreateAsync(CreatePlayerDto dto)
    {
        if (await _playerRepository.ExistsAsync(dto.Username))
        {
            throw new DuplicateUsernameException(dto.Username);
        }

        var player = new Player(dto.Username);

        await _playerRepository.AddAsync(player);

        //return PlayerMappings.ToDto(player);
        return player.ToDto();
    }

    public async Task<PlayerDto?> GetByIdAsync(Guid id)
    {
        var player = await _playerRepository.GetByIdAsync(id);

        if (player is null)
        {
            return null;
        }

        return player.ToDto();
    }

    //private static PlayerDto MapToDto(Player player)
    //{
    //    return new PlayerDto
    //    {
    //        Id = player.Id,
    //        Username = player.Username,
    //        Level = player.Level
    //    };
    //}
}
