using ForumApp.Dtos.Users;
using ForumApp.Services.Interfaces;
using ForumApp.Shared.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ForumApp.Controllers
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
                _userService.RegisterUser(registerUserDto);
                return StatusCode(StatusCodes.Status201Created, "User was created");
            }
            catch (Exception ex)
            {
                // Handle exceptions and return appropriate responses
                if (ex is DuplicateEntityException)
                {
                    return Conflict(new { Error = ex.Message });
                }

                if (ex is ValidationException)
                {
                    return BadRequest(new { Error = ex.Message });
                }

                return StatusCode(500, new { Error = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        [AllowAnonymous] //client doesn't have to provide a token
        [HttpPost("login")]
        //the response will contain the token, and the token is a string
        public ActionResult<string> Login([FromBody] LoginUserDto loginUserDto)
        {
            try
            {
                string token = _userService.LoginUser(loginUserDto);
                return Ok(token);
            }
            catch (Exception e)
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred, contact the admin!");
            }

        }
    }
}
