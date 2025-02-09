using System;
using API.Interfaces;

namespace API.DTOs;

public class CreateFilmDTO:ICreateFilm
{
    public required string Title { get; set; }
    public required string Director { get; set; }
    public int ReleaseYear { get; set; }
    public int NumberOfCopies { get; set; } = 0;
}
