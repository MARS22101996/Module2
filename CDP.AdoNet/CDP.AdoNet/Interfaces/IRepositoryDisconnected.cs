using System.Data;
using System.Data.SqlClient;

namespace CDP.AdoNet.Interfaces
{
    public interface IRepositoryDisconnected<T>
    {
        void Create(DataSet data, SqlDataAdapter adapter, T obj);
        void Update(DataSet data, SqlDataAdapter adapter, T obj);
        void Delete(DataSet data, SqlDataAdapter adapter, int id);
        DataSet GetAll(SqlDataAdapter adapter);
        void  ApplyChanges(SqlDataAdapter adapter, DataSet dataSet, SqlTransaction transaction);
    }
}
