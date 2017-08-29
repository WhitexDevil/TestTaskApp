using System;

namespace TestTaskApp.EntityFramework.Entities
{
    public class TestEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public EntityPriority Priority { get; set; }

        public bool Done { get; set; }

    }  
}
