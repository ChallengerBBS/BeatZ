using BeatZ.Application.Common.Interfaces;
using BeatZ.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BeatZ.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlbumsController : ControllerBase
    {
        private readonly ILogger<AlbumsController> logger;
        private readonly IBeatzDbContext dbContext;

        public AlbumsController(ILogger<AlbumsController> logger, IBeatzDbContext dbContext)
        {
            this.logger = logger;
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<Album> GetAllAlbums()
        {
            foreach (var album in this.dbContext.Albums)
            {
                yield return album;
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Album> GetAlbum(int id)
        {
            var album = this.dbContext.Albums.FirstOrDefault(p => p.AlbumId == id);

            if (album == null)
            {
                return NotFound();
            }

            return Ok(album);
        }

        [HttpPost]
        public async Task<ActionResult<Album>> AddAlbum(Album album)
        {
            this.dbContext.Albums.Add(album);
            await dbContext.SaveChangesAsync(new CancellationToken());

            return CreatedAtAction(nameof(GetAlbum), new { id = album.AlbumId }, album);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAlbum(int id)
        {
            var album = this.dbContext.Albums.FirstOrDefault(p => p.AlbumId == id);
            if (album != null)
            {
                this.dbContext.Albums.Remove(album);
                await dbContext.SaveChangesAsync(new CancellationToken());
                return Accepted();
            }

            return NoContent();
        }
    }
}
