using System.Security.Claims;
using API.DTOs;
using API.Interfaces.IRepositories;
using API.Models.Film;
using API.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmsController : ControllerBase
    {
        private readonly IFilmRepository _film;
        private readonly IFilmStudioRepository _filmStudio;
        private readonly IFilmCopyRepository _filmCopy;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        public FilmsController(AppDbContext context, IFilmRepository filmRepository, IFilmStudioRepository filmStudioRepository, IFilmCopyRepository filmCopyRepository, IMapper mapper)
        {
            _context = context;
            _film = filmRepository;
            _filmStudio = filmStudioRepository;
            _filmCopy = filmCopyRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFilms()
        {
            if (User.IsInRole("admin") || User.IsInRole("filmstudio"))
            {
                var films = await _film.GetAllFilmsWithCopies();
                return Ok(films);
            }
            else
            {
                var films = await _film.GetAllFilms();
                return Ok(films);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFilmById(int id)
        {
            if (User.IsInRole("admin") || User.IsInRole("filmstudio"))
            {
                var film = await _film.GetFilmWithCopiesById(id);
                if (film == null) return NotFound("Film not found.");
                return Ok(film);
            }
            else
            {
                var film = await _film.GetFilmById(id);
                if (film == null) return NotFound("Film not found.");
                return Ok(film);
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> CreateFilm([FromBody] CreateFilmDTO filmDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var film = _mapper.Map<Film>(filmDTO);
            _film.AddNewFilm(film);

            if (filmDTO.NumberOfCopies > 0)
            {
                await _filmCopy.AddNewFilmCopy(film, filmDTO.NumberOfCopies);
            }
            await _context.SaveChangesAsync();
            // Get the movie again and map it to DTO with copies
            var createdFilm = await _film.GetFilmWithCopiesById(film.Id);
            return Ok(createdFilm);
        }

        [Authorize(Roles = "filmstudio")]
        [HttpPost("rent")]
        public async Task<IActionResult> RentFilm([FromQuery] int id, [FromQuery] string studioid)
        {
            var userFilmStudioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userFilmStudioId) || userFilmStudioId != studioid)
            {
                return Unauthorized();
            }
            //Get movie
            var film = await _film.GetFilmWithCopiesById(id);
            if (film == null)
            {
                return Conflict(new { message = "Film not found." }); 
            }

            var availableCopy = film.FilmCopies?.FirstOrDefault(fc => !fc.IsRented);
            if (availableCopy == null)
            {
                return Conflict(new { message = "There are no available copies." }); 
            }
            var studio = await _filmStudio.GetFullFilmStudioById(userFilmStudioId);
            if (studio == null)
            {
                return Conflict(new { message = "Filmstudio not found." });
            }
            if (studio.RentedFilmCopies.Any(fc => fc.FilmId == id))
            {
                return StatusCode(403, new { message = "Filmstudio already rents this film" });
            }
            // Anropa repository för att hantera filmhyrning och databasen
            var rentSuccess = await _filmStudio.RentFilm(studio, availableCopy);

            if (!rentSuccess)
            {
                return StatusCode(409, new { message = "Something went wrong and you could not rent this film." });
            }

            return Ok(new { message = "Film rented successfuly." });
        }

        [Authorize(Roles = "filmstudio")]
        [HttpPost("return")]
        public async Task<IActionResult> ReturnFilm([FromQuery] int id, [FromQuery] string studioid)
        {
            var userFilmStudioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userFilmStudioId) || userFilmStudioId != studioid)
            {
                return Unauthorized();
            }
            //Get movie
            var film = await _film.GetFilmWithCopiesById(id);
            if (film == null)
            {
                return Conflict(new { message = "Film not found." }); 
            }
            var studio = await _filmStudio.GetFullFilmStudioById(userFilmStudioId);
            if (studio == null)
            {
                return Conflict(new { message = "Filmstudio not found." }); // Status 409
            }
            var rentedCopy = studio.RentedFilmCopies.FirstOrDefault(fc => fc.FilmId == id);

            if (rentedCopy == null)
            {
                return Conflict(new { message = "Filmstudio has not rented this film." });
            }
            var rentSuccess = await _filmStudio.ReturnFilm(studio, _mapper.Map<FilmCopyDTO>(rentedCopy));

            if (!rentSuccess)
            {
                return StatusCode(409, new { message = "Something went wrong and you could not rent this film." });
            }

            return Ok(new { message = "Film returned successfuly." });
        }

        [Authorize(Roles = "admin")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateFilm(int id, [FromBody] UpdateFilmDTO updateFilmDTO)
        {
            var updatedFilm = await _film.Update(id, updateFilmDTO);
            if (updatedFilm == null) return NotFound("Movie not found.");
            
            var result = await _film.GetFilmWithCopiesById(id);
            return Ok(result);
        }
    }
}
