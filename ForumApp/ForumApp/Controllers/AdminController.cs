﻿using ForumApp.Dtos.Users;
using ForumApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult<List<UserDto>> GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }
    }
}