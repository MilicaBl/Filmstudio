using System;

namespace API.Interfaces;

public interface ICreateFilm
{
    string Title { get; set; }
    string Director { get; set; }
    int ReleaseYear { get; set; }
    int NumberOfCopies { get; set; }
}
