using System.Collections.Generic;
using System.Data;

namespace CDP.AdoNet.Interfaces
{
    public interface IRepository<T>
    {
        void Create(T item, bool isCommitted, IsolationLevel level);
        void Update(T item, bool isCommitted, IsolationLevel level);
        void Delete(int id, bool isCommitted, IsolationLevel level);
        IEnumerable<T> GetAll();

        //void GetById(int id);  
   }
}
