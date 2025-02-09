using System;
using API.Models.Film;

namespace API.Interfaces.IRepositories;

public interface IFilmCopyRepository
{
    Task AddNewFilmCopy(Film film, int copiesToAdd);
    Task RemoveFilmCopy( Film film, int copiesToRemove);
}
