using CDP.AdoNet.Interfaces;
using CDP.AdoNet.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CDP.AdoNet.Extensions;
using CDP.AdoNet.Infrastructure;

namespace CDP.AdoNet.Repositories
{
    public class RouteRepositoryConnected : IRepository<RouteOfCargo>
    {
        private readonly TransactionWrapperConnected _transactionWrapper;

        public RouteRepositoryConnected(ITransactionWrapperConnected uow)
        {
            if (uow == null)
            {
                throw new ArgumentNullException();
            }

            _transactionWrapper = uow as TransactionWrapperConnected;

            if (_transactionWrapper == null)
            {
                throw new NotSupportedException();
            }
        }

        public void Create(RouteOfCargo obj)
        {
            using (var command = _transactionWrapper.CreateCommand())
            {
                command.CommandText = "SET IDENTITY_INSERT dbo.RouteOfCargo ON;" +
                    "INSERT dbo.RouteOfCargo (Id, OriginWarehouseId, DestinationWarehouseId, Distance) " +
                    "VALUES (@Id, @OriginWarehouseId, @DestinationWarehouseId, @Distance)";
                SetParameters(command, obj);
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<RouteOfCargo> GetAll()
        {
            using (var command = _transactionWrapper.CreateCommand())
            {
                command.CommandText =
                    "SELECT Id, OriginWarehouseId, DestinationWarehouseId, Distance FROM dbo.RouteOfCargo";
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
            using (var command = _transactionWrapper.CreateCommand())
            {
                command.CommandText = "UPDATE dbo.RouteOfCargo SET OriginWarehouseId = @OriginWarehouseId," +
                "DestinationWarehouseId = @DestinationWarehouseId, Distance = @Distance WHERE Id = @Id";
                SetParameters(command, obj);
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var command = _transactionWrapper.CreateCommand())
            {
                command.CommandText = "DELETE from dbo.RouteOfCargo WHERE Id = @Id";
                command.AddParameter("@Id", id);
                command.ExecuteNonQuery();
            }
        }

        public RouteOfCargo GetById(int originId, int? destinationId)
        {
            using (var command = _transactionWrapper.CreateCommand())
            {
                command.CommandText =
                    "SELECT Id, OriginWarehouseId, DestinationWarehouseId, Distance FROM dbo.RouteOfCargo " +
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
            route.Id = (int) record["Id"];
            route.OriginWarehouseId = (int) (record["OriginWarehouseId"]);
            route.DestinationWarehouseId = (int) (record["DestinationWarehouseId"]);
            route.Distance = Convert.ToInt32(record["Distance"]);
        }

        private void SetParameters(IDbCommand command, RouteOfCargo obj)
        {
            command.AddParameter("@Id", obj.Id);
            command.AddParameter("@OriginWarehouseId", obj.OriginWarehouseId);
            command.AddParameter("@DestinationWarehouseId", obj.DestinationWarehouseId);
            command.AddParameter("@Distance", obj.Distance);
        }
    }
}