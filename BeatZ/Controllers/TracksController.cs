using AutoMapper;
using BeatZ.Application.Common.Interfaces;
using BeatZ.Domain.Dtos;
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
        private readonly IBeatzDbContext _dbContext;
        private readonly IMapper _mapper;

        public TracksController(ILogger<TracksController> logger, IBeatzDbContext dbContext, IMapper mapper)
        {
            _logger = logger;
            this._dbContext = dbContext;
            this._mapper = mapper;
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
                var artist = this._dbContext.Artists.Where(x => x.ArtistId == currentArtist.ArtistId).FirstOrDefault();
                if (artist == null || (artist.ArtistName != currentArtist.ArtistName))
                {
                    return BadRequest();
                }
            }
            trackToAdd.Artists.ToList().AddRange(track.Artists);

            this._dbContext.Tracks.Add(trackToAdd);
            await _dbContext.SaveChangesAsync(new CancellationToken());

            if (trackToAdd.TrackId > 0)
            {
                this._logger.LogInformation($"Insert new track: {trackToAdd}");
            }

            return CreatedAtAction(nameof(GetTrack), new { id = trackToAdd.TrackId }, trackToAdd);
        }

        [HttpGet]
        public IEnumerable<TrackListDto> GetAllTracks()
        {
            foreach (var track in this._dbContext.Tracks)
            {
                var dto = _mapper.Map<TrackListDto>(track);

                var artists = this._dbContext.Tracks
                    .Where(c => c.TrackId == track.TrackId)
                    .SelectMany(c => c.Artists)
                    .Select(c => c.ArtistName);

                dto.Artists.AddRange(artists);

                yield return dto;
            }
        }

        [HttpGet("{id:int}")]
        public ActionResult<Track> GetTrack(int id)
        {
            var track = this._dbContext.Tracks.FirstOrDefault(p => p.TrackId == id);

            if (track == null)
            {
                return NotFound();
            }

            return Ok(track);
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult> EditTrack(int id, [FromBody] JsonPatchDocument<Track> patchEntity)
        {
            var track = this._dbContext.Tracks.FirstOrDefault(p => p.TrackId == id);
            if (track == null)
            {
                return NotFound();
            }

            patchEntity.ApplyTo(track, ModelState);

            await _dbContext.SaveChangesAsync(new CancellationToken());
            return Accepted();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTrack(int id)
        {
            var track = this._dbContext.Tracks.FirstOrDefault(p => p.TrackId == id);
            if (track != null)
            {
                this._dbContext.Tracks.Remove(track);
                await _dbContext.SaveChangesAsync(new CancellationToken());
                return Accepted();
            }

            return NotFound();
        }
    }
}