using System;
using System.Threading.Tasks;
using System.Web.Http;
using Autofac;
using Microsoft.Owin;
using Owin;
using TestTaskApp.Frontend.App_Start;

[assembly: OwinStartup(typeof(TestTaskApp.Frontend.Startup))]

namespace TestTaskApp.Frontend
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration httpConfiguration = new HttpConfiguration();
            
            AutofacConfig.Register(httpConfiguration);
            WebApiConfig.Register(httpConfiguration);

            app.UseAutofacMiddleware(AutofacConfig.Container);
            app.UseWebApi(httpConfiguration);
        }
    }
}
