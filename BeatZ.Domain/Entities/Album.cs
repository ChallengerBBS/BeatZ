using System.ComponentModel.DataAnnotations;

namespace BeatZ.Domain.Entities
{
    public class Album
    {
        public Album()
        {
            this.AlbumName = "";
            this.Tracks = new HashSet<Track>();
        }

        public int AlbumId { get; set; }

        [Required]
        [MaxLength(50)]
        public string AlbumName { get; set; }

        public ICollection<Track> Tracks { get; set; }
    }
}
