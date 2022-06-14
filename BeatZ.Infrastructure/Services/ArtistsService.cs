using BeatZ.Application.Common.Interfaces;
using BeatZ.Domain.Entities;
using System.Linq.Expressions;

namespace BeatZ.Infrastructure.Services
{
    public class ArtistsService : IArtistService
    {
        private readonly IBeatzDbContext _dbContext;

        public ArtistsService(IBeatzDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<int> AddArtistAsync(Artist artist)
        {
            await this._dbContext.Artists.AddAsync(artist);
            return await this._dbContext.SaveChangesAsync(new CancellationToken());
        }

        public async Task DeleteArtist(int id)
        {
            var artist = this._dbContext.Artists.FirstOrDefault(p => p.ArtistId == id);

            if (artist != null)
            {
                this._dbContext.Artists.Remove(artist);
                await this._dbContext.SaveChangesAsync(new CancellationToken());
            }
        }

        public IEnumerable<Artist> GetAllArtists(Expression<Func<Artist, bool>> predicate)
        {
            foreach (var artist in this._dbContext.Artists.Where(predicate))
            {
                yield return artist;
            }
        }

        public Artist GetArtist(Expression<Func<Artist, bool>> predicate)
        {
            return this._dbContext.Artists.FirstOrDefault(predicate);
        }
    }
}
