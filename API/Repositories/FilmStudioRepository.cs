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
    
    public async Task<FilmStudioDTO?> GetFullFilmStudioById(string id)
    {
        var filmStudio = await _context.FilmStudios.Include(s => s.RentedFilmCopies).ThenInclude(fc => fc.Film).FirstOrDefaultAsync(s => s.Id == id);
        return filmStudio == null ? null : _mapper.Map<FilmStudioDTO>(filmStudio);
    }

    public async Task<FilmStudioMinimalDTO?> GetMiniFilmStudioById(string id)
    {
        var filmStudio = await _context.FilmStudios.Include(s => s.RentedFilmCopies).FirstOrDefaultAsync(s => s.Id == id);
        return filmStudio == null ? null : _mapper.Map<FilmStudioMinimalDTO>(filmStudio);
    }

    public async Task<IEnumerable<FilmCopyDTO>> GetRentedFilmsForFilmStudio(string id)
    {
        var filmcopies = await _context.FilmCopies
        .Where(fc => fc.FilmStudioId == id && fc.IsRented == true).Include(fc=>fc.Film).ToListAsync();
        return _mapper.Map<IEnumerable<FilmCopyDTO>>(filmcopies);
    }

    public async Task<bool> RentFilm(FilmStudioDTO studio, FilmCopyDTO filmCopy)
    {
        var filmCopyInDb = await _context.FilmCopies.FindAsync(filmCopy.Id);
        var filmStudioInDb = await _context.FilmStudios.FindAsync(studio.FilmStudioId);
        if (filmCopyInDb == null || filmStudioInDb == null)
        {
            return false;
        }
        filmCopyInDb.IsRented = true;
        filmCopyInDb.TimeWhenRented = DateTime.UtcNow;
        filmStudioInDb.RentedFilmCopies?.Add(filmCopyInDb);

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ReturnFilm(FilmStudioDTO studio, FilmCopyDTO rentedFilmCopy)
    {
        var filmCopyInDb = await _context.FilmCopies.FindAsync(rentedFilmCopy.Id);
        var filmStudioInDb = await _context.FilmStudios.Include(fs => fs.RentedFilmCopies).FirstOrDefaultAsync(fs => fs.Id == studio.FilmStudioId);
        if (filmCopyInDb == null || filmStudioInDb == null)
        {
            return false;
        }
        filmCopyInDb.IsRented = false;
        filmCopyInDb.TimeWhenRented = null;
        filmCopyInDb.FilmStudioId = string.Empty;//removes connection to filmstudio

        await _context.SaveChangesAsync();
        return true;
    }

}
