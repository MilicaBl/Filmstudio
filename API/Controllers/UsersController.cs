using API.DTOs;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly JwtToken _jwtToken;

        public UsersController(IMapper mapper, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, JwtToken jwtToken)
        {
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtToken = jwtToken;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAdmin([FromBody] UserRegisterDTO model)
        {
            if (model == null)
            {
                return BadRequest(new { message = "Invalid request" });
            }
            var existingUser = await _userManager.FindByNameAsync(model.UserName);
            if (existingUser != null)
            {
                return Conflict(new { message = "Username is already taken" });
            }

            // Create new user
            var user = _mapper.Map<ApplicationUser>(model);
            // Create user in identity
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            // Create role if it does not exists
            if (!await _roleManager.RoleExistsAsync("admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole("admin"));
            }
            // If IsAdmin==true add admin role
            if (model.IsAdmin)
            {
                await _userManager.AddToRoleAsync(user, "admin");
                return Ok(_mapper.Map<UserDTO>(user));
            }
            else return BadRequest("Something went wrong.");
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateDTO model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return Unauthorized(new { message = "Invalid username or password" });
            }
            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault() ?? "user";

            var token = _jwtToken.GenerateJwtToken(user, role);
            if (user is FilmStudio filmStudio)
            {
                var filmStudioDto = _mapper.Map<FilmStudioDTO>(filmStudio);
                var filmStudioAuthenticatedDTO = new FilmStudioAuthenticatedDTO
                {
                    Role = "filmstudio",
                    UserName = user.UserName,
                    Token = token,
                    FilmStudio = filmStudioDto
                };
                return Ok(filmStudioAuthenticatedDTO);
            }
            else if (user is ApplicationUser admin)
            {
                var userAdminAuthenticatedDTO = new UserAdminAuthenticatedDTO
                {
                    Id = user.Id,
                    Role = "admin",
                    Token = token,
                    UserName = user.UserName,
                };
                return Ok(userAdminAuthenticatedDTO);
            }

            return BadRequest(new { message = "User role not recognized" });
        }
    }
}
