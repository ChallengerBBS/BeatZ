using BeatZ.Application.Common.Interfaces;
using BeatZ.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BeatZ.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlbumsController : ControllerBase
    {
        private readonly IAlbumService _albumService;
        private readonly ITrackService _trackService;

        public AlbumsController(IAlbumService albumService, ITrackService trackService)
        {
            this._albumService = albumService;
            this._trackService = trackService;
        }

        [HttpGet]
        public IEnumerable<Album> GetAllAlbums()
        {
            return this._albumService.GetAllAlbums(p => true);
        }

        [HttpGet("{id}")]
        public ActionResult<Album> GetAlbum(int id)
        {
            var album = this._albumService.GetAlbum(p => p.AlbumId == id);

            if (album == null)
            {
                return NotFound();
            }

            return Ok(album);
        }

        [HttpPost]
        public async Task<ActionResult<Album>> AddAlbumAsync(Album album)
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
                var track = this._trackService.GetTrack(x => x.TrackId == currentTrack.TrackId);
                if (track == null || (track.TrackName != currentTrack.TrackName))
                {
                    return BadRequest("Some of the provided tracks are not found!");
                }

                albumToAdd.Tracks.Add(track);
            }


            var albumId = await this._albumService.AddAlbumAsync(albumToAdd);

            return CreatedAtAction(nameof(GetAlbum), new { id = albumId });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAlbum(int id)
        {
            var album = this._albumService.GetAlbum(p => p.AlbumId == id);
            if (album != null)
            {
                await this._albumService.DeleteAlbumAsync(album.AlbumId);
                return Accepted();
            }

            return NoContent();
        }
    }
}
