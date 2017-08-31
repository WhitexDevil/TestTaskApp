using System;

namespace TestTaskApp.EntityFramework.Entities
{
    public class DbTestEntity : IDateTracked
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public byte Priority { get; set; }

        public bool Done { get; set; }

    }
}
