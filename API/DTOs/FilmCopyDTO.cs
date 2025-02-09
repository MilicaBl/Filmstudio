using System;
using API.Interfaces;

namespace API.DTOs;

public class FilmCopyDTO
{
    public int Id{get; set;}
    public bool IsRented{get; set;}
    public DateTime? TimeWhenRented{get; set;}

}
