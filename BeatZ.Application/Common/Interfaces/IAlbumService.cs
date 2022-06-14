using BeatZ.Domain.Entities;
using System.Linq.Expressions;

namespace BeatZ.Application.Common.Interfaces
{
    public interface IAlbumService
    {
        public Task<int> AddAlbumAsync(Album album);

        public IEnumerable<Album> GetAllAlbums(Expression<Func<Album, bool>> predicate);

        public Album GetAlbum(Expression<Func<Album, bool>> predicate);

        public Task DeleteAlbumAsync(int id);
    }
}
