using FluentValidation;
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
    private readonly ITokenService _tokenService;

    private readonly IValidator<RegisterDto> _registerValidator;
    private readonly IValidator<LoginDto> _loginValidator;

    public AuthService(
        IPlayerRepository playerRepository,
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher,
        ITokenService tokenService,
        IValidator<RegisterDto> registerValidator,
        IValidator<LoginDto> loginValidator)
    {
        _playerRepository = playerRepository;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
        _registerValidator = registerValidator;
        _loginValidator = loginValidator;
    }

    public async Task<LoginResponseDto> LoginAsync(LoginDto dto)
    {
        await _loginValidator.ValidateAndThrowAsync(dto);

        var player = await _playerRepository.
            GetByEmailAsync(dto.Email);

        if (player is null)
        {
            Console.WriteLine("No User");
            throw new InvalidCredentialsException();
        }

        var isPasswordValid = _passwordHasher.Verify(
            player.PasswordHash,
            dto.Password);

        if (!isPasswordValid)
        {
            Console.WriteLine("No Valid Password");
            throw new InvalidCredentialsException();
        }

        var token = _tokenService.Generate(player);

        var user = new AuthenticatedUserDto(
        player.Id,
        player.Username,
        player.Email);

        return new LoginResponseDto(
            token.AccessToken,
            token.ExpiresAt,
            user);
    }

    public async Task<Guid> RegisterAsync(RegisterDto dto)
    {
        await _registerValidator.ValidateAndThrowAsync(dto);

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