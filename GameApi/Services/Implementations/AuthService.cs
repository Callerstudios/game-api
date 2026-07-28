using GameApi.DTOs.Auth;
using GameApi.Exceptions.ConflictException;
using GameApi.Exceptions.UnauthorizedException;
using GameApi.Models;
using GameApi.Repositories.Interfaces;
using GameApi.Services.Interfaces;

public class AuthService : IAuthService
{
    private readonly IPlayerRepository _playerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;

    public AuthService(
        IPlayerRepository playerRepository,
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher)
    {
        _playerRepository = playerRepository;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public async Task<LoginResponseDto> LoginAsync(LoginDto dto)
    {
        var player = await _playerRepository.GetByEmailAsync(dto.Email);

        if (player is null)
        {
            throw new InvalidCredentialsException();
        }

        var isPasswordValid = _passwordHasher.Verify(
            player.PasswordHash,
            dto.Password);

        if (!isPasswordValid)
        {
            throw new InvalidCredentialsException();
        }

        throw new NotImplementedException("JWT generation will be implemented next.");
    }

    public async Task<Guid> RegisterAsync(RegisterDto dto)
    {
        if (await _playerRepository.UsernameExistsAsync(dto.Username))
        {
            throw new DuplicateUsernameException(dto.Username);
        }

        if (await _playerRepository.EmailExistsAsync(dto.Email))
        {
            throw new DuplicateEmailException(dto.Email);
        }

        var passwordHash = _passwordHasher.Hash(dto.Password);

        var player = new Player(
            dto.Username,
            dto.Email,
            passwordHash);

        await _playerRepository.AddAsync(player);

        await _unitOfWork.SaveChangesAsync();

        return player.Id;
    }
}