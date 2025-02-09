using System;
using API.Interfaces;
using API.Models.Film;
using Microsoft.AspNetCore.Identity;

namespace API.Models;

public class FilmStudio:IdentityUser, IFilmStudio
{

    public int Id { get; set; }
    public required string FilmStudioName { get; set; }
    public required string City { get; set; }

    //one studio can have many moviecopies
    public List<FilmCopy> RentedFilmCopies { get; set; }= new List<FilmCopy>();
}
