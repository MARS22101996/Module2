using System.Data;
using CDP.AdoNet.Interfaces;
using System.Data.SqlClient;
using CDP.AdoNet.Models;

namespace CDP.AdoNet.Repositories
{
    public class RouteRepositoryDisconnected : IRouteRepositoryDisconnected
    {
        private readonly SqlConnection _connection;

        public RouteRepositoryDisconnected(SqlConnection connectionString)
        {
            _connection = connectionString;
        }

        public void Create(DataSet dataSet, SqlDataAdapter adapter, RouteOfCargo obj)
        {
            var anyRow = dataSet.Tables["RouteOfCargo"].NewRow();
            anyRow["Id"] = obj.Id;
            anyRow["OriginWarehouseId"] = obj.OriginWarehouseId;
            anyRow["DestinationWarehouseId"] = obj.DestinationWarehouseId;
            anyRow["Distance"] = obj.Distance;
            dataSet.Tables["RouteOfCargo"].Rows.Add(anyRow);
            var commandBuilder = new SqlCommandBuilder(adapter);
            adapter.UpdateCommand = commandBuilder.GetUpdateCommand();
        }

        public DataSet GetAll(SqlDataAdapter adapter)
        {
          
            using (var command = _connection.CreateCommand())
            {
                _connection.Open();
                command.CommandText=
                    "SELECT Id, OriginWarehouseId, DestinationWarehouseId, Distance FROM dbo.RouteOfCargo";
                adapter.TableMappings.Add("Table", "RouteOfCargo");
                adapter.SelectCommand = command;
                var dataSet = new DataSet("RouteOfCargo");
                adapter.Fill(dataSet);
                _connection.Close();
                return dataSet;
            }
        }

        public void Update(DataSet dataSet, SqlDataAdapter adapter, RouteOfCargo obj)
        {
            var row =
                dataSet.Tables["RouteOfCargo"].Select($"Id = '{obj.Id}'");
            row[0]["OriginWarehouseId"] = obj.OriginWarehouseId;
            row[0]["DestinationWarehouseId"] = obj.DestinationWarehouseId;
            row[0]["Distance"] = obj.Distance;
            var commandBuilder = new SqlCommandBuilder(adapter);
            adapter.UpdateCommand = commandBuilder.GetUpdateCommand();
        }

        public void ApplyChanges(SqlDataAdapter adapter, DataSet dataSet, SqlTransaction transaction)
        {
            adapter.UpdateCommand.Transaction = transaction;
            adapter.SelectCommand.Transaction = transaction;
            adapter.Update(dataSet);
        }
        public void Delete(DataSet dataSet, SqlDataAdapter adapter, int id)
        {
            var row =
               dataSet.Tables["RouteOfCargo"].Select($"Id = '{id}'");
            row[0].Delete();
            var commandBuilder = new SqlCommandBuilder(adapter);
            adapter.UpdateCommand = commandBuilder.GetUpdateCommand();
        }

        public void DeleteByWarehouseId(DataSet dataSet, SqlDataAdapter adapter, int id)
        {
            var rows =
               dataSet.Tables["RouteOfCargo"].Select($"OriginWarehouseId = '{id}' OR DestinationWarehouseId = '{id}'");
            foreach (var row in rows)
            {
                row.Delete();
            }
            var commandBuilder = new SqlCommandBuilder(adapter);
            adapter.UpdateCommand = commandBuilder.GetUpdateCommand();
        }
    }
}

