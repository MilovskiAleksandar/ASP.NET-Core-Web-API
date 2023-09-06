using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEDC.MovieApp.Refactored.DTOs.Users;
using SEDC.MovieApp.Refactored.Service.Interfaces;
using SEDC.MovieApp.Refactored.Shared.Shared;

namespace SEDC.MovieApp.Refactored.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public IActionResult RegisterUser([FromBody] RegisterUserDto registerUserDto)
        {
            try
            {
                _userService.Registration(registerUserDto);
                return Ok("Succefull register");
            }
            catch (ResourceNotFoundException)
            {
                return BadRequest("You cant register!");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred");
            }
        }

        [HttpPost("login")]
        public ActionResult<string> Login([FromBody] LoginUserDto loginUserDto)
        {
            try
            {
                string token = _userService.Login(loginUserDto);
                return Ok(token);
            }
            catch (ResourceNotFoundException)
            {
                return NotFound("User was not found");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred");
            }
        }
    }
}
