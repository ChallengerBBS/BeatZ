using BeatZ.Application.Common.Interfaces;
using BeatZ.Domain.Entities;
using BeatZ.Domain.Entities.Auth;
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

        public DbSet<UserModel> Users { get; set; } = null!;

        public DbSet<Artist> Artists { get; set; } = null!;
        public DbSet<Track> Tracks { get; set; } = null!;
    }
}
