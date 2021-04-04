using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using HiddenLove.DataAccess.Entities;
using HiddenLove.Server.Helpers;
using HiddenLove.Shared.Models;
using HiddenLove.DataAccess.TableAccesses;
using HiddenLove.DataAccess.Repositories;

namespace HiddenLove.Server.Services
{
    /// <summary>
    /// Service d'authentification des utilisateurs
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Authentification de l'utilisateur
        /// </summary>
        AuthenticationResponse Authenticate(AuthenticationRequest model);

        /// <summary>
        /// Récupération de l'utilisateur par son ID
        /// </summary>
        User GetById(int id);

        /// <summary>
        /// Inscription et authentification de l'utilisateur
        /// </summary>
        AuthenticationResponse Register(RegisterRequest model);
    }

    /// <summary>
    /// Service d'authentification des utilisateurs
    /// </summary>
    public class UserService : IUserService
    {
        private Repository DataAccess { get; }

        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings, Repository dataAccess)
        {
            _appSettings = appSettings.Value;
            DataAccess = dataAccess;
        }

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            DataAccess = new Repository(new UsersTableAccess());
        }

        public AuthenticationResponse Authenticate(AuthenticationRequest model)
        {
            User userCredentials = DataAccess.GetByColumn<int, User>("EmailAddress", model.EmailAddress).FirstOrDefault();

            if(!IsUserValid(userCredentials, model))
            {
                return null;
            }

            User user = DataAccess.GetById<int, User>(userCredentials.Id);

            string token = GerenateJwtToken(user);

            return new AuthenticationResponse
            {
                Id = user.Id,
                Token = token
            };
        }

        public User GetById(int id) =>
            DataAccess.GetById<int, User>(id);

        private static bool IsUserValid(User userCredentials, AuthenticationRequest model) =>
            userCredentials?.Passwordhash != null
            && model.Password != null
            && BCrypt.Net.BCrypt.Verify(model.Password, userCredentials?.Passwordhash);

        private string GerenateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public AuthenticationResponse Register(RegisterRequest model)
        {
            var random = new Random();

            var user = new User
            {
                EmailAddress = model.EmailAddress,
                Username = model.UserName,
                FullUsername = model.UserName + "#" + random.Next(0, 10000),
                Passwordhash = BCrypt.Net.BCrypt.HashPassword(model.Password)
            };

            if(UserExists(user.EmailAddress))
                return null;

            DataAccess.Insert<int, User>(user);

            var authenticationRequest = new AuthenticationRequest
            {
                EmailAddress = model.EmailAddress,
                Password = model.Password
            };

            return Authenticate(authenticationRequest);
        }

        private bool UserExists(string userEmailAddress)
        {
            User user = DataAccess.GetByColumn<int, User>("EmailAddress", userEmailAddress).FirstOrDefault();

            return user != null;
        }
    }
}