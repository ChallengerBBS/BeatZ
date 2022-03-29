using BeatZ.Application.Common.Interfaces;
using BeatZ.Domain.Entities;
using BeatZ.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BeatZ.Persistence
{
    public class BeatzDbContext : DbContext, IBeatzDbContext
    {
        public BeatzDbContext()
        {
        }

        public BeatzDbContext(DbContextOptions<BeatzDbContext> options)
             : base(options)
        {
        }

        public DbSet<Album> Albums { get; set; } = null!;
        public DbSet<Artist> Artists { get; set; } = null!;
        public DbSet<Track> Tracks { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _ = modelBuilder.Entity<Track>()
                 .HasMany(t => t.Artists)
                 .WithOne(a => a.Track);
        }
    }
}
