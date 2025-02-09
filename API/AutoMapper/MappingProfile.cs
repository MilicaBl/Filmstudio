using System;
using API.DTOs;
using API.Models;
using API.Models.Film;
using AutoMapper;

namespace API;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Film, FilmDTO>();
        CreateMap<CreateFilmDTO, Film>()
          .ForMember(dest => dest.FilmCopies, opt => opt.Ignore()); // Kopior skapas separat
        CreateMap<UpdateFilmDTO, Film>()
            .ForMember(dest => dest.FilmCopies, opt => opt.Ignore());
        CreateMap<Film, FilmWithCopiesDTO>();
        CreateMap<FilmCopy, FilmCopyDTO>();
        CreateMap<FilmStudio, FilmStudioDTO>();
        CreateMap<FilmStudio, FilmStudioMinimalDTO>();
    }
}
