using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestTaskApp.EntityFramework;
using TestTaskApp.Frontend.Models;

namespace TestTaskApp.Frontend.Infrastructure
{
    public class TestEntityServise : ITestEntityServise
    {

        public IEnumerable<TestEntity> GetTestEntities()
        {
            throw new NotImplementedException();
        }

        public TestEntity GetEntity(int id)
        {
            throw new NotImplementedException();
        }

        public void Create(TestEntity item)
        {
            throw new NotImplementedException();
        }

        public void Update(TestEntity item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}