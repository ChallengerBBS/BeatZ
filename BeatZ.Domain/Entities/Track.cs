﻿namespace BeatZ.Domain.Entities
{
    public class Track
    {
        public Track()
        {
            this.TrackName = "";
            this.Artists = new List<Artist>();
        }

        public int TrackId { get; set; }

        public string TrackName { get; set; }

        public string FilePath { get; set; }

        public ICollection<Album> Albums { get; set; }

        public ICollection<Artist> Artists { get; set; }
    }
}
