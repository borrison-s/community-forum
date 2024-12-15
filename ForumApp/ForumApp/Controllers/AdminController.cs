using ForumApp.Dtos.Users;
using ForumApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace ForumApp.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService;

        public AdminController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet]
        public ActionResult<List<UserDto>> GetAll()
        {
            try
            {
                //var identity = HttpContext.User.Identity as ClaimsIdentity;
                //var claims = identity.Claims;

                //if(identity.FindFirst("userRole").Value != "Admin")
                //{
                //    return StatusCode(StatusCodes.Status403Forbidden);
                //}

                //throw new Exception("Our error");

                return Ok(_userService.GetAllUsers());
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred");
            }
        }

    }
}
