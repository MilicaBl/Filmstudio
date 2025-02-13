using System;
using API.Interfaces;

namespace API.DTOs;

public class FilmCopyDTO
{
    public int Id { get; set; }
    public bool IsRented { get; set; }
    public int FilmId { get; set; }
    public DateTime? TimeWhenRented { get; set; }
    public FilmDTO Film { get; set; }
}
