using CDP.AdoNet.Interfaces;
using CDP.AdoNet.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CDP.AdoNet.Repositories
{
    public class RouteRepositoryConnected : IRouteRepositoryConnected
    {
        private readonly SqlConnection _connection;

        public RouteRepositoryConnected(SqlConnection connectionString)
        {
            _connection = connectionString;
        }

        private void SetParameters(SqlCommand command, RouteOfCargo obj)
        {
            command.Parameters.AddWithValue("@Id", obj.Id);
            command.Parameters.AddWithValue("@OriginWarehouseId", obj.OriginWarehouseId);
            command.Parameters.AddWithValue("@DestinationWarehouseId", obj.DestinationWarehouseId);
            command.Parameters.AddWithValue("@Distance", obj.Distance);
        }

        private void ExecuteQuery(SqlCommand command, bool isCommitted, IsolationLevel level)
        {
            _connection.Open();
            SqlTransaction transaction = _connection.BeginTransaction(level);
            command.Transaction = transaction;
            try
            {
                command.ExecuteNonQuery();
                if (isCommitted)
                {
                    transaction.Commit();
                }
            }
            catch (Exception e)
            {
                try
                {
                    transaction.Rollback();
                }
                catch (SqlException ex)
                {
                    if (transaction.Connection != null)
                    {
                        Console.WriteLine("An exception of type " + ex.GetType() +
                            " was encountered while attempting to roll back the transaction.");
                    }
                }

                Console.WriteLine("An exception of type " + e.GetType() +
                    " was encountered while operation.");
            }
            finally
            {
                _connection.Close();
            }
        }
        public void Create(RouteOfCargo obj, bool isCommitted, IsolationLevel level)
        {
            var query = "INSERT dbo.RouteOfCargo (OriginWarehouseId, DestinationWarehouseId, Distance) VALUES"+
                "( @OriginWarehouseId, @DestinationWarehouseId, @Distance)";
            using (SqlCommand command = new SqlCommand(query, _connection))
            {
                SetParameters(command, obj);
                ExecuteQuery(command, isCommitted, level);
            }
        }

        public IEnumerable<RouteOfCargo> GetAll()
        {
            var warehouseList = new List<RouteOfCargo>();
            var query = "SELECT Id, OriginWarehouseId, DestinationWarehouseId, Distance FROM dbo.RouteOfCargo";
            using (SqlCommand command = new SqlCommand(query, _connection))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                _connection.Open();
                dataAdapter.Fill(dataTable);
                _connection.Close();

                foreach (DataRow row in dataTable.Rows)
                {
                    warehouseList.Add(
                         new RouteOfCargo
                         {
                             Id = Convert.ToInt32(row["Id"]),
                             OriginWarehouseId = Convert.ToInt32(row["OriginWarehouseId"]),
                             DestinationWarehouseId = Convert.ToInt32(row["DestinationWarehouseId"]),
                             Distance = Convert.ToInt32(row["Distance"])
                         }
                    );
                }
                return warehouseList;
            }
        }

        public void Update(RouteOfCargo obj, bool isCommitted, IsolationLevel level)
        {
            var query = "UPDATE dbo.RouteOfCargo SET OriginWarehouseId = @OriginWarehouseId,"+
                " DestinationWarehouseId = @DestinationWarehouseId, Distance = @Distance WHERE Id = @Id";
            using (SqlCommand command = new SqlCommand(query, _connection))
            {
                SetParameters(command, obj);
                ExecuteQuery(command, isCommitted, level);
            }
        }

        public void Delete(int id, bool isCommitted, IsolationLevel level)
        {
            var query = "DELETE from dbo.RouteOfCargo WHERE Id = @Id";
            using (SqlCommand command = new SqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                ExecuteQuery(command, isCommitted, level);
            }
        }
    }
}
