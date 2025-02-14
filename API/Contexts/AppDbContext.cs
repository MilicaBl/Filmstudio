using System;
using API.Models.Film;
using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        this.Database.EnsureCreated();
    }
    public DbSet<Film> Films { get; set; }
    public DbSet<FilmStudio> FilmStudios { get; set; }
    public DbSet<FilmCopy> FilmCopies { get; set; }
    // public DbSet<ApplicationUser> Users { get; set; } 
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Film>().HasData(
           new Film { Id = 1, Title = "The Shawshank Redemption", Director = "Frank Darabont", ReleaseYear = 1994, Genre = "Drama" },
           new Film { Id = 2, Title = "The Godfather", Director = "Francis Ford Coppola", ReleaseYear = 1972, Genre = "Crime, Drama" },
           new Film { Id = 3, Title = "The Dark Knight", Director = "Christopher Nolan", ReleaseYear = 2008, Genre = "Action, Crime, Drama" },
           new Film { Id = 4, Title = "Schindler's List", Director = "Steven Spielberg", ReleaseYear = 1993, Genre = "Biography, Drama, History" },
           new Film { Id = 5, Title = "The Lord of the Rings: The Return of the King", Director = "Peter Jackson", ReleaseYear = 2003, Genre = "Adventure, Drama" },
           new Film { Id = 6, Title = "Pulp Fiction", Director = "Quentin Tarantino", ReleaseYear = 1994, Genre = "Crime, Drama" },
           new Film { Id = 7, Title = "The Lord of the Rings: The Fellowship of the Ring", Director = "Peter Jackson", ReleaseYear = 2001, Genre = "Action, Adventure, Drama" },
           new Film { Id = 8, Title = "The Empire Strikes Back", Director = "Irvin Kershner", ReleaseYear = 1980, Genre = "Action, Adventure, Fantasy" },
           new Film { Id = 9, Title = "Forrest Gump", Director = "Robert Zemeckis", ReleaseYear = 1994, Genre = "Drama, Romance" },
           new Film { Id = 10, Title = "Inception", Director = "Christopher Nolan", ReleaseYear = 2010, Genre = "Action, Adventure, Sci-Fi" },
           new Film { Id = 11, Title = "The Matrix", Director = "Lana Wachowski, Lilly Wachowski", ReleaseYear = 1999, Genre = "Action, Sci-Fi" },
           new Film { Id = 12, Title = "Goodfellas", Director = "Martin Scorsese", ReleaseYear = 1990, Genre = "Crime, Drama" },
           new Film { Id = 13, Title = "Fight Club", Director = "David Fincher", ReleaseYear = 1999, Genre = "Drama" },
           new Film { Id = 14, Title = "Forrest Gump", Director = "Robert Zemeckis", ReleaseYear = 1994, Genre = "Drama, Romance" },
           new Film { Id = 15, Title = "The Lion King", Director = "Roger Allers, Rob Minkoff", ReleaseYear = 1994, Genre = "Animation, Adventure, Drama" },
           new Film { Id = 16, Title = "Star Wars: A New Hope", Director = "George Lucas", ReleaseYear = 1977, Genre = "Action, Adventure, Fantasy" },
           new Film { Id = 17, Title = "The Godfather: Part II", Director = "Francis Ford Coppola", ReleaseYear = 1974, Genre = "Crime, Drama" },
           new Film { Id = 18, Title = "The Dark Knight Rises", Director = "Christopher Nolan", ReleaseYear = 2012, Genre = "Action, Drama" },
           new Film { Id = 19, Title = "Se7en", Director = "David Fincher", ReleaseYear = 1995, Genre = "Crime, Drama, Mystery" },
           new Film { Id = 20, Title = "The Silence of the Lambs", Director = "Jonathan Demme", ReleaseYear = 1991, Genre = "Crime, Drama, Thriller" }
       );
        // Lägg till filmstudios
        builder.Entity<FilmStudio>().HasData(
            new FilmStudio { Id = "1", Name = "FilmStudio 1", City = "Stockholm", Role = "filmstudio" },
            new FilmStudio { Id = "2", Name = "FilmStudio 2", City = "Göteborg", Role = "filmstudio" }
         );
        builder.Entity<FilmCopy>().HasData(
            // Film 1 (The Shawshank Redemption)
            new FilmCopy { Id = 1, FilmId = 1, FilmStudioId = "", IsRented = false, TimeWhenRented = null },
            new FilmCopy { Id = 2, FilmId = 1, FilmStudioId = "", IsRented = false, TimeWhenRented = null },
            new FilmCopy { Id = 3, FilmId = 1, FilmStudioId = "", IsRented = false, TimeWhenRented = null },

            // Film 2 (The Godfather) 
            new FilmCopy { Id = 4, FilmId = 2, FilmStudioId = "", IsRented = false, TimeWhenRented = null },
            new FilmCopy { Id = 5, FilmId = 2, FilmStudioId = "", IsRented = false, TimeWhenRented = null },
            new FilmCopy { Id = 6, FilmId = 2, FilmStudioId = "", IsRented = false, TimeWhenRented = null },
            new FilmCopy { Id = 7, FilmId = 2, FilmStudioId = "", IsRented = false, TimeWhenRented = null },

            // Film 3 (The Dark Knight) 
            new FilmCopy { Id = 8, FilmId = 3, FilmStudioId = "", IsRented = false, TimeWhenRented = null },
            new FilmCopy { Id = 9, FilmId = 3, FilmStudioId = "", IsRented = false, TimeWhenRented = null },
            new FilmCopy { Id = 10, FilmId = 3, FilmStudioId = "", IsRented = false, TimeWhenRented = null },
            new FilmCopy { Id = 11, FilmId = 3, FilmStudioId = "", IsRented = false, TimeWhenRented = null },
            new FilmCopy { Id = 12, FilmId = 3, FilmStudioId = "", IsRented = false, TimeWhenRented = null },

            // Film 4 (Pulp Fiction) 
            new FilmCopy { Id = 13, FilmId = 4, FilmStudioId = "", IsRented = false, TimeWhenRented = null },
            new FilmCopy { Id = 14, FilmId = 4, FilmStudioId = "", IsRented = false, TimeWhenRented = null },

            // Film 5 (Forrest Gump) 
            new FilmCopy { Id = 15, FilmId = 5, FilmStudioId = "", IsRented = false, TimeWhenRented = null },
            new FilmCopy { Id = 16, FilmId = 5, FilmStudioId = "", IsRented = false, TimeWhenRented = null },
            new FilmCopy { Id = 17, FilmId = 5, FilmStudioId = "", IsRented = false, TimeWhenRented = null },

            // Film 6 (Inception)
            new FilmCopy { Id = 18, FilmId = 6, FilmStudioId = "", IsRented = false, TimeWhenRented = null },
            new FilmCopy { Id = 19, FilmId = 6, FilmStudioId = "", IsRented = false, TimeWhenRented = null },

            // Film 7 (Fight Club)
            new FilmCopy { Id = 20, FilmId = 7, FilmStudioId = "", IsRented = false, TimeWhenRented = null },
            new FilmCopy { Id = 21, FilmId = 7, FilmStudioId = "", IsRented = false, TimeWhenRented = null },

            // Film 8 (The Matrix) 
            new FilmCopy { Id = 22, FilmId = 8, FilmStudioId = "", IsRented = false, TimeWhenRented = null },
            new FilmCopy { Id = 23, FilmId = 8, FilmStudioId = "", IsRented = false, TimeWhenRented = null },
            new FilmCopy { Id = 24, FilmId = 8, FilmStudioId = "", IsRented = false, TimeWhenRented = null },

            // Film 9 (The Lord of the Rings: The Fellowship of the Ring) 
            new FilmCopy { Id = 25, FilmId = 9, FilmStudioId = "1", IsRented = true, TimeWhenRented = DateTime.Now.AddDays(-5) },
            new FilmCopy { Id = 26, FilmId = 9, FilmStudioId = "2", IsRented = true, TimeWhenRented = DateTime.Now.AddDays(-3) },

            // Film 10 (Interstellar)
            new FilmCopy { Id = 27, FilmId = 10, FilmStudioId = "", IsRented = false, TimeWhenRented = null },
            new FilmCopy { Id = 28, FilmId = 10, FilmStudioId = "2", IsRented = true, TimeWhenRented = DateTime.Now.AddDays(-3) },
            new FilmCopy { Id = 29, FilmId = 10, FilmStudioId = "1", IsRented = true, TimeWhenRented = DateTime.Now.AddDays(-1) },
            // The Matrix 
            new FilmCopy { Id = 30, FilmId = 11, FilmStudioId = "", IsRented = false, TimeWhenRented = null },
            new FilmCopy { Id = 31, FilmId = 11, FilmStudioId = "", IsRented = false, TimeWhenRented = null },
            new FilmCopy { Id = 32, FilmId = 11, FilmStudioId = "", IsRented = false, TimeWhenRented = null },
            new FilmCopy { Id = 33, FilmId = 11, FilmStudioId = "1", IsRented = true, TimeWhenRented = DateTime.Now.AddDays(-3) },

            // Goodfellas
            new FilmCopy { Id = 34, FilmId = 12, FilmStudioId = "", IsRented = false, TimeWhenRented = null },
            new FilmCopy { Id = 35, FilmId = 12, FilmStudioId = "", IsRented = false, TimeWhenRented = null },
            new FilmCopy { Id = 36, FilmId = 12, FilmStudioId = "1", IsRented = true, TimeWhenRented = DateTime.Now.AddDays(-10) },
            new FilmCopy { Id = 37, FilmId = 12, FilmStudioId = "2", IsRented = true, TimeWhenRented = DateTime.Now.AddDays(-5) },

            // Fight Club 
            new FilmCopy { Id = 38, FilmId = 13, FilmStudioId = "", IsRented = false, TimeWhenRented = null },
            new FilmCopy { Id = 39, FilmId = 13, FilmStudioId = "1", IsRented = true, TimeWhenRented = DateTime.Now.AddDays(-7) },

            // Forrest Gump 
            new FilmCopy { Id = 40, FilmId = 14, FilmStudioId = "", IsRented = false, TimeWhenRented = null },
            new FilmCopy { Id = 41, FilmId = 14, FilmStudioId = "", IsRented = false, TimeWhenRented = null },
            new FilmCopy { Id = 42, FilmId = 14, FilmStudioId = "1", IsRented = true, TimeWhenRented = DateTime.Now.AddDays(-1) },

            // The Lion King 
            new FilmCopy { Id = 43, FilmId = 15, FilmStudioId = "", IsRented = false, TimeWhenRented = null },
            new FilmCopy { Id = 44, FilmId = 15, FilmStudioId = "", IsRented = false, TimeWhenRented = null },
            new FilmCopy { Id = 45, FilmId = 15, FilmStudioId = "1", IsRented = true, TimeWhenRented = DateTime.Now.AddDays(-6) },
            new FilmCopy { Id = 46, FilmId = 15, FilmStudioId = "2", IsRented = true, TimeWhenRented = DateTime.Now.AddDays(-2) },

            // Star Wars: A New Hope 
            new FilmCopy { Id = 47, FilmId = 16, FilmStudioId = "", IsRented = false, TimeWhenRented = null },
            new FilmCopy { Id = 48, FilmId = 16, FilmStudioId = "1", IsRented = true, TimeWhenRented = DateTime.Now.AddDays(-8) },
            new FilmCopy { Id = 49, FilmId = 16, FilmStudioId = "", IsRented = false, TimeWhenRented = null },
            new FilmCopy { Id = 50, FilmId = 16, FilmStudioId = "", IsRented = false, TimeWhenRented = null },
            new FilmCopy { Id = 51, FilmId = 16, FilmStudioId = "2", IsRented = true, TimeWhenRented = DateTime.Now.AddDays(-1) },

            // The Godfather: Part II 
            new FilmCopy { Id = 52, FilmId = 17, FilmStudioId = "", IsRented = false, TimeWhenRented = null },

            // The Dark Knight Rises 
            new FilmCopy { Id = 53, FilmId = 18, FilmStudioId = "", IsRented = false, TimeWhenRented = null },
            new FilmCopy { Id = 54, FilmId = 18, FilmStudioId = "1", IsRented = true, TimeWhenRented = DateTime.Now.AddDays(-5) },
            new FilmCopy { Id = 55, FilmId = 18, FilmStudioId = "", IsRented = false, TimeWhenRented = null },

            // Se7en 
            new FilmCopy { Id = 56, FilmId = 19, FilmStudioId = "1", IsRented = true, TimeWhenRented = DateTime.Now.AddDays(-9) },
            new FilmCopy { Id = 57, FilmId = 19, FilmStudioId = "2", IsRented = true, TimeWhenRented = DateTime.Now.AddDays(-3) },
            new FilmCopy { Id = 58, FilmId = 19, FilmStudioId = "", IsRented = false, TimeWhenRented = null },

            // The Silence of the Lambs 
            new FilmCopy { Id = 59, FilmId = 20, FilmStudioId = "", IsRented = false, TimeWhenRented = null },
            new FilmCopy { Id = 60, FilmId = 20, FilmStudioId = "1", IsRented = true, TimeWhenRented = DateTime.Now.AddDays(-1) }
    );
        builder.Entity<FilmCopy>()
        .HasOne(fc => fc.Film)
        .WithMany(f => f.FilmCopies)
        .HasForeignKey(fc => fc.FilmId);

        builder.Entity<FilmCopy>()
        .HasOne(fc => fc.FilmStudio)
        .WithMany(fs => fs.RentedFilmCopies)
        .HasForeignKey(fc => fc.FilmStudioId);
    }
}

