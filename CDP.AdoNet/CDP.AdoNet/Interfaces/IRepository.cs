using System.Collections.Generic;

namespace CDP.AdoNet.Interfaces
{
    public interface IRepository<T>
    {
        void Create(T item );
        void Update(T item );
        void Delete(int id );
        IEnumerable<T> GetAll();
        T GetById(int id);
    }
}
