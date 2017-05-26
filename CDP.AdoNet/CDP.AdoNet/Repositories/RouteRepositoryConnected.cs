using CDP.AdoNet.Interfaces;
using CDP.AdoNet.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CDP.AdoNet.Infrastructure;

namespace CDP.AdoNet.Repositories
{
    public class RouteRepositoryConnected : IRepository<RouteOfCargo>
    {
        private readonly TransactionWrapperConnected _transactionWrapper;

        public RouteRepositoryConnected(ITransactionWrapperConnected uow)
        {
            if (uow == null)
                throw new ArgumentNullException("uow");

            _transactionWrapper = uow as TransactionWrapperConnected;
            if (_transactionWrapper == null)
                throw new NotSupportedException("Ohh my, change that UnitOfWorkFactory, will you?");
        }

        public void Create(RouteOfCargo obj)
        {
            using (var cmd = _transactionWrapper.CreateCommand())
            {
                cmd.CommandText = "set IDENTITY_INSERT dbo.RouteOfCargo on;" +
                                  "INSERT dbo.RouteOfCargo (Id, OriginWarehouseId, DestinationWarehouseId, Distance) VALUES" +
                                   $"( {obj.Id} , {obj.OriginWarehouseId} , {obj.DestinationWarehouseId} , {obj.Distance} )";
                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<RouteOfCargo> GetAll()
        {
            using (var command = _transactionWrapper.CreateCommand())
            {
                command.CommandText = "SELECT Id, OriginWarehouseId, DestinationWarehouseId, Distance FROM dbo.RouteOfCargo";
                using (var reader = command.ExecuteReader())
                {
                    var routes = new List<RouteOfCargo>();
                    while (reader.Read())
                    {
                        var route = new RouteOfCargo();
                        Map(reader, route);
                        routes.Add(route);
                    }
                    return routes;
                }
            }
        }

        public void Update(RouteOfCargo obj)
        {
            using (var cmd = _transactionWrapper.CreateCommand())
            {
                cmd.CommandText = $"UPDATE dbo.RouteOfCargo SET OriginWarehouseId = {obj.OriginWarehouseId}," +
                $" DestinationWarehouseId = {obj.DestinationWarehouseId}, Distance = {obj.Distance} WHERE Id = {obj.Id}";
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var cmd = _transactionWrapper.CreateCommand())
            {
                cmd.CommandText = $"DELETE from dbo.RouteOfCargo WHERE Id = {id}";
                cmd.ExecuteNonQuery();
            }
        }

        public RouteOfCargo GetById(int originId, int? destinationId)
        {
            using (var command = _transactionWrapper.CreateCommand())
            {
                command.CommandText = "SELECT Id, OriginWarehouseId, DestinationWarehouseId, Distance FROM dbo.RouteOfCargo " +
                                      $"WHERE OriginWarehouseId = {originId} AND DestinationWarehouseId = {destinationId}";
                using (var reader = command.ExecuteReader())
                {
                    var routes = new List<RouteOfCargo>();
                    while (reader.Read())
                    {
                        var route = new RouteOfCargo();
                        Map(reader, route);
                        routes.Add(route);
                    }
                    return routes.FirstOrDefault();
                }
            }
        }

        private void Map(IDataRecord record, RouteOfCargo route)
        {
            route.Id = (int)record["Id"];
            route.OriginWarehouseId = (int) (record["OriginWarehouseId"]);
            route.DestinationWarehouseId = (int)(record["DestinationWarehouseId"]);
            route.Distance = Convert.ToInt32(record["Distance"]);
        }
    }
}