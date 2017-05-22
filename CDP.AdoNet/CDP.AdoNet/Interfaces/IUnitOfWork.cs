namespace CDP.AdoNet.Interfaces
{
    public interface IUnitOfWork
    {
        IWarehouseRepositoryConnected WarehouseRepositoryConnected { get;}

        IWarehouseRepositoryConnected WarehouseRepositoryDisconnected { get; }

        IRouteRepositoryConnected RouteRepositoryConnected { get; }

        IRouteRepositoryConnected RouteRepositoryDisconnected { get; }
    }
}
