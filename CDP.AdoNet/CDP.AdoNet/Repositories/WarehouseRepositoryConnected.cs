using CDP.AdoNet.Interfaces;
using CDP.AdoNet.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace CDP.AdoNet.Repositories
{
    public class WarehouseRepositoryConnected : IRepository<Warehouse>
    {
        private readonly SqlConnection _connection;

        public WarehouseRepositoryConnected(SqlConnection connectionString)
        {
            _connection = connectionString;
        }
        private void SetParameters(SqlCommand command, Warehouse obj)
        {
            command.Parameters.AddWithValue("@Id", obj.Id);
            command.Parameters.AddWithValue("@City", obj.City);
            command.Parameters.AddWithValue("@State", obj.State);
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

                Console.WriteLine("An exception: " + e.Message + " was encountered while operation.");
            }
            finally
            {
                _connection.Close();
            }
        }

        public void Create(Warehouse obj, bool isCommitted, IsolationLevel level)
        {
            var query = "INSERT dbo.Warehouse (Id, City, State) VALUES(@Id, @City, @State)";
            using (SqlCommand command = new SqlCommand(query, _connection))
            {
                SetParameters(command, obj);
                ExecuteQuery(command, isCommitted, level);
            }
        }

        public IEnumerable<Warehouse> GetAll()
        {        
            var warehouseList = new List<Warehouse>();
            var query = "SELECT Id, City, State FROM dbo.Warehouse";
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
                         new Warehouse
                         {
                             Id = Convert.ToInt32(row["Id"]),
                             City = Convert.ToString(row["City"]),
                             State = Convert.ToString(row["State"])
                         }
                         );
                }
                return warehouseList;
            }
        }

        public void Update(Warehouse obj, bool isCommitted, IsolationLevel level)
        {
            var query = "UPDATE dbo.Warehouse SET City = @City, State = @State WHERE Id = @Id";
            using (var command = new SqlCommand(query, _connection))
            {
                SetParameters(command, obj);
                ExecuteQuery(command, isCommitted, level);
            }
        }

        public void Delete(int id, bool isCommitted, IsolationLevel level)
        {
            var query = "DELETE FROM dbo.RouteOfCargo WHERE OriginWarehouseId = @Id " +
                        "OR DestinationWarehouseId = @Id; DELETE from dbo.Warehouse WHERE Id = @Id";
            using (var command = new SqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                ExecuteQuery(command, isCommitted, level);
            }
        }
    }
}
