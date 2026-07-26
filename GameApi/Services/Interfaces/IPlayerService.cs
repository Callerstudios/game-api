using GameApi.DTOs.Players;

namespace GameApi.Services.Interfaces;

public interface IPlayerService
{
    Task<IEnumerable<PlayerDto>> GetAllAsync();

    Task<PlayerDto> CreateAsync(CreatePlayerDto dto);
}