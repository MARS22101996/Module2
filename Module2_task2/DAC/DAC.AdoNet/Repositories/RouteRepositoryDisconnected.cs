using System.Data;
using CDP.AdoNet.Interfaces;
using System.Data.SqlClient;
using System.Linq;
using CDP.AdoNet.Models;

namespace CDP.AdoNet.Repositories
{
    public class RouteRepositoryDisconnected : IRouteRepositoryDisconnected
    {
        private readonly SqlConnection _connection;

        private const string TableName = "RouteOfCargo";

        public RouteRepositoryDisconnected(SqlConnection connectionString)
        {
            _connection = connectionString;
        }

        public void Create(DataSet dataSet, SqlDataAdapter adapter, RouteOfCargo obj)
        {
            var anyRow = dataSet.Tables[TableName].NewRow();
            anyRow["Id"] = obj.Id;
            anyRow["OriginWarehouseId"] = obj.OriginWarehouseId;
            anyRow["DestinationWarehouseId"] = obj.DestinationWarehouseId;
            anyRow["Distance"] = obj.Distance;
            dataSet.Tables[TableName].Rows.Add(anyRow);
            var commandBuilder = new SqlCommandBuilder(adapter);
            adapter.UpdateCommand = commandBuilder.GetUpdateCommand();
        }

        public DataSet GetAll(SqlDataAdapter adapter)
        {
            using (var command = _connection.CreateCommand())
            {
                try
                {
                    _connection.Open();
                    command.CommandText =
                        "SELECT Id, OriginWarehouseId, DestinationWarehouseId, Distance FROM dbo.RouteOfCargo";
                    adapter.TableMappings.Add("Table", TableName);
                    adapter.SelectCommand = command;
                    var dataSet = new DataSet(TableName);
                    adapter.Fill(dataSet);
                    return dataSet;
                }
                finally
                {
                    _connection.Close();
                }
            }
        }

        public void Update(DataSet dataSet, SqlDataAdapter adapter, RouteOfCargo obj)
        {
            var row =
                dataSet.Tables[TableName].Select($"Id = '{obj.Id}'");
            row.First()["OriginWarehouseId"] = obj.OriginWarehouseId;
            row.First()["DestinationWarehouseId"] = obj.DestinationWarehouseId;
            row.First()["Distance"] = obj.Distance;
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
                dataSet.Tables[TableName].Select($"Id = '{id}'");
            row.First().Delete();
            var commandBuilder = new SqlCommandBuilder(adapter);
            adapter.UpdateCommand = commandBuilder.GetUpdateCommand();
        }

        public void DeleteByWarehouseId(DataSet dataSet, SqlDataAdapter adapter, int id)
        {
            var rows =
                dataSet.Tables[TableName].Select($"OriginWarehouseId = '{id}' OR DestinationWarehouseId = '{id}'");
            foreach (var row in rows)
            {
                row.Delete();
            }
            var commandBuilder = new SqlCommandBuilder(adapter);
            adapter.UpdateCommand = commandBuilder.GetUpdateCommand();
        }
    }
}