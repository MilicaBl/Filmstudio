using System;
using System.ComponentModel.DataAnnotations.Schema;
using API.Interfaces;
using API.Models.Film;
using Microsoft.AspNetCore.Identity;

namespace API.Models;

public class FilmStudio : ApplicationUser, IFilmStudio
{
    [NotMapped]
    public string FilmStudioId => Id;
    public required string Name { get; set; }
    public required string City { get; set; }
    public new string Role { get; set; } = "filmstudio";

    //one studio can have many moviecopies
    public List<FilmCopy> RentedFilmCopies { get; set; } = new List<FilmCopy>();
}
