using System;
using API.Interfaces;

namespace API.DTOs;

public class FilmStudioMinimalDTO
{
    public int Id { get; set; }
    public  required string  FilmStudioName { get; set; }

}
