using System.Data;
using System.Data.SqlClient;
using CDP.AdoNet.Models;

namespace CDP.AdoNet.Interfaces
{
    public interface IRouteRepositoryDisconnected : IRepositoryDisconnected<RouteOfCargo>
    {
        DataSet DeleteByWarehouseId(DataSet dataSet, SqlDataAdapter adapter, int id);
    }
}
