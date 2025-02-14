using System;
using API.Interfaces;

namespace API.DTOs;

public class FilmStudioMinimalDTO
{
    public required string FilmStudioId { get; set; }
    public required string Name { get; set; }

}
