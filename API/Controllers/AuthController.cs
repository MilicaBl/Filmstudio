using API.DTOs;
using API.Interfaces.IRepositories;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IFilmStudioRepository _filmStudioRepository;
        private readonly UserManager<FilmStudio> _userManager;
        private readonly SignInManager<FilmStudio> _signInManager;
        private readonly IMapper _mapper;

        public AuthController(IFilmStudioRepository filmStudioRepository, IMapper mapper, UserManager<FilmStudio> userManager, SignInManager<FilmStudio> signInManager)
        {
            _filmStudioRepository = filmStudioRepository;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpPost("api/filmstudio/register")]
        public async Task<IActionResult> RegisterFilmStudio([FromBody] RegisterFilmStudioDTO model)
        {
            if (model == null)
            {
                return BadRequest(new { message = "Invalid request" });
            }
            var filmStudioUser= _mapper.Map<FilmStudio>(model);
            var result = await _userManager.CreateAsync(filmStudioUser, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(filmStudioUser,"FilmStudio");
                return Ok(new{ message="Filmstudio registered successfully"});
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }
    }
}
