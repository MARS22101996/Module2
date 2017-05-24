using CDP.AdoNet.Interfaces;
using System.Data.SqlClient;
using System.Configuration;
using System;
using CDP.AdoNet.Models;

namespace CDP.AdoNet.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly SqlConnection _connectionString;

        private IRepository<Warehouse>  _warehouseRepositoryConnected;

        private IRepositoryDisconnected<Warehouse> _warehouseRepositoryDisconnected;

        private IRepository<RouteOfCargo> _routeRepositoryConnected;

        private IRepositoryDisconnected<RouteOfCargo> _routeRepositoryDisconnected;

        public IRepository<Warehouse> WarehouseRepositoryConnected => _warehouseRepositoryConnected ?? (_warehouseRepositoryConnected = new WarehouseRepositoryConnected(_connectionString));

        public IRepositoryDisconnected<Warehouse> WarehouseRepositoryDisconnected => _warehouseRepositoryDisconnected ?? (_warehouseRepositoryDisconnected = new WarehouseRepositoryDisconnected(_connectionString));

        public IRepository<RouteOfCargo> RouteRepositoryConnected => _routeRepositoryConnected ?? (_routeRepositoryConnected = new RouteRepositoryConnected(_connectionString));

        public IRepositoryDisconnected<RouteOfCargo> RouteRepositoryDisconnected => _routeRepositoryDisconnected ?? (_routeRepositoryDisconnected = new RouteRepositoryDisconnected(_connectionString));

        public UnitOfWork()
        {
            string conString = ConfigurationManager.ConnectionStrings["CDPDatabase"].ToString();
            _connectionString = new SqlConnection(conString);           
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
