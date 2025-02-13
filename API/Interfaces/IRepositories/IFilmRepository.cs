using System;
using API.DTOs;
using API.Models.Film;

namespace API;

public interface IFilmRepository
{
    Task<IEnumerable<FilmDTO>> GetAllFilms();
    Task<FilmDTO?> GetFilmById(int id);
    void AddNewFilm(Film film);
    Task<Film?> Update(int id, UpdateFilmDTO updateDto);
    Task<IEnumerable<FilmWithCopiesDTO>> GetAllFilmsWithCopies();
    Task<FilmWithCopiesDTO?> GetFilmWithCopiesById(int id);
}
