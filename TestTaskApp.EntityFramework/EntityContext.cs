using System.Data.Entity;
using TestTaskApp.EntityFramework.Entities;

namespace TestTaskApp.EntityFramework
{
    public class EntityContext : DbContext
    {
        public EntityContext()
            : base("DbTestAppConnection")
        { }

        public DbSet<TestEntity> TestEntities { get; set; }
    }
}
