using GameApi.Models;
using GameApi.Services.Interfaces;
using GameApi.Services.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GameApi.Services.Implementations
{
    public class JwtTokenService : ITokenService
    {
        private readonly JwtOptions _options;

        public JwtTokenService(IOptions<JwtOptions> options)
        {
            _options = options.Value;
        }

        public AccessTokenResult Generate(Player player)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_options.SecretKey));

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var expiresAt = DateTime.UtcNow.AddMinutes(
                _options.ExpirationMinutes);

            var claims = new List<Claim>
            {
                // Standard JWT Claims
                new(JwtRegisteredClaimNames.Sub, player.Id.ToString()),
                new(JwtRegisteredClaimNames.UniqueName, player.Username),
                new(JwtRegisteredClaimNames.Email, player.Email),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

                // ASP.NET Core Claims
                new(ClaimTypes.NameIdentifier, player.Id.ToString()),
                new(ClaimTypes.Name, player.Username),
                new(ClaimTypes.Email, player.Email)
            };

            var token = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                expires: expiresAt,
                signingCredentials: credentials);

            var accessToken = new JwtSecurityTokenHandler()
                .WriteToken(token);

            return new AccessTokenResult(
                accessToken,
                expiresAt);
        }
    }
}
