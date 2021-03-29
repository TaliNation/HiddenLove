using Microsoft.AspNetCore.Mvc;
using HiddenLove.Shared.Models;
using HiddenLove.Server.Services;
using HiddenLove.Server.Helpers;
using System;
using System.Diagnostics;
using HiddenLove.Shared.Entities;
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
            AuthenticationResponse response = UserService.Authenticate(model);

            if(response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterRequest model)
        {
            var response = UserService.Register(model);

            if(response == null)
                throw new Exception("Erreur");
                
            return Ok(response);
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(UserService.GetAll());
        }

        [Authorize]
        [HttpGet("currentuser")]
        public IActionResult GetCurrentUser()
        {
            return Ok((User)HttpContext.Items["User"]);
        }
    }
}