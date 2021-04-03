using Microsoft.AspNetCore.Mvc;
using HiddenLove.Shared.Models;
using HiddenLove.Server.Services;
using HiddenLove.Server.Helpers;
using System;
using System.Diagnostics;
using HiddenLove.DataAccess.Entities;
using System.Collections.Generic;
using System.Net;

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
        [Produces("application/json")]
        public IActionResult Authenticate(AuthenticationRequest model)
        {
            AuthenticationResponse response = UserService.Authenticate(model);

            if(response == null)
                return BadRequest(new HttpError("Wrong username or password.", HttpStatusCode.BadRequest));

            return Ok(response);
        }

        [HttpPost("register")]
        [Produces("application/json")]
        public IActionResult Register(RegisterRequest model)
        {
            var response = UserService.Register(model);

            if(response == null)
                return BadRequest(new HttpError("Invalid email address."));
                
            return Ok(response);
        }
    }
}