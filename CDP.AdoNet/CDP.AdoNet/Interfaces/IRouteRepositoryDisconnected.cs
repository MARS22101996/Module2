using System.Data;
using System.Data.SqlClient;
using CDP.AdoNet.Models;

namespace CDP.AdoNet.Interfaces
{
    public interface IRouteRepositoryDisconnected : IRepositoryDisconnected<RouteOfCargo>
    {
        void DeleteByWarehouseId(DataSet dataSet, SqlDataAdapter adapter, int id);
    }
}
