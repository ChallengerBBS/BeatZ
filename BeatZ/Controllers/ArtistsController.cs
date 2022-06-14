using BeatZ.Application.Common.Interfaces;
using BeatZ.Domain.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace BeatZ.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArtistsController : ControllerBase
    {
        private readonly IArtistService _artistService;

        public ArtistsController(IArtistService artistService)
        {
            this._artistService = artistService;
        }

        [HttpGet]
        public IEnumerable<Artist> GetAllArtists()
        {
            return this._artistService.GetAllArtists(p=>true);
        }

        [HttpGet("{id}")]
        public ActionResult<Artist> GetArtist(int id)
        {
            var artist = this._artistService.GetArtist(p => p.ArtistId == id);

            if (artist == null)
            {
                return NotFound();
            }

            return Ok(artist);
        }

        [HttpPost]
        public async Task<ActionResult<Artist>> AddArtistAsync(Artist artist)
        {
            if (artist == null)
            {
                return BadRequest();
            }

            if (string.IsNullOrWhiteSpace(artist.ArtistName) || artist.ArtistName.Length > 50)
            {
                return BadRequest("Artist name should be in range from 1 to 50 characters!");
            }

            await this._artistService.AddArtistAsync(artist);

            return CreatedAtAction(nameof(GetArtist), new { id = artist.ArtistId }, artist);
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult> EditArtist(int id, [FromBody] JsonPatchDocument<Artist> patchEntity)
        {
            var artist = this._artistService.GetArtist(p => p.ArtistId == id);
            if (artist == null)
            {
                return NotFound();
            }

            patchEntity.ApplyTo(artist, ModelState);

            var artistId = await this._artistService.AddArtistAsync(artist);

            if (artistId == 0)
            {
                return BadRequest();
            }

            return Accepted();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteArtist(int id)
        {
            var artist = this._artistService.GetArtist(p => p.ArtistId == id);
            if (artist != null)
            {
                await this._artistService.DeleteArtist(artist.ArtistId);
                return Accepted();
            }

            return NoContent();
        }
    }
}
