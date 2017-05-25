using System;
using System.Configuration;
using System.Data.SqlClient;
using CDP.AdoNet.Interfaces;
using CDP.AdoNet.Models;
using CDP.AdoNet.Repositories;
using static System.GC;

namespace CDP.AdoNet.UnitOfWorks
{
    public sealed class FacadeDisconnected : IFacadeDisconnected, IDisposable
    {
        private readonly SqlConnection _connection;

        private IRepositoryDisconnected<Warehouse> _warehouseRepositoryDisconnected;

        private IRouteRepositoryDisconnected _routeRepositoryDisconnected;

        public IRepositoryDisconnected<Warehouse> WarehouseRepositoryDisconnected
            =>
                _warehouseRepositoryDisconnected ??
                (_warehouseRepositoryDisconnected = new WarehouseRepositoryDisconnected(_connection));

        public IRouteRepositoryDisconnected RouteRepositoryDisconnected
            =>
                _routeRepositoryDisconnected ??
                (_routeRepositoryDisconnected = new RouteRepositoryDisconnected(_connection));


        public SqlConnection Connection()
        {
            return _connection;
        }

        public FacadeDisconnected()
        {
            string conString = ConfigurationManager.ConnectionStrings["CDPDatabase"].ToString();
            _connection = new SqlConnection(conString); 
        }

        private bool _disposed;

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _connection.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            SuppressFinalize(this);
        }
    }
}
