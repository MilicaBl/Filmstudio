using System;
using API.Models;
using API.Models.Film;

namespace API.Interfaces;

public interface IFilm
{
    int Id { get; set; }
    string Title { get; set; }
    string Director { get; set; }
    int ReleaseYear { get; set; }
    string Genre { get; set; }
    List<FilmCopy> FilmCopies { get; set; } 
    //Antal tillganglia exemplar som gar att lana ut
}
