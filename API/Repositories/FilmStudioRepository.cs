using System;
using API.DTOs;
using API.Interfaces.IRepositories;
using API.Models;
using API.Models.Film;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class FilmStudioRepository : IFilmStudioRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public FilmStudioRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<FilmStudioDTO>> GetAllFullFilmStudios()
    {
        var filmStudios = await _context.FilmStudios.Include(s => s.RentedFilmCopies).ToListAsync();
        return _mapper.Map<IEnumerable<FilmStudioDTO>>(filmStudios);
    }

    public async Task<IEnumerable<FilmStudioMinimalDTO>> GetAllMiniFilmStudios()
    {
        var filmStudios = await _context.FilmStudios.Include(s => s.RentedFilmCopies).ToListAsync();
        return _mapper.Map<IEnumerable<FilmStudioMinimalDTO>>(filmStudios);
    }

    public async Task<FilmStudioDTO?> GetFullFilmStudioById(int id)
    {
        var filmStudio = await _context.FilmStudios.Include(s => s.RentedFilmCopies).FirstOrDefaultAsync(s => s.Id == id);
        return filmStudio == null ? null : _mapper.Map<FilmStudioDTO>(filmStudio);
    }

    public async Task<FilmStudioMinimalDTO?> GetMiniFilmStudioById(int id)
    {
        var filmStudio = await _context.FilmStudios.Include(s => s.RentedFilmCopies).FirstOrDefaultAsync(s => s.Id == id);
        return filmStudio == null ? null : _mapper.Map<FilmStudioMinimalDTO>(filmStudio);
    }

    public async Task<IEnumerable<FilmCopyDTO>> GetRentedFilmsForFilmStudio(int id)
    {
        return await _context.FilmCopies
        .Where(fc => fc.FilmStudioId == id && fc.IsRented == true)
        .Select(fc => new FilmCopyDTO
        {
            Id = fc.Id
        })
        .ToListAsync();
    }

    public async Task<bool> RentFilm(FilmStudioDTO studio, FilmCopyDTO filmCopy)
    {
        filmCopy.IsRented = true;
        studio.RentedFilmCopies?.Add(_mapper.Map<FilmCopy>(filmCopy));

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ReturnFilm(FilmStudioDTO studio, FilmCopyDTO rentedFilmCopy)
    {
        rentedFilmCopy.IsRented = false;
        studio.RentedFilmCopies?.Remove(_mapper.Map<FilmCopy>(rentedFilmCopy));

        await _context.SaveChangesAsync();
        return true;

    }

}
