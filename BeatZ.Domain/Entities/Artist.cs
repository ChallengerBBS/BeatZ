﻿namespace BeatZ.Domain.Entities;

public class Artist
{
    public Artist()
    {
        this.ArtistName = "";
    }
    public int ArtistId { get; set; }

    public string ArtistName { get; set; }
}
