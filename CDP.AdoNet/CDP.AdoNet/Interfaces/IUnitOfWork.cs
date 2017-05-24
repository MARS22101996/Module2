using CDP.AdoNet.Interfaces;
using CDP.AdoNet.Models;

namespace CDP.AdoNet.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Warehouse> WarehouseRepositoryConnected { get;}

        IRepositoryDisconnected<Warehouse> WarehouseRepositoryDisconnected { get; }

        IRepository<RouteOfCargo> RouteRepositoryConnected { get; }

        IRepositoryDisconnected<RouteOfCargo> RouteRepositoryDisconnected { get; }
    }
}
