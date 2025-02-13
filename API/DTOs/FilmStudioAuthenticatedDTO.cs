using System;
using API.Interfaces;

namespace API.DTOs;

public class FilmStudioAuthenticatedDTO
{
    public required string UserName { get; set; }
    public required string Role { get; set; }
    public FilmStudioDTO FilmStudio { get; set; }
    public string Token { get; set; } = string.Empty;
}
