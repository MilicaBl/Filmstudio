using System;
using API.Models.Film;
using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API;

public class AppDbContext:IdentityDbContext<IdentityUser>
{
     public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        this.Database.EnsureCreated();
    }
    public DbSet<Film> Films{get; set;}
    public DbSet<FilmStudio> FilmStudios{get; set;}
    public DbSet<FilmCopy> FilmCopies{get; set;}
    public DbSet<ApplicationUser> Users{get; set;} //titta pa detta senare
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<FilmCopy>()
        .HasOne(fc => fc.Film)
        .WithMany(f=> f.FilmCopies)
        .HasForeignKey(fc => fc.Id);

        builder.Entity<FilmCopy>()
        .HasOne(fc => fc.FilmStudio)
        .WithMany(fs=>fs.RentedFilmCopies)
        .HasForeignKey(fc=>fc.FilmStudioId);
    }
}

