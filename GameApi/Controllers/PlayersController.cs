using GameApi.DTOs.Players;
using GameApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GameApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayersController : ControllerBase
{
    private readonly IPlayerService _playerService;

    public PlayersController(IPlayerService playerService)
    {
        _playerService = playerService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PlayerDto>>> GetPlayers()
    {
        var players = await _playerService.GetAllAsync();

        return Ok(players);
    }

    [HttpPost]
    public async Task<ActionResult<PlayerDto>> CreatePlayer(CreatePlayerDto dto)
    {
        var player = await _playerService.CreateAsync(dto);

        //return CreatedAtAction(
        //    nameof(GetPlayers),
        //    new { id = player.Id },
        //    player);
        return Ok(player);
    }
}