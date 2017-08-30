using System.Data.Entity;

namespace TestTaskApp.EntityFramework
{
     public class TestTaskAppDbInitializer : MigrateDatabaseToLatestVersion<TestTaskAppContext, Configuration>
    {
       
    }
}
