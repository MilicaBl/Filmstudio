using System;
using API.Interfaces;

namespace API.DTOs;

public class RegisterFilmStudioDTO
{
    public required string UserName { get; set; }
    public required string FilmStudioName { get; set; }
    public required string City { get; set; }
    public required string Password { get; set; }
}
