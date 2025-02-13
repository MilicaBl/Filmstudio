using System;
using API.Interfaces.IRepositories;
using API.Models.Film;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class FilmCopyRepository : IFilmCopyRepository
{
    private readonly AppDbContext _context;
    public FilmCopyRepository(AppDbContext context)
    {

        _context = context;
    }
    public async Task AddNewFilmCopy(Film film, int copiesToAdd)
    {
        var newCopies = new List<FilmCopy>();
        for (int i = 0; i < copiesToAdd; i++)
        {
            newCopies.Add(new FilmCopy { FilmId = film.Id, Film = film, FilmStudioId = "" });
        }
        await _context.FilmCopies.AddRangeAsync(newCopies);
    }

    public async Task RemoveFilmCopy(Film film, int copiesToRemove)
    {
        var availableCopies = film.FilmCopies.Where(fc => !fc.IsRented).ToList();
        var rentedCopies = film.FilmCopies.Where(fc => fc.IsRented).OrderBy(fc => fc.TimeWhenRented).ToList();

        int removedCount = 0;
        //remove mot rented movies first
        while (availableCopies.Count > 0 && removedCount < copiesToRemove)
        {
            var copyToRemove = availableCopies.First();
            _context.FilmCopies.Remove(copyToRemove);
            availableCopies.Remove(copyToRemove);
            removedCount++;
        }
        var studios = await _context.FilmStudios.Where(s => s.RentedFilmCopies.Any()).ToListAsync();
        //remove movies that are rented first 
        while (rentedCopies.Count > 0 && removedCount < copiesToRemove)
        {
            var copyToRemove = rentedCopies.First();

            // Find the studio that rents movie and remove connection
            var studio = studios.FirstOrDefault(s => s.RentedFilmCopies.Contains(copyToRemove));
            if (studio != null)
            {
                studio.RentedFilmCopies.Remove(copyToRemove);
            }
            _context.FilmCopies.Remove(copyToRemove);
            rentedCopies.Remove(copyToRemove);
            removedCount++;
        }
    }
}

