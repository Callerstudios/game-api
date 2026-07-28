using GameApi.DTOs.Auth;
using GameApi.Models;
using GameApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GameApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var playerId = await _authService.RegisterAsync(dto);

        return CreatedAtAction(
            nameof(PlayersController.GetPlayer),
            nameof(PlayersController),
            new { id = playerId },
            null);
    }
    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<LoginResponseDto>> Login(LoginDto dto)
    {
        var response = await _authService.LoginAsync(dto);

        return Ok(response);
    }
}
