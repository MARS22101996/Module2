using System;
using System.Configuration;
using System.Data.SqlClient;
using CDP.AdoNet.Interfaces;
using CDP.AdoNet.Models;
using CDP.AdoNet.Repositories;

namespace CDP.AdoNet.Infrastructure
{
    public sealed class FacadeDisconnected : IFacadeDisconnected, IDisposable
    {
        private  SqlConnection _connection;

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

        public SqlConnection GetConnection()
        {
            return _connection;
        }

        public FacadeDisconnected()
        {
            string conString = ConfigurationManager.ConnectionStrings["CDPDatabase"].ToString();
            _connection = new SqlConnection(conString); 
        }
        public void Dispose()
        {
            if (_connection != null )
            {
                _connection.Close();
                _connection = null;
            }
        }
    }
}
