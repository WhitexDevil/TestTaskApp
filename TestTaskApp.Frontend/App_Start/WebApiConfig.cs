using System.Web.Http;
using FluentValidation.WebApi;
using TestTaskApp.Frontend.Infrastructure.Authentication;
using TestTaskApp.Frontend.Infrastructure.Filters;

namespace TestTaskApp.Frontend
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.SuppressHostPrincipal();
            config.Filters.Add(new DummyAuthenticationAttrribute());
            config.Filters.Add(new ValidateModelStateFilter());
            
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            FluentValidationModelValidatorProvider.Configure(config);
        }
    }
}