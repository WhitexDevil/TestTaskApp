using System.Data.Entity.Migrations;


namespace TestTaskApp.EntityFramework
{
    public class Configuration : DbMigrationsConfiguration<TestTaskAppContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = false;
        }
    }
}
