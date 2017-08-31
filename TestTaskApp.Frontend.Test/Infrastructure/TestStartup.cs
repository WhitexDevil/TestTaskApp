using System.Web.Http;
using System.Web.Http.Dispatcher;
using Owin;
using TestTaskApp.Frontend.App_Start;
using TestTaskApp.Frontend.Infrastructure.Authentication;

namespace TestTaskApp.Frontend.Test.Infrastructure
{
    public class TestStartup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            config.Services.Replace(typeof(IAssembliesResolver), new TestWebApiResolver());

            config.SuppressHostPrincipal();
            config.Filters.Add(new DummyAuthenticationAttrribute());

            AutofacConfig.Register(config);

            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            app.UseWebApi(config);
        }
    }
    
}
