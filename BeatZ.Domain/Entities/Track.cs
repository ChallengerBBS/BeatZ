using System.ComponentModel.DataAnnotations;

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

        [Required]
        [MaxLength(50)]
        public string TrackName { get; set; }

        [Required]
        [MaxLength(250)]
        public string FilePath { get; set; }

        public virtual ICollection<Album> Albums { get; set; }

        public virtual ICollection<Artist> Artists { get; set; }
    }
}
