using System;
using System.Collections.Generic;

namespace TestTaskApp.Frontend.Infrastructure.DataAccess
{
    public interface IRepository<T> : IDisposable
        where T : class
    {
        IEnumerable<T> GetEntities(); 
        T GetEntity(int id); 
        void Create(T item); 
        void Update(T item); 
        void Delete(int id); 
        void Save();  
    }
}
