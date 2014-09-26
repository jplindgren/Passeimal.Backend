using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Passeimal.Backend.Dal.Repositories {
    public interface IRepository<T> {
        IQueryable<T> GetAll();
        T Get(Guid id);
        void Add(T entity);
        void Remove(T entity);
        void ExecuteCommand(string sql, params object[] parameters);
        void SaveChanges();
    }
}
