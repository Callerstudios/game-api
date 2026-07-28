using GameApi.Models;
using GameApi.Services.Models;

namespace GameApi.Services.Interfaces;

public interface ITokenService
{
    AccessTokenResult Generate(Player player);
}