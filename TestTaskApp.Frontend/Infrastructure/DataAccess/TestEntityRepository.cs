using System.Collections.Generic;
using System.Data.Entity;
using TestTaskApp.EntityFramework;
using TestTaskApp.EntityFramework.Entities;

namespace TestTaskApp.Frontend.Infrastructure.DataAccess
{
    public class TestEntityRepository:IRepository<DbTestEntity>
    {
        private readonly TestTaskAppContext _context;

        public TestEntityRepository(TestTaskAppContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IEnumerable<DbTestEntity> GetEntities()
        {
            return _context.TestEntities;
        }

        public DbTestEntity GetEntity(int id)
        {
            return _context.TestEntities.Find(id);
        }

        public void Create(DbTestEntity item)
        {
            _context.TestEntities.Add(item);
        }

        public void Update(DbTestEntity item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
           var entity = _context.TestEntities.Find(id);
            if (entity != null)
                _context.TestEntities.Remove(entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}