using System;
using API.DTOs;
using API.Models;
using API.Models.Film;

namespace API.Interfaces.IRepositories;

public interface IFilmStudioRepository
{
    Task<bool> RentFilm(FilmStudioDTO studio, FilmCopyDTO filmCopy);
    Task<bool> ReturnFilm(FilmStudioDTO studio, FilmCopyDTO filmCopy);
    Task<IEnumerable<FilmStudioDTO>> GetAllFullFilmStudios();
     Task<IEnumerable<FilmStudioMinimalDTO>> GetAllMiniFilmStudios();
    Task <FilmStudioDTO?> GetFullFilmStudioById(int id);
    Task <FilmStudioMinimalDTO?> GetMiniFilmStudioById(int id);
    Task<IEnumerable<FilmCopyDTO>> GetRentedFilmsForFilmStudio(int id);
}
