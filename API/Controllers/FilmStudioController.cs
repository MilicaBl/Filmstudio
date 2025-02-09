using API.DTOs;
using API.Interfaces;
using API.Interfaces.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmStudioController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IFilmStudioRepository _filmStudio;
        private readonly IFilmRepository _film;
        public FilmStudioController(IMapper mapper, IFilmStudioRepository filmStudioRepository, IFilmRepository filmRepository)
        {
            _mapper = mapper;
            _filmStudio = filmStudioRepository;
            _film = filmRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetFilmStudios()
        {

            if (User.IsInRole("Admin"))
            {
                var studios = await _filmStudio.GetAllFullFilmStudios();
                return Ok(studios);
            }
            else
            {
                var studios = await _filmStudio.GetAllMiniFilmStudios();
                return Ok(studios);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFilmStudioById(int id)
        {
            var filmStudio = await _filmStudio.GetFullFilmStudioById(id);
            if (filmStudio == null)
            {
                return NotFound(new { message = "Filmstudio not found" });
            }
            int? userFilmStudioId = FilmStudioHelpers.GetUserFilmStudioId(User);


            if ((userFilmStudioId.HasValue && userFilmStudioId.Value == id) || User.IsInRole("Admin"))
            {
                return Ok(new List<FilmStudioDTO> { filmStudio });
            }
            var minimalStudioDTO = await _filmStudio.GetMiniFilmStudioById(id);
            return Ok(new List<FilmStudioMinimalDTO> { minimalStudioDTO });

        }
        //[Authorize(Roles = "FilmStudio")]
        [HttpGet("/api/mystudio/rentals")]
        public async Task<IActionResult> GetRentedFilmsForFilmStudio()
        {
            var userFilmStudioId = FilmStudioHelpers.GetUserFilmStudioId(User);
            if (!userFilmStudioId.HasValue)
            {
                return Unauthorized();
            }
            var rentedFilms = await _filmStudio.GetRentedFilmsForFilmStudio(userFilmStudioId.Value);

            return Ok(rentedFilms);
        }
        //[Authorize(Roles = "FilmStudio")]
        [HttpPost("/api/films/rent")]
        public async Task<IActionResult> RentFilm([FromQuery] int id, [FromQuery] int studioid)
        {
            var userFilmStudioId = FilmStudioHelpers.GetUserFilmStudioId(User);
            if (!userFilmStudioId.HasValue || userFilmStudioId != studioid)
            {
                return Unauthorized();
            }
            //Get movie
            var film = await _film.GetFilmWithCopiesById(id);
            if (film == null)
            {
                return Conflict(new { message = "Film not found." }); // Status 409
            }

            var availableCopy = film.FilmCopies?.FirstOrDefault(fc => !fc.IsRented);
            if (availableCopy == null)
            {
                return Conflict(new { message = "Det finns inga lediga kopior." }); // Status 409
            }
            var studio = await _filmStudio.GetFullFilmStudioById(studioid);
            if (studio == null)
            {
                return Conflict(new { message = "Filmstudio not found." }); // Status 409
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
        //[Authorize(Roles = "FilmStudio")]
        [HttpPost("/api/films/return")]
        public async Task<IActionResult> ReturnFilm([FromQuery] int id, [FromQuery] int studioid)
        {
            var userFilmStudioId = FilmStudioHelpers.GetUserFilmStudioId(User);
            if (!userFilmStudioId.HasValue || userFilmStudioId != studioid)
            {
                return Unauthorized();
            }
            //Get movie
            var film = await _film.GetFilmWithCopiesById(id);
            if (film == null)
            {
                return Conflict(new { message = "Film not found." }); // Status 409
            }
            var studio = await _filmStudio.GetFullFilmStudioById(studioid);
            if (studio == null)
            {
                return Conflict(new { message = "Filmstudio not found." }); // Status 409
            }
            var rentedCopy = studio.RentedFilmCopies.FirstOrDefault(fc => fc.FilmId == id);

            if (rentedCopy == null)
            {
                return Conflict(new { message = "Filmstudio has not rented this film." }); // Status 409
            }
            var rentSuccess = await _filmStudio.RentFilm(studio, _mapper.Map<FilmCopyDTO>(rentedCopy));

            if (!rentSuccess)
            {
                return StatusCode(409, new { message = "Something went wrong and you could not rent this film." });
            }

            return Ok(new { message = "Film rented successfuly." });
        }
    }
}
