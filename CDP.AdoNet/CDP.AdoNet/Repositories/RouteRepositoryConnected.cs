﻿using CDP.AdoNet.Interfaces;
using CDP.AdoNet.Models;
using CDP.AdoNet.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CDP.AdoNet.Repositories
{
    public class RouteRepositoryConnected : IRepository<RouteOfCargo>
    {
        private readonly AdoNetUnitOfWork _unitOfWork;

        public RouteRepositoryConnected(IUnitOfWorkAdo uow)
        {
            if (uow == null)
                throw new ArgumentNullException("uow");

            _unitOfWork = uow as AdoNetUnitOfWork;
            if (_unitOfWork == null)
                throw new NotSupportedException("Ohh my, change that UnitOfWorkFactory, will you?");
        }

        public void Create(RouteOfCargo obj)
        {
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = "INSERT dbo.RouteOfCargo (OriginWarehouseId, DestinationWarehouseId, Distance) VALUES" +
                $"( {obj.OriginWarehouseId} , {obj.DestinationWarehouseId} , {obj.Distance} )";
                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<RouteOfCargo> GetAll()
        {
            using (var command = _unitOfWork.CreateCommand())
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
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = $"UPDATE dbo.RouteOfCargo SET OriginWarehouseId = {obj.OriginWarehouseId}," +
                $" DestinationWarehouseId = {obj.DestinationWarehouseId}, Distance = {obj.Distance} WHERE Id = {obj.Id}";
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = $"DELETE from dbo.RouteOfCargo WHERE Id = {id}";
                cmd.ExecuteNonQuery();
            }
        }

        public RouteOfCargo GetById(int id)
        {
            using (var command = _unitOfWork.CreateCommand())
            {
                command.CommandText = "SELECT Id, OriginWarehouseId, DestinationWarehouseId, Distance FROM dbo.RouteOfCargo " +
                                      $"WHERE Id = {id}";
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
