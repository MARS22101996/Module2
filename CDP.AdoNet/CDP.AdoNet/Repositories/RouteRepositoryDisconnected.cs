using System;
using System.Collections.Generic;
using System.Data;
using CDP.AdoNet.Interfaces;
using System.Data.SqlClient;
using CDP.AdoNet.Models;

namespace CDP.AdoNet.Repositories
{
    public class RouteRepositoryDisconnected : IRouteRepositoryConnected
    {
        private readonly SqlConnection _connection;

        public RouteRepositoryDisconnected(SqlConnection connectionString)
        {
            _connection = connectionString;
        }

        public void Create(RouteOfCargo obj, bool isCommitted, IsolationLevel level)
        {
            //var adapter = new SqlDataAdapter();
            //var query = "INSERT dbo.RouteOfCargo (OriginWarehouseId, DestinationWarehouseId, Distance) VALUES" +
            //    "( @OriginWarehouseId, @DestinationWarehouseId, @Distance)";
            //using (var command = new SqlCommand(query, _connection))
            //{
            //    try
            //    {
            //        _connection.Open();
            //        command.Parameters.AddWithValue("@Id", obj.Id);
            //        command.Parameters.AddWithValue("@OriginWarehouseId", obj.OriginWarehouseId);
            //        command.Parameters.AddWithValue("@DestinationWarehouseId", obj.DestinationWarehouseId);
            //        command.Parameters.AddWithValue("@Distance", obj.Distance);
            //        adapter.InsertCommand = command;
            //        adapter.InsertCommand.ExecuteNonQuery();
            //        _connection.Close();
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine("An exception: " + ex.Message + " was encountered while operation.");
            //    }
            //}
            const string query = "SELECT Id, OriginWarehouseId, DestinationWarehouseId, Distance FROM dbo.RouteOfCargo";

            using (var command = new SqlCommand(query, _connection))
            {
                _connection.Open();
                var adapter = new SqlDataAdapter();
                adapter.TableMappings.Add("Table", "Items");
                adapter.SelectCommand = command;
                var dataSet = new DataSet("Itms");
                adapter.Fill(dataSet);
                _connection.Close();
                DataRow anyRow = dataSet.Tables["Items"].NewRow();
                anyRow["OriginWarehouseId"] = obj.OriginWarehouseId;
                anyRow["DestinationWarehouseId"] = obj.DestinationWarehouseId;
                anyRow["Distance"] = obj.Distance;
                dataSet.Tables["Items"].Rows.Add(anyRow);
                var commandBuilder = new SqlCommandBuilder(adapter);
                adapter.UpdateCommand = commandBuilder.GetUpdateCommand();
                _connection.Open();
                adapter.Update(dataSet);
                _connection.Close();
            }
        }

        public IEnumerable<RouteOfCargo> GetAll()
        {
            var routeOfCargoList = new List<RouteOfCargo>();
            const string query = "SELECT Id, OriginWarehouseId, DestinationWarehouseId, Distance FROM dbo.RouteOfCargo";

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
                    routeOfCargoList.Add(
                        new RouteOfCargo
                        {
                            Id = Convert.ToInt32(row["Id"]),
                            OriginWarehouseId = Convert.ToInt32(row["OriginWarehouseId"]),
                            DestinationWarehouseId = Convert.ToInt32(row["DestinationWarehouseId"]),
                            Distance = Convert.ToInt32(row["Distance"])
                        }
                    );
                }
                return routeOfCargoList;
            }
        }

        public void Update(RouteOfCargo obj, bool isCommitted, IsolationLevel level)
        {
            const string query = "SELECT Id, OriginWarehouseId, DestinationWarehouseId, Distance FROM dbo.RouteOfCargo";

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
                row[0]["OriginWarehouseId"] = obj.OriginWarehouseId;
                row[0]["DestinationWarehouseId"] = obj.DestinationWarehouseId;
                row[0]["Distance"] = obj.Distance;
                var commandBuilder = new SqlCommandBuilder(adapter);
                adapter.UpdateCommand = commandBuilder.GetUpdateCommand();
                _connection.Open();
                adapter.Update(dataSet);
                _connection.Close();
            }
        }

        public void Delete(int id, bool isCommitted, IsolationLevel level)
        {
            const string query = "DELETE from dbo.RouteOfCargo WHERE Id = @Id";
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
                   dataSet.Tables["Items"].Select($"Id = '{id}'");
                row[0].Delete();
                var commandBuilder = new SqlCommandBuilder(adapter);
                adapter.UpdateCommand = commandBuilder.GetUpdateCommand();
                _connection.Open();
                adapter.Update(dataSet);
                _connection.Close();
            }
            //var adapter = new SqlDataAdapter();
            //const string query = "DELETE from dbo.RouteOfCargo WHERE Id = @Id";
            //using (var command = new SqlCommand(query, _connection))
            //{
            //    try
            //    {
            //        _connection.Open();
            //        command.Parameters.AddWithValue("@Id", id);
            //        adapter.DeleteCommand = command;
            //        adapter.DeleteCommand.ExecuteNonQuery();
            //        _connection.Close();
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine("An exception: " + ex.Message + " was encountered while operation.");
            //    }
            //}
        }
    }
}
