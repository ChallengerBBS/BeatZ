namespace BeatZ.Domain.Entities
{
    public class Track
    {
        public Track()
        {
            this.TrackName = "";
            this.FilePath = "";
            this.Albums = new HashSet<Album>();
            this.Artists = new HashSet<Artist>();
        }

        public int TrackId { get; set; }

        public string TrackName { get; set; }

        public string FilePath { get; set; }

        public virtual ICollection<Album> Albums { get; set; }

        public virtual ICollection<Artist> Artists { get; set; }
    }
}
