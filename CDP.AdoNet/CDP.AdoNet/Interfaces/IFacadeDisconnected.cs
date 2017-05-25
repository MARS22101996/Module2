using System.Data.SqlClient;
using CDP.AdoNet.Models;

namespace CDP.AdoNet.Interfaces
{
    public interface IFacadeDisconnected
    {
        IRepositoryDisconnected<Warehouse> WarehouseRepositoryDisconnected { get; }

        IRouteRepositoryDisconnected RouteRepositoryDisconnected { get; }

        SqlConnection Connection();
    }
}
