using System.Web.Http;
using System.Web.Http.Dispatcher;
using Microsoft.Owin.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Owin;
using TestTaskApp.Frontend.App_Start;
using TestTaskApp.Frontend.Infrastructure.Authentication;
using TestTaskApp.Frontend.Test.Infrastructure;

namespace TestTaskApp.Frontend.Test.IntegrationTests
{
    [TestClass]
    public abstract class BaseApiTest
    {
        private HttpConfiguration _configuration;
        public TestServer Server { get; private set; }

        [TestInitialize]
        public virtual void Setup()
        {
            Server = TestServer.Create(app =>
            {
                _configuration = new HttpConfiguration();
                _configuration.Services.Replace(typeof(IAssembliesResolver), new TestWebApiResolver());

                _configuration.SuppressHostPrincipal();
                _configuration.Filters.Add(new DummyAuthenticationAttrribute());

                AutofacConfig.Register(_configuration);

                _configuration.MapHttpAttributeRoutes();
                _configuration.Routes.MapHttpRoute(
                    name: "DefaultApi",
                    routeTemplate: "api/{controller}/{id}",
                    defaults: new { id = RouteParameter.Optional }
                );

                app.UseAutofacMiddleware(AutofacConfig.Container);
                app.UseWebApi(_configuration);
            });
        }

        [TestCleanup]
        public virtual void Cleanup()
        {
            _configuration.Dispose();
            Server.Dispose();
        }
    }
}
