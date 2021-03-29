using System.Linq;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Routing;

namespace HiddenLove.Server.Helpers
{
    public class RoutePrefixConvention : IApplicationModelConvention
    {
        private readonly AttributeRouteModel RoutePrefix;

        public RoutePrefixConvention(IRouteTemplateProvider route)
        {
            RoutePrefix = new AttributeRouteModel(route);
        }

        public void Apply(ApplicationModel application)
        {
            foreach (var selector in application.Controllers.SelectMany(c => c.Selectors))
            {
                if(selector.AttributeRouteModel != null)
                {
                    selector.AttributeRouteModel = AttributeRouteModel.CombineAttributeRouteModel(RoutePrefix, selector.AttributeRouteModel);
                }
                else
                {
                    selector.AttributeRouteModel = RoutePrefix;
                }
            }
        }
    }
}