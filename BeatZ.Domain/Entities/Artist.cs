using System.ComponentModel.DataAnnotations;

namespace BeatZ.Domain.Entities;

public class Artist
{
    public Artist()
    {
        this.ArtistName = "";
        this.Tracks = new HashSet<Track>();
    }

    public int ArtistId { get; set; }

    [Required]
    [MaxLength(50)]
    public string ArtistName { get; set; }

    public virtual ICollection<Track> Tracks { get; set; } 
}
