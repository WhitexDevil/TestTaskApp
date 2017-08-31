using System;

namespace TestTaskApp.Frontend.Models
{
    public class TestEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public EntityPriority Priority { get; set; }

        public bool Done { get; set; }
    }
}