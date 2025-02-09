using System;
using API.Interfaces;
using API.Models.Film;

namespace API.DTOs;

public class FilmStudioDTO 
{
    public int Id { get; set; }
    public required string FilmStudioName { get; set;}
    public required string City { get; set; }
    public List<FilmCopy>? RentedFilmCopies { get ; set;}
}
