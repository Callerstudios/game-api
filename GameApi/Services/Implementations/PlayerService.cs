using FluentValidation;
using GameApi.Common;
using GameApi.DTOs.Players;
using GameApi.Exceptions.ConflictException;
using GameApi.Exceptions.NotFoundException;
using GameApi.Mappings;
using GameApi.Models;
using GameApi.Repositories.Interfaces;
using GameApi.Services.Interfaces;

namespace GameApi.Services.Implementations;

public class PlayerService : IPlayerService
{
    private readonly IPlayerRepository _playerRepository;
    private readonly IUnitOfWork _unitOfWork;

    //private readonly IValidator<CreatePlayerDto> _createValidator;
    private readonly IValidator<UpdatePlayerDto> _updateValidator;

    public PlayerService(
        IPlayerRepository repository,
        IUnitOfWork unitOfWork,
        //IValidator<CreatePlayerDto> createValidator,
        IValidator<UpdatePlayerDto> updateValidator)
    {
        _playerRepository = repository;
        _unitOfWork = unitOfWork;
        //_createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    public async Task<PagedResponse<PlayerDto>> GetAllAsync(
    PlayerQueryParameters query)
    {
        var result = await _playerRepository.GetAllAsync(query);

        return new PagedResponse<PlayerDto>([..result.Items.Select(player => player.ToDto())], query.Page, query.PageSize, result.TotalCount);
    }

    //public async Task<PlayerDto> CreateAsync(CreatePlayerDto dto)
    //{
    //    await _createValidator.ValidateAndThrowAsync(dto);

    //    if (await _playerRepository.ExistsAsync(dto.Username))
    //    {
    //        throw new DuplicateUsernameException(dto.Username);
    //    }

    //    var player = new Player(dto.Username, dto.Email, string.Empty);

    //    await _playerRepository.AddAsync(player);

    //    await _unitOfWork.SaveChangesAsync();

    //    return player.ToDto();
    //}
    public async Task<PlayerDto> UpdateAsync(
    Guid id,
    UpdatePlayerDto dto)
    {
        await _updateValidator.ValidateAndThrowAsync(dto);

        var player = await _playerRepository.GetByIdAsync(id);

        if (player is null)
        {
            throw new PlayerNotFoundException(id);
        }

        if (await _playerRepository.UsernameExistsAsync(dto.Username, id))
        {
            throw new DuplicateUsernameException(dto.Username);
        }

        player.UpdateUsername(dto.Username);

        await _unitOfWork.SaveChangesAsync();

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
    public async Task DeleteAsync(Guid id)
    {
        var player = await _playerRepository.GetByIdAsync(id);

        if (player is null)
        {
            throw new PlayerNotFoundException(id);
        }

        _playerRepository.Delete(player);

        await _unitOfWork.SaveChangesAsync();
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
