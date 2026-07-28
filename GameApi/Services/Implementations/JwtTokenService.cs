using GameApi.Configuration;
using GameApi.Models;
using GameApi.Services.Interfaces;
using GameApi.Services.Models;
using Microsoft.Extensions.Options;

namespace GameApi.Services.Implementations
{
    public class JwtTokenService : ITokenService
    {
        private readonly JwtOptions _options;

        public JwtTokenService(IOptions<JwtOptions> options)
        {
            _options = options.Value;
        }

        public LoginToken Generate(Player player)
        {
            throw new NotImplementedException();
        }
    }
}
