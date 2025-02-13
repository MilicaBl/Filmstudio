using System;
using API.Models;
using API.Models.Film;


namespace API.Interfaces;

public interface IFilmCopy
{
        int Id { get; set; }
        bool IsRented { get; set; }
        int FilmId { get; set; }
        Film Film { get; set; }
        string FilmStudioId { get; set; }
        FilmStudio FilmStudio { get; set; }
        DateTime? TimeWhenRented { get; set; }
}
