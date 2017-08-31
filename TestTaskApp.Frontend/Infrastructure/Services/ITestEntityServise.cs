using System.Collections.Generic;
using TestTaskApp.Frontend.Models;

namespace TestTaskApp.Frontend.Infrastructure.Services
{
    public interface ITestEntityServise 
    {
        IEnumerable<TestEntity> GetTestEntities();
        TestEntity GetEntity(int id);
        void Create(TestEntity item);
        void Update(TestEntity item);
        void Delete(int id);
    }
}