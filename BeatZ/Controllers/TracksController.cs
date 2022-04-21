using BeatZ.Application.Common.Interfaces;
using BeatZ.Domain.Entities;
using Microsoft.AspNetCore.JsonPatch;
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

        [HttpPost]
        public async Task<ActionResult<Track>> AddTrack(Track track)
        {
            if (track == null ||
                string.IsNullOrEmpty(track.TrackName) ||
                string.IsNullOrEmpty(track.FilePath))
                return BadRequest("Invalid input data!");

            var trackToAdd = new Track()
            {
                TrackName = track.TrackName,
                FilePath = track.FilePath
            };

            foreach (var currentArtist in track.Artists)
            {
                var artist = this.dbContext.Artists.Where(x => x.ArtistId == currentArtist.ArtistId).FirstOrDefault();
                if (artist == null || (artist.ArtistName != currentArtist.ArtistName))
                {
                    return BadRequest();
                }
            }
            trackToAdd.Artists.ToList().AddRange(track.Artists);

            this.dbContext.Tracks.Add(trackToAdd);
            await dbContext.SaveChangesAsync(new CancellationToken());

            if (trackToAdd.TrackId > 0)
            {
                this._logger.LogInformation($"Insert new track: {trackToAdd}");
            }

            return CreatedAtAction(nameof(GetTrack), new { id = trackToAdd.TrackId }, trackToAdd);
        }

        [HttpGet]
        public IEnumerable<Track> GetAllTracks()
        {
            foreach (var track in this.dbContext.Tracks)
            {
                yield return track;
            }
        }

        [HttpGet("{id:int}")]
        public ActionResult<Track> GetTrack(int id)
        {
            var track = this.dbContext.Tracks.FirstOrDefault(p => p.TrackId == id);

            if (track == null)
            {
                return NotFound();
            }

            return Ok(track);
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult> EditTrack(int id, [FromBody] JsonPatchDocument<Track> patchEntity)
        {
            var track = this.dbContext.Tracks.FirstOrDefault(p => p.TrackId == id);
            if (track == null)
            {
                return NotFound();
            }

            patchEntity.ApplyTo(track, ModelState);

            await dbContext.SaveChangesAsync(new CancellationToken());
            return Accepted();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTrack(int id)
        {
            var track = this.dbContext.Tracks.FirstOrDefault(p => p.TrackId == id);
            if (track != null)
            {
                this.dbContext.Tracks.Remove(track);
                await dbContext.SaveChangesAsync(new CancellationToken());
                return Accepted();
            }

            return NotFound();
        }
    }
}