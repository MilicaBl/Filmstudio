using System;
using API.DTOs;
using API.Repositories;
using API.Models.Film;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using API.Interfaces.IRepositories;

namespace API;

public class FilmRepository : IFilmRepository
{
    private readonly AppDbContext _context;
    private readonly IFilmCopyRepository _filmCopy;
    private readonly IMapper _mapper;

    public FilmRepository(AppDbContext context, IFilmCopyRepository filmCopyRepository, IMapper mapper)
    {
        _context = context;
        _filmCopy = filmCopyRepository;
        _mapper = mapper;
    }
    public void AddNewFilm(Film film)
    {
        _context.Films.Add(film);
    }

    public async Task Delete(int id)//anvands inte just nu
    {
        var film = await _context.Films.FindAsync(id);
        if (film != null)
        {
            _context.Films.Remove(film);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<FilmDTO>> GetAllFilms()
    {
        var films = await _context.Films.ToListAsync();
        return _mapper.Map<IEnumerable<FilmDTO>>(films);
    }

    public async Task<IEnumerable<FilmWithCopiesDTO>> GetAllFilmsWithCopies()
    {
        var films = await _context.Films.ToListAsync();
        return _mapper.Map<IEnumerable<FilmWithCopiesDTO>>(films);
    }

    public async Task<FilmDTO?> GetFilmById(int id)
    {
        var film = await _context.Films.FindAsync(id);
        return film == null ? null : _mapper.Map<FilmDTO>(film);
    }

    public async Task<FilmWithCopiesDTO?> GetFilmWithCopiesById(int id)
    {
        var film = await _context.Films.FindAsync(id);
        return film == null ? null : _mapper.Map<FilmWithCopiesDTO>(film);
    }

    public async Task<Film?> Update(int id, UpdateFilmDTO updateDTO)
    {
        var film = await _context.Films.Include(f => f.FilmCopies).FirstOrDefaultAsync(f => f.Id == id);
        if (film == null) return null;

        _mapper.Map(updateDTO, film);

        //if number of copies has changed
        if (updateDTO.NumberOfCopies.HasValue)
        {
            var currentFilmCopiesCount = film.FilmCopies.Count;
            var newFilmCopiesCount = updateDTO.NumberOfCopies.Value;

            if (newFilmCopiesCount > currentFilmCopiesCount)
            {
                var copiesToAdd = newFilmCopiesCount - currentFilmCopiesCount;
                await _filmCopy.AddNewFilmCopy(film, copiesToAdd);
            }
            else if (newFilmCopiesCount < currentFilmCopiesCount)
            {
                var copiesToRemove = currentFilmCopiesCount - newFilmCopiesCount;
                await _filmCopy.RemoveFilmCopy(film, copiesToRemove);

            }
        }

        await _context.SaveChangesAsync();
        return film;
    }
}
