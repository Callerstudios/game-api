using GameApi.Models;
using GameApi.Services.Models;

namespace GameApi.Services.Interfaces;

public interface ITokenService
{
    LoginToken Generate(Player player);
}