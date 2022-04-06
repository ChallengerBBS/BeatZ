using BeatZ.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeatZ.Persistence.Configurations
{
    public sealed class TrackConfiguration : IEntityTypeConfiguration<Track>
    {
        public void Configure(EntityTypeBuilder<Track> builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.ToTable("Tracks");

            builder.Property(b => b.TrackName)
                .IsRequired();

            builder.HasMany(t => t.Artists)
                .WithMany(a => a.Tracks);
        }
    }
}
