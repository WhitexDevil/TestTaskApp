using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using AutoMapper;
using TestTaskApp.EntityFramework;
using TestTaskApp.EntityFramework.Entities;
using TestTaskApp.Frontend.Infrastructure.Exceptions;
using TestTaskApp.Frontend.Models;

namespace TestTaskApp.Frontend.Infrastructure.Services
{
    public class TestEntityServise : ITestEntityServise,IDisposable
    {
        private readonly TestTaskAppContext _context;
        public TestEntityServise()
        {
            _context = new TestTaskAppContext();
        }

        public IEnumerable<TestEntity> GetTestEntities()
        {
            return _context.TestEntities.Select(Mapper.Map<TestEntity>).ToList(); 
        }

        public TestEntity GetEntity(int id)
        {
            return Mapper.Map<TestEntity>(GetDbEntityById(id));
        }

        public void Create(TestEntity item)
        {
            var dbEntity = Mapper.Map<DbTestEntity>(item);
            _context.TestEntities.Add(dbEntity);
            _context.SaveChanges();
        }

        public void Update(TestEntity item)
        {
            var dbEntity = GetDbEntityById(item.Id);

            dbEntity.Name = item.Name;
            dbEntity.Description = item.Description;
            dbEntity.Done = item.Done;
            dbEntity.Priority = (byte) item.Priority;

            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var dbEntity = GetDbEntityById(id);
            _context.TestEntities.Remove(dbEntity);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        private DbTestEntity GetDbEntityById(int id)
        {
            var dbEntity = _context.TestEntities.FirstOrDefault(e => e.Id == id);
            if (dbEntity == null)
                throw new TestEntityNotFoundException($"Can not find TestEntity with Id={id}");
            return dbEntity;
        }
    }
}