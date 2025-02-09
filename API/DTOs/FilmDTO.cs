using System;

namespace API.DTOs;

public class FilmDTO
{
    public int Id { get; set; }
    public required string  Title { get; set; }
    public string Director { get; set; }=string.Empty;
    public int ReleaseYear { get; set; }
    public string Genre { get; set; }=string.Empty;
}
