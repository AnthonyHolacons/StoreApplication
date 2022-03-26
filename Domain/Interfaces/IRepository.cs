using System.Collections.Generic;

namespace Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        T Add(T entity);

        void Delete(T entity);

        T Get(int id);

        IEnumerable<T> GetAll();

    }
}
