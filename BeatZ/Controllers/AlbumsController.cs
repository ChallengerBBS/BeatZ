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
            if (album == null)
            {
                return BadRequest();
            }

            if (string.IsNullOrEmpty(album.AlbumName))
            {
                return BadRequest("Album name should be in range from 1 to 50 characters!");
            }
            var albumToAdd = new Album() { AlbumName = album.AlbumName };

            foreach (var currentTrack in album.Tracks)
            {
                var track = this.dbContext.Tracks.Where(x => x.TrackId == currentTrack.TrackId).FirstOrDefault();
                if (track == null || (track.TrackName != currentTrack.TrackName))
                {
                    return BadRequest("Some of the provided tracks are not found!");
                }

                albumToAdd.Tracks.Add(track);
            }


            this.dbContext.Albums.Add(albumToAdd);
            await dbContext.SaveChangesAsync(new CancellationToken());

            return CreatedAtAction(nameof(GetAlbum), new { id = albumToAdd.AlbumId }, albumToAdd);
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
