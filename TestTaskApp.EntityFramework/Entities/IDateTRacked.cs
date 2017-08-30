using System;

namespace TestTaskApp.EntityFramework.Entities
{
    public interface IDateTracked
    {
        DateTime CreatedDate { get; set; }
        DateTime UpdatedDate { get; set; }
    }
}
