using GameApi.Common;
using GameApi.DTOs.Players;
using GameApi.Models;

namespace GameApi.Services.Interfaces;

public interface IPlayerService
{
    Task<PagedResponse<PlayerDto>> GetAllAsync(PlayerQueryParameters query);

    //Task<PlayerDto> CreateAsync(CreatePlayerDto dto);

    Task<PlayerDto> UpdateAsync(Guid id, UpdatePlayerDto dto);

    Task<PlayerDto?> GetByIdAsync(Guid id);

    Task DeleteAsync(Guid id);
}