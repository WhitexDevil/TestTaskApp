using System.Collections.Generic;
using System.Reflection;
using System.Web.Http.Dispatcher;
using TestTaskApp.Frontend.ApiControllers;

namespace TestTaskApp.Frontend.Test.Infrastructure
{
    public class TestWebApiResolver : DefaultAssembliesResolver
    {
        public override ICollection<Assembly> GetAssemblies()
        {
            return new List<Assembly> { typeof(TestEntityController).Assembly };
        }
    }
}
