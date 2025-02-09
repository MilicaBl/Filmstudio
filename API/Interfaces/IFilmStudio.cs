using System;
using API.Models;
using API.Models.Film;

namespace API.Interfaces;

public interface IFilmStudio
{
    int Id { get; set; }
    string FilmStudioName { get; set; }
    string City { get; set; }
    List<FilmCopy> RentedFilmCopies { get; set; }
}
