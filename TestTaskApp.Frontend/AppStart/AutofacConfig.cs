using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using TestTaskApp.Frontend.Infrastructure;

namespace TestTaskApp.Frontend.AppStart
{
    public static class AutofacConfig
    {
        public static IContainer Container;
        public static void Register(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            RegisterInfrastructure(builder);

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            Container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(Container);
        }

        private static void RegisterInfrastructure(ContainerBuilder builder)
        {
            builder.RegisterType<TestEntityRepository>()
                .As<ITestEntityRepository>()
                .InstancePerRequest();
        }
    }
}