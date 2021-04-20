using Microsoft.AspNetCore.Mvc;
using HiddenLove.Shared.Models;
using HiddenLove.Server.Services;
using HiddenLove.Server.Helpers;
using System;
using System.Diagnostics;
using HiddenLove.DataAccess.Entities;
using System.Collections.Generic;
using System.Net;
using HiddenLove.Shared.Enums;

namespace HiddenLove.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private User CurrentUser => (User)HttpContext.Items["User"];

        private readonly IUserService UserService;

        public UsersController(IUserService userService)
        {
            UserService = userService;
        }

        /// <summary>
        /// Authentification de l'utilisateur
        /// </summary>
        [HttpPost("authenticate")]
        [Produces("application/json")]
        public IActionResult Authenticate(AuthenticationRequest model)
        {
            AuthenticationResponse response = UserService.Authenticate(model);

            if(response == null)
                return BadRequest(new HttpError("Wrong username or password.", HttpStatusCode.BadRequest));

            return Ok(response);
        }

        /// <summary>
        /// Inscription d'un utilisateur et authentication avec son nouveau compte
        /// </summary>
        [HttpPost("register")]
        [Produces("application/json")]
        public IActionResult Register(RegisterRequest model)
        {
            var response = UserService.Register(model);

            if(response == null)
                return BadRequest(new HttpError("Invalid email address."));

            return Ok(response);
        }

        [Authorize]
        [HttpGet("[action]")]
        [Produces("application/json")]
        public IActionResult GetCurrentUser()
        {
            return Ok(CurrentUser);
        }
    }
}