using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using TestTaskApp.EntityFramework;
using TestTaskApp.EntityFramework.Entities;
using TestTaskApp.Frontend.Infrastructure.DataAccess;
using TestTaskApp.Frontend.Infrastructure.Services;

namespace TestTaskApp.Frontend
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
            builder.RegisterType<TestTaskAppContext>()
                .AsSelf()
                .InstancePerRequest();

            builder.RegisterType<TestEntityRepository>()
                .As<IRepository<DbTestEntity>>()
                .InstancePerRequest();

            builder.RegisterType<TestEntityServise>()
                .As<ITestEntityServise>()
                .InstancePerRequest();
           
            ;
        }
    }
}