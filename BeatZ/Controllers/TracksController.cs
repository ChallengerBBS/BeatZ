using BeatZ.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BeatZ.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TracksController : ControllerBase
    {
        private readonly ILogger<TracksController> _logger;

        private readonly List<Track> _tracks = new List<Track>();

        private static readonly List<Artist> _artists = new List<Artist> {
            new Artist() { ArtistId = 1, ArtistName = "Michael Jackson" },
            new Artist() { ArtistId = 2, ArtistName = "Taylor Swift" },
            new Artist() { ArtistId = 3, ArtistName = "Britney Spears" },
            new Artist() { ArtistId = 4, ArtistName = "Beyonce" },
            new Artist() { ArtistId = 5, ArtistName = "Elton John" },

        };

        public TracksController(ILogger<TracksController> logger)
        {
            _logger = logger;
            _tracks.Add(new Track() { TrackId = 1, TrackName = "Got to be there", Artists = new List<Artist>() { _artists[0] } });
            _tracks.Add(new Track() { TrackId = 2, TrackName = "August", Artists = new List<Artist>() { _artists[1] } });
            _tracks.Add(new Track() { TrackId = 3, TrackName = "Lucky", Artists = new List<Artist>() { _artists[2] } });
            _tracks.Add(new Track() { TrackId = 4, TrackName = "Halo", Artists = new List<Artist>() { _artists[3] } });
            _tracks.Add(new Track() { TrackId = 5, TrackName = "Your Song", Artists = new List<Artist>() { _artists[4] } });
        }

        [HttpGet]
        public IEnumerable<Track> GetAllTracks()
        {
            return this._tracks;
        }
    }
}