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
using HiddenLove.Server.Models;
using BCrypt.Net;
using HiddenLove.DataAccess.Repositories;

namespace HiddenLove.Server.Services
{
    public interface IUserService
    {
        AuthenticationResponse Authenticate(AuthenticationRequest model);
        IEnumerable<User> GetAll();
        User GetById(int id);
    }

    public class UserService : IUserService
    {
        private UserRepository _dataAccess { get; } 

        private readonly AppSettings _appSettings;

        public UserService(UserRepository dataAccess, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _dataAccess = dataAccess;
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

            return new AuthenticationResponse(user, token);
        } 

        public IEnumerable<User> GetAll() =>
            _dataAccess.GetAll();

        public User GetById(int id) =>
            _dataAccess.GetById(id);

        private bool IsUserValid(User userCredentials, AuthenticationRequest model) =>
            userCredentials != null
            && userCredentials.PasswordHash != null
            && BCrypt.Net.BCrypt.Verify(model.Password, userCredentials?.PasswordHash);

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
    }
}