using BeatZ.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeatZ.Persistence.Configurations
{
    public sealed class AlbumConfiguration : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.ToTable("Albums");

            builder.HasMany(a => a.Tracks)
                .WithMany(t=>t.Albums);
        }
    }
}
