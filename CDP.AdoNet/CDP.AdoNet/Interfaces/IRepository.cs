using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
