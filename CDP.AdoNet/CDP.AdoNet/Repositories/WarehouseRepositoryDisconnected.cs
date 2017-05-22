using CDP.AdoNet.Interfaces;
using CDP.AdoNet.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CDP.AdoNet.Repositories
{
    public class WarehouseRepositoryDisconnected : IWarehouseRepositoryConnected
    {
        private readonly SqlConnection _connection;

        public WarehouseRepositoryDisconnected(SqlConnection connectionString)
        {
            _connection = connectionString;
        }

        public void Create(Warehouse obj, bool isCommitted, IsolationLevel level)
        {
            var adapter = new SqlDataAdapter();
            const string query = "INSERT dbo.Warehouse (Id, City, State) VALUES(@Id, @City, @State)";
            using (var command = new SqlCommand(query, _connection))
            {
                try
                {
                    _connection.Open();
                    command.Parameters.AddWithValue("@Id", obj.Id);
                    command.Parameters.AddWithValue("@City", obj.City);
                    command.Parameters.AddWithValue("@State", obj.State);
                    adapter.InsertCommand = command;
                    adapter.InsertCommand.ExecuteNonQuery();
                    _connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An exception: " + ex.Message + " was encountered while operation.");
                }
            }
        }

        public IEnumerable<Warehouse> GetAll()
        {
            var warehouseList = new List<Warehouse>();
            const string query = "SELECT Id, City, State FROM dbo.Warehouse";

            using (var command = new SqlCommand(query, _connection))
            {
                _connection.Open();
                var adapter = new SqlDataAdapter();
                adapter.TableMappings.Add("Table", "Items");
                adapter.SelectCommand = command;
                var dataSet = new DataSet("Itms");
                adapter.Fill(dataSet);
                _connection.Close();
                foreach (DataRow row in dataSet.Tables["Items"].Rows)
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
            const string query = "SELECT Id, City, State FROM dbo.Warehouse";

            using (var command = new SqlCommand(query, _connection))
            {
                _connection.Open();
                var adapter = new SqlDataAdapter();
                adapter.TableMappings.Add("Table", "Items");
                adapter.SelectCommand = command;
                var dataSet = new DataSet("Itms");
                adapter.Fill(dataSet);
                _connection.Close();
                var row =
                    dataSet.Tables["Items"].Select($"Id = '{obj.Id}'");
                row[0]["City"] = obj.City;
                row[0]["State"] = obj.State;
                var commandBuilder = new SqlCommandBuilder(adapter);
                adapter.UpdateCommand = commandBuilder.GetUpdateCommand();
                _connection.Open();
                adapter.Update(dataSet);
                _connection.Close();
            }
        }

        public void Delete(int id, bool isCommitted, IsolationLevel level)
        {
            var adapter = new SqlDataAdapter();
            const string query =
                "DELETE FROM dbo.RouteOfCargo WHERE OriginWarehouseId = @Id OR DestinationWarehouseId = @Id;"+
                "DELETE FROM dbo.Warehouse WHERE Id = @Id; ";
            using (var command = new SqlCommand(query, _connection))
            {
                try
                {
                    _connection.Open();
                    command.Parameters.AddWithValue("@Id", id);
                    adapter.DeleteCommand = command;
                    adapter.DeleteCommand.ExecuteNonQuery();
                    _connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An exception: " + ex.Message + " was encountered while operation.");
                }
            }
        }
    }
}
