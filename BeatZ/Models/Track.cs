namespace BeatZ.Models
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

        public List<Artist> Artists { get; set; }
    }
}
