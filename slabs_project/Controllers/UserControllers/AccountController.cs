using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using slabs_project.Models.DTOs.UserDTOs;
using slabs_project.Models.Entities;
using slabs_project.Services.UserServices;

namespace slabs_project.Controllers.UserControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;

        public AccountController(
            UserManager<User> userManager,
            IUserService userService
            )
        {
            _userManager = userManager;
            _userService = userService;
        }


        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO dto)
        {
            var exists = await _userManager.FindByEmailAsync(dto.Email);
            if (exists != null)
            {
                return BadRequest("A user with the specified e-mail is already registered!");
            }

            var result = await _userService.RegisterUserAsync(dto);
            if (result)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("User already registered!");
            }

            return BadRequest();
        }


        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO dto)
        {
            if (!ModelState.IsValid && dto != null)
                return BadRequest();

            var token = await _userService.LoginUser(dto);
            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(new { token });
        }
    }
}
