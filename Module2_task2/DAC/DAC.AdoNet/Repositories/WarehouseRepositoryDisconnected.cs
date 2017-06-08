using CDP.AdoNet.Interfaces;
using CDP.AdoNet.Models;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace CDP.AdoNet.Repositories
{
    public class WarehouseRepositoryDisconnected : IRepositoryDisconnected<Warehouse>
    {
        private readonly SqlConnection _connection;

        private const string TableName = "Warehouse";

        public WarehouseRepositoryDisconnected(SqlConnection connectionString)
        {
            _connection = connectionString;
        }

        public void Create(DataSet dataSet, SqlDataAdapter adapter, Warehouse obj)
        {
            var anyRow = dataSet.Tables[TableName].NewRow();
            anyRow["Id"] = obj.Id;
            anyRow["City"] = obj.City;
            anyRow["State"] = obj.State;
            dataSet.Tables[TableName].Rows.Add(anyRow);
            var commandBuilder = new SqlCommandBuilder(adapter);
            adapter.UpdateCommand = commandBuilder.GetUpdateCommand();
        }

        public DataSet GetAll(SqlDataAdapter adapter)
        {
            const string query = "SELECT Id, City, State FROM dbo.Warehouse";

            using (var command = new SqlCommand(query, _connection))
            {
                try
                {
                    _connection.Open();
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

        public void Update(DataSet dataSet, SqlDataAdapter adapter, Warehouse obj)
        {
            var row =
                dataSet.Tables[TableName].Select($"Id = '{obj.Id}'");
            row.First()["City"] = obj.City;
            row.First()["State"] = obj.State;
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
    }
}