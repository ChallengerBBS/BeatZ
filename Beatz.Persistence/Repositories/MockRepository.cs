using BeatZ.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatZ.Persistence.Repositories
{
    public class MockRepository
    {
        private List<Track> _tracks = new List<Track>();
        private List<Artist> _artists = new List<Artist>();
        public MockRepository()
        {
            FeedMockTracks(this._artists);
        }

        private void FeedMockTracks(List<Artist> _artists)
        {
            //this._tracks.Add(new Track() { TrackId = 1, TrackName = "Got to be there", Artists = new List<Artist>() { _artists[0] } });
            //this._tracks.Add(new Track() { TrackId = 2, TrackName = "August", Artists = new List<Artist>() { _artists[1] } });
            //this._tracks.Add(new Track() { TrackId = 3, TrackName = "Lucky", Artists = new List<Artist>() { _artists[2] } });
            //this._tracks.Add(new Track() { TrackId = 4, TrackName = "Halo", Artists = new List<Artist>() { _artists[3] } });
            //this._tracks.Add(new Track() { TrackId = 5, TrackName = "Your Song", Artists = new List<Artist>() { _artists[4] } });
        }

        private void FeedMockArtists()
        {
            this._artists.Add(new Artist() { ArtistId = 1, ArtistName = "Michael Jackson" });
            this._artists.Add(new Artist() { ArtistId = 2, ArtistName = "Taylor Swift" });
            this._artists.Add(new Artist() { ArtistId = 3, ArtistName = "Britney Spears" });
            this._artists.Add(new Artist() { ArtistId = 4, ArtistName = "Beyonce" });
            this._artists.Add(new Artist() { ArtistId = 5, ArtistName = "Elton John" });
        }
    }
}
