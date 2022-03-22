using BeatZ.Application.Common.Interfaces;
using BeatZ.Domain.Entities;
using BeatZ.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BeatZ.Persistence
{
    public  class BeatzDbContext : IdentityDbContext<ApplicationUser>, IBeatzDbContext
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
