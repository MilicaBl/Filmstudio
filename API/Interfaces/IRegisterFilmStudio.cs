using System;

namespace API.Interfaces;

public interface IRegisterFilmStudio
{
    string UserName { get; set; }
    string FilmStudioName { get; set; }
    string City { get; set; }
    string Password { get; set; }
}
