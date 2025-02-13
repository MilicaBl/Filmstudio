using System;
using API.Interfaces;
using API.Models.Film;

namespace API.DTOs;

public class FilmStudioDTO
{
    public string FilmStudioId { get; set; }
    public required string Name { get; set; }
    public required string City { get; set; }
    public List<FilmCopyDTO>? RentedFilmCopies { get; set; }
}
