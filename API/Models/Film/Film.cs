using System;
using API.Interfaces;

namespace API.Models.Film;

public class Film : IFilm
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string Director { get; set; } = string.Empty;
    public int ReleaseYear { get; set;}
    public string Genre { get; set; }=string.Empty;

    //One film can have many copies
    public List<FilmCopy> FilmCopies { get; set; } = new List<FilmCopy>();
}
