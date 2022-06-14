using System.Linq.Expressions;

using BeatZ.Application.Common.Interfaces;
using BeatZ.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace BeatZ.Infrastructure.Services
{
    public class AlbumsService : IAlbumService
    {
        private readonly IBeatzDbContext _dbContext;
        private readonly ILogger<AlbumsService> _logger;

        public AlbumsService(IBeatzDbContext dbContext, ILogger<AlbumsService> logger)
        {
            this._dbContext = dbContext;
            this._logger = logger;
        }

        public async Task<int> AddAlbumAsync(Album album)
        {
            await this._dbContext.Albums.AddAsync(album);
           return await this._dbContext.SaveChangesAsync(new CancellationToken());
        }

        public async Task DeleteAlbumAsync(int id)
        {
            var album = this._dbContext.Albums.FirstOrDefault(a=>a.AlbumId == id);

            if (album != null)
            {
                this._dbContext.Albums.Remove(album);
                await this._dbContext.SaveChangesAsync(new CancellationToken());
            }
        }

        public Album GetAlbum(Expression<Func<Album, bool>> predicate)
        {
            return this._dbContext.Albums.FirstOrDefault(predicate);
        }

        public IEnumerable<Album> GetAllAlbums(Expression<Func<Album, bool>> predicate)
        {
            foreach (var album in this._dbContext.Albums.Where(predicate))
            {
                yield return album;
            }
        }
    }
}
