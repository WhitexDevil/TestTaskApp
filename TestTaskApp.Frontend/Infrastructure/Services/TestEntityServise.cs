using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TestTaskApp.EntityFramework.Entities;
using TestTaskApp.Frontend.Infrastructure.DataAccess;
using TestTaskApp.Frontend.Infrastructure.Exceptions;
using TestTaskApp.Frontend.Models;

namespace TestTaskApp.Frontend.Infrastructure.Services
{
    public class TestEntityServise : ITestEntityServise
    {
        private readonly IRepository<DbTestEntity> _testEntityRepository;
        public TestEntityServise(IRepository<DbTestEntity> testEntityRepository)
        {
            _testEntityRepository = testEntityRepository;
        }

        public IEnumerable<TestEntity> GetTestEntities()
        {
            return _testEntityRepository.GetEntities().Select(Mapper.Map<TestEntity>).ToList(); 
        }

        public TestEntity GetEntity(int id)
        {
            return Mapper.Map<TestEntity>(GetDbEntityById(id));
        }

        public TestEntity Create(TestEntity item)
        {
            var dbEntity = Mapper.Map<DbTestEntity>(item);
            _testEntityRepository.Create(dbEntity);
            _testEntityRepository.Save();
            return Mapper.Map<TestEntity>(dbEntity);
        }

        public void Update(TestEntity item)
        {
            var dbEntity = GetDbEntityById(item.Id);
            Mapper.Map(item, dbEntity);
            _testEntityRepository.Update(dbEntity);
            _testEntityRepository.Save();
        }

        public void Delete(int id)
        {
            GetDbEntityById(id);
            _testEntityRepository.Delete(id);
            _testEntityRepository.Save();
        }

        private DbTestEntity GetDbEntityById(int id)
        {
            var dbEntity = _testEntityRepository.GetEntity(id);
            if (dbEntity == null)
                throw new TestEntityNotFoundException($"Can not find TestEntity with Id={id}");
            return dbEntity;
        }
    }
}