﻿namespace BeatZ.Models
{
    public class Album
    {
        public Album()
        {
            this.AlbumName = "";
            this.Tracks = new List<Track>();
        }
        public int AlbumId { get; set; }

        public string AlbumName { get; set; }

        public List<Track> Tracks { get; set; }
    }
}
