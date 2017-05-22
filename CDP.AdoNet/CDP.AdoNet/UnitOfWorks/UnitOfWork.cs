using CDP.AdoNet.Interfaces;
using System.Data.SqlClient;
using System.Configuration;
using System;

namespace CDP.AdoNet.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly SqlConnection _connectionString;

        private IWarehouseRepositoryConnected _warehouseRepositoryConnected;

        private IWarehouseRepositoryConnected _warehouseRepositoryDisconnected;

        private IRouteRepositoryConnected _routeRepositoryConnected;

        private IRouteRepositoryConnected _routeRepositoryDisconnected;

        public IWarehouseRepositoryConnected WarehouseRepositoryConnected => _warehouseRepositoryConnected ?? (_warehouseRepositoryConnected = new WarehouseRepositoryConnected(_connectionString));

        public IWarehouseRepositoryConnected WarehouseRepositoryDisconnected => _warehouseRepositoryDisconnected ?? (_warehouseRepositoryDisconnected = new WarehouseRepositoryDisconnected(_connectionString));

        public IRouteRepositoryConnected RouteRepositoryConnected => _routeRepositoryConnected ?? (_routeRepositoryConnected = new RouteRepositoryConnected(_connectionString));

        public IRouteRepositoryConnected RouteRepositoryDisconnected => _routeRepositoryConnected ?? (_routeRepositoryDisconnected = new RouteRepositoryDisconnected(_connectionString));

        public UnitOfWork()
        {
            string conString = ConfigurationManager.ConnectionStrings["CDPDatabase"].ToString();
            _connectionString = new SqlConnection(conString);
            //_connectionString = "Server=.\\SQLExpress;Database=mariia_suvalova_cdp2017q1;Integrated Security=SSPI;";
        }

        private bool _disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _connectionString.Dispose();
                }
                this._disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
