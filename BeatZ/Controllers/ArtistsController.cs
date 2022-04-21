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
        private readonly ILogger<ArtistsController> logger;
        private readonly IBeatzDbContext dbContext;

        public ArtistsController(ILogger<ArtistsController> logger, IBeatzDbContext dbContext)
        {
            this.logger = logger;
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<Artist> GetAllArtists()
        {
            foreach (var artist in this.dbContext.Artists)
            {
                yield return artist;
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Artist> GetArtist(int id)
        {
            var artist = this.dbContext.Artists.FirstOrDefault(p => p.ArtistId == id);

            if (artist == null)
            {
                return NotFound();
            }

            return Ok(artist);
        }

        [HttpPost]
        public async Task<ActionResult<Artist>> AddArtist(Artist artist)
        {
            if (artist == null)
            {
                return BadRequest();
            }

            if (string.IsNullOrWhiteSpace(artist.ArtistName) || artist.ArtistName.Length > 50)
            {
                return BadRequest("Artist name should be in range from 1 to 50 characters!");
            }

            this.dbContext.Artists.Add(artist);
            await dbContext.SaveChangesAsync(new CancellationToken());

            return CreatedAtAction(nameof(GetArtist), new { id = artist.ArtistId }, artist);
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult> EditArtist(int id, [FromBody] JsonPatchDocument<Artist> patchEntity)
        {
            var artist = this.dbContext.Artists.FirstOrDefault(p => p.ArtistId == id);
            if (artist == null)
            {
                return NotFound();
            }

            patchEntity.ApplyTo(artist, ModelState);

            await dbContext.SaveChangesAsync(new CancellationToken());
            return Accepted();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteArtist(int id)
        {
            var artist = this.dbContext.Artists.FirstOrDefault(p => p.ArtistId == id);
            if (artist != null)
            {
                this.dbContext.Artists.Remove(artist);
                await dbContext.SaveChangesAsync(new CancellationToken());
                return Accepted();
            }

            return NoContent();
        }
    }
}
