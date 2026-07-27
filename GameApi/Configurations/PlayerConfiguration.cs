using GameApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameApi.Configurations;

public class PlayerConfiguration : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Username)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(p => p.Level)
            .IsRequired();
    }
}