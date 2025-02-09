using API.DTOs;
using API.Interfaces.IRepositories;
using API.Models.Film;
using API.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmsController : ControllerBase
    {
        private readonly IFilmRepository _filmRepository;
        private readonly IFilmCopyRepository _filmCopyRepository;
        private readonly IMapper _mapper;
         private readonly AppDbContext _context;
        public FilmsController(AppDbContext context, IFilmRepository filmRepository, FilmCopyRepository filmCopyRepository, IMapper mapper)
        {
            _context=context;
            _filmRepository = filmRepository;
            _filmCopyRepository = filmCopyRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllFilms()
        {
            var films = await _filmRepository.GetAllFilms();
            return Ok(films);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFilmById(int id)
        {
            var film = await _filmRepository.GetFilmById(id);
            if (film == null) return NotFound("Film not found.");
            return Ok(film);
        }
        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateFilm([FromBody] CreateFilmDTO filmDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var film = _mapper.Map<Film>(filmDTO);
             _filmRepository.AddNewFilm(film);

            if (filmDTO.NumberOfCopies > 0)
            {
                await _filmCopyRepository.AddNewFilmCopy(film, filmDTO.NumberOfCopies);
            }
            await _context.SaveChangesAsync();
            // Hämta filmen igen och mappa till DTO med kopior
            var createdFilm = await _filmRepository.GetFilmById(film.Id);
            var responseDto = _mapper.Map<FilmWithCopiesDTO>(createdFilm);

            return Ok(responseDto);

        }
        [HttpPatch("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateFilm(int id, [FromBody] UpdateFilmDTO updateFilmDTO)
        {
            var updatedFilm = await _filmRepository.Update(id, updateFilmDTO);
            if (updatedFilm == null) return NotFound("Movie not found.");
            return Ok(updatedFilm);
        }
    }
}
