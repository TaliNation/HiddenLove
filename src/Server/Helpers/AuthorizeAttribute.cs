using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using HiddenLove.DataAccess.Entities;
using HiddenLove.Shared.Enums;

namespace HiddenLove.Server.Helpers
{
    /// <summary>
    /// Gestion des accès à certaines ressources
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly CustomerTier? MinimumTier;

        public AuthorizeAttribute()
        {
            MinimumTier = null;
        }

        public AuthorizeAttribute(CustomerTier minimumTier)
        {
            MinimumTier = minimumTier;
        }

        /// <summary>
        /// Vérifier qu'un utilisateur est authentifié
        /// </summary>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = (User)context.HttpContext.Items["User"];
            if (user == null || IsLackingPrivileges(user.Id_Privilege))
            {
                context.Result = new JsonResult(new { Message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }

        private bool IsLackingPrivileges(int? userPrivileges)
        {
            if(!MinimumTier.HasValue)
                return false;

            if(!userPrivileges.HasValue)
                return true;

            return userPrivileges.Value > (int)MinimumTier.Value;
        }
    }
}