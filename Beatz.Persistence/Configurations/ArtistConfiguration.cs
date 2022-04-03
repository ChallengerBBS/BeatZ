using BeatZ.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeatZ.Persistence.Configurations
{
    public sealed class ArtistConfiguration : IEntityTypeConfiguration<Artist>
    {
        public void Configure(EntityTypeBuilder<Artist> builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.ToTable("Artists");

            builder.Property(b => b.ArtistName)
                .IsRequired();

            builder.HasMany(a => a.Tracks)
                .WithMany(t => t.Artists);
        }
    }
}
