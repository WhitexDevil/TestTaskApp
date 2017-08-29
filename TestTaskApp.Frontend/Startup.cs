using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin;
using Owin;
using TestTaskApp.Frontend.AppStart;

[assembly: OwinStartup(typeof(TestTaskApp.Frontend.Startup))]

namespace TestTaskApp.Frontend
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration httpConfiguration = new HttpConfiguration();
            WebApiConfig.Register(httpConfiguration);
            app.UseWebApi(httpConfiguration);
        }
    }
}
