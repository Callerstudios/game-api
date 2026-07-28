using GameApi.DTOs.Auth;

namespace GameApi.Services.Interfaces
{
    public interface IAuthService
    {
        Task<Guid> RegisterAsync(RegisterDto dto);

        //Task<string> LoginAsync(LoginDto dto);
        Task<LoginResponseDto> LoginAsync(LoginDto dto);
    }
}
