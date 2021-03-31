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
using BCrypt.Net;
using HiddenLove.DataAccess.Repositories;

namespace HiddenLove.Server.Services
{
    public interface IUserService
    {
        AuthenticationResponse Authenticate(AuthenticationRequest model);
        IEnumerable<User> GetAll();
        User GetById(int id);
        AuthenticationResponse Register(RegisterRequest model);
    }

    public class UserService : IUserService
    {
        private UserRepository _dataAccess { get; } 

        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings, UserRepository dataAccess)
        {
            _appSettings = appSettings.Value;
            _dataAccess = dataAccess;
        }

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _dataAccess = new UserRepository();
        }

        public AuthenticationResponse Authenticate(AuthenticationRequest model)
        {
            User userCredentials = _dataAccess.GetByEmailAddress(model.EmailAddress);
            
            if(!IsUserValid(userCredentials, model))
            {
                return null;
            }

            User user = _dataAccess.GetById(userCredentials.Id);

            string token = GerenateJwtToken(user);

            return new AuthenticationResponse
            {
                Id = user.Id,
                Token = token
            };
        } 

        public IEnumerable<User> GetAll() =>
            _dataAccess.GetAll();

        public User GetById(int id) =>
            _dataAccess.GetById(id);

        private bool IsUserValid(User userCredentials, AuthenticationRequest model) =>
            userCredentials != null
            && userCredentials.Passwordhash != null
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

            _dataAccess.Insert(user);

            var authenticationRequest = new AuthenticationRequest
            {
                EmailAddress = model.EmailAddress,
                Password = model.Password
            };

            return Authenticate(authenticationRequest);
        }

        private bool UserExists(string userEmailAddress)
        {
            User user = _dataAccess.GetByEmailAddress(userEmailAddress);

            if(user == null)
                return false;
            else
                return true;
        }
    }
}