using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using DapperExtensions;
using DAC.Dapper.Interfaces;
using DAC.Dapper.Models;

namespace DAC.Dapper.Repositories
{
    public class RouteRepositoryDapper: IRouteRepositoryDapper
    {
        private readonly SqlConnection _connection;

        public RouteRepositoryDapper()
        {
            _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CDPDatabase"].ToString());
        }

        public IEnumerable<RouteOfCargo> GetAll()
        {
            IEnumerable<RouteOfCargo> routes;
            using (var connection = _connection)
            {
                connection.Open();
                routes = connection.GetList<RouteOfCargo>();
                connection.Close();
            }
            return routes;
        }

        public RouteOfCargo GetById(int id)
        {
            RouteOfCargo route;
            using (var connection = _connection)
            {
                connection.Open();
                route = connection.Get<RouteOfCargo>(id);
                connection.Close();
            }
            return route;
        }

        public void Create(RouteOfCargo item)
        {
            using (var connection = _connection)
            {
                connection.Open();
                connection.Insert(item);
                connection.Close();
            }
        }

        public void Delete(int id)
        {
            using (var connection = _connection)
            {
                connection.Open();
                var item = connection.Get<RouteOfCargo>(id);
                connection.Delete(item);
                connection.Close();
            }
        }

        public void Update(RouteOfCargo item)
        {
            using (var connection = _connection)
            {
                connection.Open();
                connection.Update(item);
                connection.Close();
            }
        }
    }
}