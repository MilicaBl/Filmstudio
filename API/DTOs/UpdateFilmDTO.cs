using System;

namespace API.DTOs;

public class UpdateFilmDTO
{
    public string? Title { get; set; }
    public string? Director { get; set; }
    public string? Genre { get; set; }
    public int? ReleaseYear { get; set; }
    public int? NumberOfCopies { get; set; }
}
