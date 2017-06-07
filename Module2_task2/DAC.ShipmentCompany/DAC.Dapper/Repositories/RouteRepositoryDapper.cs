using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using DapperExtensions;
using DAC.Dapper.Interfaces;
using DAC.Dapper.Models;

namespace DAC.Dapper.Repositories
{
    public class RouteRepositoryDapper: IRoutesRepository
    {
        private readonly string _connectionString;

        public RouteRepositoryDapper()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["CDPDatabase"].ToString();
        }

        public IEnumerable<RouteOfCargo> GetAll()
        {
            IEnumerable<RouteOfCargo> list;
            using (var cn = new SqlConnection(_connectionString))
            {
                cn.Open();
                list = cn.GetList<RouteOfCargo>();
                cn.Close();
            }
            return list;
        }

        public RouteOfCargo GetById(int id)
        {
            RouteOfCargo route;
            using (var cn = new SqlConnection(_connectionString))
            {
                cn.Open();
                route = cn.Get<RouteOfCargo>(id);
                cn.Close();
            }
            return route;
        }

        public void Create(RouteOfCargo item)
        {
            using (var cn = new SqlConnection(_connectionString))
            {
                cn.Open();
                cn.Insert(item);
                cn.Close();
            }
        }

        public void Delete(int id)
        {
            using (var cn = new SqlConnection(_connectionString))
            {
                cn.Open();
                var item = cn.Get<RouteOfCargo>(id);
                cn.Delete(item);
                cn.Close();
            }
        }

        public void Update(RouteOfCargo item)
        {
            using (var cn = new SqlConnection(_connectionString))
            {
                cn.Open();
                cn.Update(item);
                cn.Close();
            }
        }
    }
}