using System.Security.Claims;
using API.DTOs;
using API.Interfaces;
using API.Interfaces.IRepositories;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmStudioController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IFilmStudioRepository _filmStudio;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public FilmStudioController(IMapper mapper, IFilmStudioRepository filmStudioRepository, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _mapper = mapper;
            _filmStudio = filmStudioRepository;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterFilmStudio([FromBody] RegisterFilmStudioDTO model)
        {
            if (model == null)
            {
                return BadRequest(new { message = "Invalid request" });
            }
            var existingFilmStudio = await _userManager.FindByNameAsync(model.UserName);
            if (existingFilmStudio != null)
            {
                return Conflict(new { message = "Username is already taken" });
            }

            var filmStudioUser = _mapper.Map<FilmStudio>(model);

            var result = await _userManager.CreateAsync(filmStudioUser, model.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            // Create roles if they do not exist
            if (!await _roleManager.RoleExistsAsync("filmstudio"))
            {
                await _roleManager.CreateAsync(new IdentityRole("filmstudio"));
            }
            await _userManager.AddToRoleAsync(filmStudioUser, "filmstudio");

            return Ok(_mapper.Map<FilmStudioDTO>(filmStudioUser));
        }

        [HttpGet("/api/filmstudios")]
        public async Task<IActionResult> GetFilmStudios()
        {
            if (User.IsInRole("admin"))
            {
                var studios = await _filmStudio.GetAllFullFilmStudios();
                return Ok(new { message = "admin", studios });
            }
            else
            {
                var studios = await _filmStudio.GetAllMiniFilmStudios();
                return Ok(studios);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFilmStudioById(string id)
        {
            var filmStudio = await _filmStudio.GetFullFilmStudioById(id);
            if (filmStudio == null)
            {
                return NotFound(new { message = "Filmstudio not found" });
            }
            string? userFilmStudioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if ((!string.IsNullOrEmpty(userFilmStudioId) && userFilmStudioId == id) || User.IsInRole("admin"))
            {
                return Ok(new List<FilmStudioDTO> { filmStudio });
            }

            var minimalStudioDTO = await _filmStudio.GetMiniFilmStudioById(id);
            return Ok(new List<FilmStudioMinimalDTO> { minimalStudioDTO });
        }

        [Authorize(Roles = "filmstudio")]
        [HttpGet("/api/mystudio/rentals")]
        public async Task<IActionResult> GetRentedFilmsForFilmStudio()
        {
            var userFilmStudioId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userFilmStudioId))
            {
                return Unauthorized();
            }
            var rentedFilms = await _filmStudio.GetRentedFilmsForFilmStudio(userFilmStudioId);

            return Ok(rentedFilms);
        }
    }
}
