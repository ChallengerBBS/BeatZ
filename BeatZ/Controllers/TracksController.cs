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
        private readonly ITrackService _trackService;
        private readonly IMapper _mapper;

        public TracksController(ITrackService trackService, IMapper mapper)
        {
            this._trackService = trackService;
            this._mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Track>> AddTrack(Track track)
        {
            if (track == null ||
                string.IsNullOrEmpty(track.TrackName) ||
                string.IsNullOrEmpty(track.FilePath))
                return BadRequest("Invalid input data!");

            var trackId = await this._trackService.AddTrackAsync(track);

            if (trackId == 0)
            {
                return BadRequest();
            }


            return CreatedAtAction(nameof(GetTrack), new { id = trackId });
        }

        [HttpGet]
        public IEnumerable<TrackListDto> GetAllTracks()
        {
            var allTracks = this._trackService.GetAllTracks(p => true);


            foreach (var track in allTracks)
            {
                var dto = _mapper.Map<TrackListDto>(track);

                var artistsNames = this._trackService.GetTrackArtistsNames(track.TrackId);

                dto.Artists.AddRange(artistsNames);

                yield return dto;
            }
        }

        [HttpGet("{trackId:int}")]
        public ActionResult<Track> GetTrack(int trackId)
        {
            var track = this._trackService.GetTrack(p => p.TrackId == trackId);

            if (track == null)
            {
                return NotFound();
            }

            return Ok(track);
        }

        //[HttpGet("{trackId:int}/play")]
        //public FileContentResult PlaySong(int trackId)
        //{
        //    var track = this._dbContext.Tracks.FirstOrDefault(p => p.TrackId == trackId);

        //    if (track != null && !string.IsNullOrWhiteSpace(track.FilePath))
        //    {
        //        return File(System.IO.File.ReadAllBytes(track.FilePath), "audio/mpeg", Path.GetFileName(track.FilePath));
        //    }

        //    return new FileContentResult(new byte[0], "audio/mpeg");
        //}


        [HttpPatch("{id:int}")]
        public async Task<ActionResult> EditTrack(int id, [FromBody] JsonPatchDocument<Track> patchEntity)
        {
            var track = this._trackService.GetTrack(p => p.TrackId == id);
            if (track == null)
            {
                return NotFound();
            }

            patchEntity.ApplyTo(track, ModelState);

            var trackId = await this._trackService.AddTrackAsync(track);

            if (trackId == 0)
            {
                return BadRequest();
            }

            return Accepted();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTrack(int id)
        {
            var track = this._trackService.GetTrack(p => p.TrackId == id);
            if (track != null)
            {
                await this._trackService.DeleteTrack(track.TrackId);
                return Accepted();
            }

            return NotFound();
        }
    }
}