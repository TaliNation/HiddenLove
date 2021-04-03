using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiddenLove.Server.Services;

namespace HiddenLove.Server.Helpers
{
    /// <summary>
    /// Identification d'un utilisateur via son JWT
    /// </summary>
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Récupération du token et identification de l'utilisateur
        /// </summary>
        public async Task Invoke(HttpContext httpContext, IUserService userService)
        {
            var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if(token != null)
                AttachUserToContext(httpContext, userService, token);

            await _next(httpContext);
        }

        /// <summary>
        /// Attachement de l'utilisateur au contexte Http pour pouvoir l'identifier dans les API et lui autoriser les accès
        /// </summary>
        private void AttachUserToContext(HttpContext httpContext, IUserService userService, string token)
        {
            try
            {
                var tokenHandler =new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

                httpContext.Items["User"] = userService.GetById(userId);
            }
            catch
            {
                
            }
        }
    }
}