using System.Linq.Expressions;

using BeatZ.Domain.Entities;

namespace BeatZ.Application.Common.Interfaces
{
    public interface IArtistService
    {
        public Task<int> AddArtistAsync(Artist artist);

        public IEnumerable<Artist> GetAllArtists(Expression<Func<Artist, bool>> predicate);

        public Artist GetArtist(Expression<Func<Artist, bool>> predicate);

        public Task DeleteArtist(int id);
    }
}
