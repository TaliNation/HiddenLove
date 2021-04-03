using HiddenLove.Server.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace HiddenLove.Server.Extensions
{
    public static class MvcOptionsExtensions
    {
        /// <summary>
        /// Ajout d'un pefixe qui précedera toutes les routes vers les API
        /// </summary>
        public static void UseGeneralRoutePrefix(this MvcOptions opts, IRouteTemplateProvider routeAttribute) =>
            opts.Conventions.Add(new RoutePrefixConvention(routeAttribute));

        /// <summary>
        /// Wrapper de <see cref="UseGeneralRoutePrefix(MvcOptions, IRouteTemplateProvider)"/>
        /// </summary>
        /// <param name="prefix">Ne doit pas terminer par "/"</param>
        public static void UseGeneralRoutePrefix(this MvcOptions opts, string prefix)
        {
            prefix = RemoveEndSlash(prefix);
            opts.UseGeneralRoutePrefix(new RouteAttribute(prefix));
        }

        private static string RemoveEndSlash(string s) => 
            s.EndsWith('/') ? s.Substring(0, s.Length - 2) : s;
    }
}