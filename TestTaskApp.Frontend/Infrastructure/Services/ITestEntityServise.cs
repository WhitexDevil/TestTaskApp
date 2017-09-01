using System.Collections.Generic;
using TestTaskApp.Frontend.Models;

namespace TestTaskApp.Frontend.Infrastructure.Services
{
    public interface ITestEntityServise 
    {
        IEnumerable<TestEntity> GetTestEntities();
        TestEntity GetEntity(int id);
        TestEntity Create(TestEntity item);
        void Update(TestEntity item);
        void Delete(int id);
    }
}