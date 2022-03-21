using BeatZ.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace BeatZ.Persistence
{
    public  class BeatzDbContext : DbContext
    {
       public BeatzDbContext(DbContextOptions<BeatzDbContext> options)
            : base(options)
        {
        }

        public DbSet<Album> Albums { get; set; } = null!;
        public DbSet<Artist> Artists { get; set; } = null!;
        public DbSet<Track> Tracks { get; set; } = null!;
    }
}
