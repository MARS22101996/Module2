using CDP.AdoNet.Interfaces;
using CDP.AdoNet.Models;
using System.Data;
using System.Data.SqlClient;

namespace CDP.AdoNet.Repositories
{
    public class WarehouseRepositoryDisconnected : IRepositoryDisconnected<Warehouse>
    {
        private readonly SqlConnection _connection;

        public WarehouseRepositoryDisconnected(SqlConnection connectionString)
        {
            _connection = connectionString;
        }

        public void Create(DataSet dataSet, SqlDataAdapter adapter, Warehouse obj)
        {
            var anyRow = dataSet.Tables["Warehouse"].NewRow();
            anyRow["Id"] = obj.Id;
            anyRow["City"] = obj.City;
            anyRow["State"] = obj.State;
            dataSet.Tables["Warehouse"].Rows.Add(anyRow);
            var commandBuilder = new SqlCommandBuilder(adapter);
            adapter.UpdateCommand = commandBuilder.GetUpdateCommand();
        }

        public DataSet GetAll(SqlDataAdapter adapter)
        {
            const string query = "SELECT Id, City, State FROM dbo.Warehouse";

            using (var command = new SqlCommand(query, _connection))
            {
                _connection.Open();
                adapter.TableMappings.Add("Table", "Warehouse");
                adapter.SelectCommand = command;
                var dataSet = new DataSet("Warehouse");
                adapter.Fill(dataSet);
                _connection.Close();
                return dataSet;
            }
        }

        public void Update(DataSet dataSet, SqlDataAdapter adapter, Warehouse obj)
        {
            var row =
                dataSet.Tables["Warehouse"].Select($"Id = '{obj.Id}'");
            row[0]["City"] = obj.City;
            row[0]["State"] = obj.State;
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
                dataSet.Tables["Warehouse"].Select($"Id = '{id}'");
            row[0].Delete();
            var commandBuilder = new SqlCommandBuilder(adapter);
            adapter.UpdateCommand = commandBuilder.GetUpdateCommand();
        }
    }
}