using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CedroRestauranteWS.Repositories
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();

        T GetById(int id);

        IEnumerable<T> Find(Func<T, bool> predicate);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        int Count(Func<T, bool> predicate);
    }
}
