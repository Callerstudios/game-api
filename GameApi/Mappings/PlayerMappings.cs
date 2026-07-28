using GameApi.DTOs.Players;
using GameApi.Models;

namespace GameApi.Mappings
{
    public static class PlayerMappings
    {
        public static PlayerDto ToDto(this Player player)
        {
            return new PlayerDto(
                player.Id,
                player.Username,
                player.Email,
                player.Level,
                player.Experience
            );
        }
    }
}
