using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using slabs_project.Models.DTOs.UserDTOs;
using slabs_project.Models.Entities;
using slabs_project.Repositories;
using slabs_project.Services.UserServices;

namespace slabs_project.Controllers.UserControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;

        public UserController(IRepositoryWrapper repository,
                               UserManager<User> userManager,
                               IUserService userService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("getUserByIdWithRoles/{id}")]
        public async Task<IActionResult> GetUserByIdWithRoles([FromRoute] int id)
        {
            var user = await _repository.User.GetUserByIdWithRoles(id);
            if (user == null)
            {
                return NotFound("The user with the specified id does not exist!");
            }

            return Ok(new { user });
        }


        [Authorize]
        [HttpGet("getUserByEmail/{email}")]
        public async Task<IActionResult> GetUserByEmail([FromRoute] string email)
        {
            var user = await _repository.User.GetUserByEmail(email);
            if (user == null)
            {
                return NotFound("The user with the specified e-mail does not exist!");
            }

            return Ok(new { user });
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("getAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _repository.User.GetAllUsers();

            return Ok(new { users });
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] RegisterUserDTO dto)
        {
            var newUser = await _userService.RegisterUserAsync(dto);
            if (newUser)
            {
                return BadRequest("User already registered!");
            }

            return Ok(new { newUser });
        }


        [Authorize(Roles = "Admin")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchUser([FromRoute] int id, [FromBody] JsonPatchDocument<User> user)
        {
            var userToUpdate = await _repository.User.GetByIdAsync(id);
            if (userToUpdate == null)
            {
                return NotFound("The user with the specified ID does not exist!");
            }

            user.ApplyTo(userToUpdate, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _repository.User.Update(userToUpdate);

            await _repository.SaveAsync();

            return Ok(userToUpdate);
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromQuery] int id)
        {
            var user = await _repository.User.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound("The user with the specified id does not exist!");
            }
            await _userManager.DeleteAsync(user);

            return NoContent();
        }
    }
}
