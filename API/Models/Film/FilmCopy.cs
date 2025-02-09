using System;
using API.Interfaces;
using API.Models.Film;


namespace API.Models.Film;

public class FilmCopy:IFilmCopy
{
    public int Id { get; set; }
    public bool IsRented { get; set; }=false;
    public DateTime? TimeWhenRented{get; set;}

    //relation to film
    public int FilmId { get; set; }
    public required Film Film{get; set;}

    //relation to filmstudio
    public int FilmStudioId { get; set; }
    public FilmStudio? FilmStudio {get; set;}
}