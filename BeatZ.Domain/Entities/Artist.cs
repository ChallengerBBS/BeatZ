﻿using System.ComponentModel.DataAnnotations;

namespace BeatZ.Domain.Entities;

public class Artist
{
    public Artist()
    {
        this.ArtistName = "";
        this.Track = new Track();
    }

    [Key]
    public int ArtistId { get; set; }

    public Track Track { get; set; }

    [Required(ErrorMessage = "There should be an artist name specified!")]
    [MinLength(2, ErrorMessage = "Artist name's length should be atleast 2 characters long!")]
    public string ArtistName { get; set; }
}
