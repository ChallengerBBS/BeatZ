using BeatZ.Application.Common.Interfaces;
using BeatZ.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BeatZ.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TracksController : ControllerBase
    {
        private readonly ILogger<TracksController> _logger;
        private readonly IBeatzDbContext dbContext;

        public TracksController(ILogger<TracksController> logger, IBeatzDbContext dbContext)
        {
            _logger = logger;
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<Track> GetAllTracks()
        {
            foreach (var track in this.dbContext.Tracks)
            {
                yield return track;
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Track))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Track> GetTrack(int id)
        {
            var track = this.dbContext.Tracks.FirstOrDefault(p => p.TrackId == id);

            if (track == null)
            {
                return NotFound();
            }

            return Ok(track);
        }

        [HttpPost]
        public async Task<ActionResult<Track>> CreateTracks(Track track)
        {
            this.dbContext.Tracks.Add(track);
            await dbContext.SaveChangesAsync(new CancellationToken());

            return CreatedAtAction(nameof(GetTrack), new { id = track.TrackId }, track);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTracks(int id)
        {
            var track = this.dbContext.Tracks.FirstOrDefault(p => p.TrackId == id);
            if (track != null)
            {
                this.dbContext.Tracks.Remove(track);
                await dbContext.SaveChangesAsync(new CancellationToken());
                return Accepted();
            }

            return NoContent();
        }
    }
}