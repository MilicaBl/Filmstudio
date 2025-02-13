using System;
using API.Models;
using API.Models.Film;

namespace API.Interfaces;

public interface IFilmStudio : IUser
{
    string FilmStudioId { get; }
    string Name { get; set; }
    string City { get; set; }
}
