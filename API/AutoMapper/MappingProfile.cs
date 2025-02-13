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
          .ForMember(dest => dest.FilmCopies, opt => opt.Ignore()); // copies are created separately
        CreateMap<UpdateFilmDTO, Film>()
            .ForMember(dest => dest.FilmCopies, opt => opt.Ignore());
        CreateMap<Film, FilmWithCopiesDTO>();
        CreateMap<FilmCopy, FilmCopyDTO>();
        CreateMap<FilmCopyDTO, FilmCopy>();
        CreateMap<FilmStudio, FilmStudioDTO>();
        CreateMap<FilmStudio, FilmStudioMinimalDTO>();
        CreateMap<FilmStudio, FilmStudioAuthenticatedDTO>()
          .ForMember(dest => dest.FilmStudio, opt => opt.MapFrom(src => src));
        CreateMap<RegisterFilmStudioDTO, FilmStudio>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FilmStudioName))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => "filmstudio"))
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
        CreateMap<ApplicationUser, UserDTO>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Role, opt => opt.Ignore());
        CreateMap<UserRegisterDTO, ApplicationUser>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName));
    }
}
