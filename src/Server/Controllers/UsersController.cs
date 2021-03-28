using Microsoft.AspNetCore.Mvc;
using HiddenLove.Shared.Models.Authentication;
using HiddenLove.Server.Services;
using HiddenLove.Server.Helpers;
using System;
using System.Diagnostics;
using HiddenLove.DataAccess.Entities;
using System.Collections.Generic;

namespace HiddenLove.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService UserService;

        public UsersController(IUserService userService)
        {
            UserService = userService;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticationRequest model)
        {
            var response = UserService.Authenticate(model);

            if(response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [Authorize]
        [HttpGet]
        public IEnumerable<User> GetAll()
        {
            // ((User)HttpContext.Items["User"]).FullUserName;
            return UserService.GetAll();
        }

        [Authorize]
        [HttpGet("currentuser")]
        public User GetCurrentUser()
        {
            return (User)HttpContext.Items["User"];
        }
    }
}