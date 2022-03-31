using BeatZ.Domain.Validation;
using System.ComponentModel.DataAnnotations;

namespace BeatZ.Domain.Entities
{
    public class Track
    {
        public Track()
        {
            this.TrackName = "";
            this.Artists = new List<Artist>();
        }

        [Key]
        public int TrackId { get; set; }

        [Required(ErrorMessage = "Track name is required!")]
        public string TrackName { get; set; }

        [MustHaveAtleastOneElement(ErrorMessage ="There should be alteast one track artist specified!")]
        public List<Artist> Artists { get; set; }
    }
}
