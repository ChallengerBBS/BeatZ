using System.ComponentModel.DataAnnotations;

namespace BeatZ.Domain.Entities
{
    public class Album
    {
        public Album()
        {
            this.AlbumName = "";
            this.Tracks = new List<Track>();
        }

        [Key]
        public int AlbumId { get; set; }

        public string AlbumName { get; set; }

        public List<Track> Tracks { get; set; }
    }
}
