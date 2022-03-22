using BeatZ.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BeatZ.Application.Common.Interfaces
{
    public interface IBeatzDbContext
    {
        public DbSet<Album> Albums { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Track> Tracks { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
