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

        public DataSet Create(DataSet dataSet, SqlDataAdapter adapter, Warehouse obj)
        {
            DataRow anyRow = dataSet.Tables["Warehouse"].NewRow();
            anyRow["Id"] = obj.Id;
            anyRow["City"] = obj.City;
            anyRow["State"] = obj.State;
            dataSet.Tables["Warehouse"].Rows.Add(anyRow);
            var commandBuilder = new SqlCommandBuilder(adapter);
            adapter.UpdateCommand = commandBuilder.GetUpdateCommand();
            return dataSet;
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

        public DataSet Update(DataSet dataSet, SqlDataAdapter adapter, Warehouse obj)
        {
            var row =
                dataSet.Tables["Warehouse"].Select($"Id = '{obj.Id}'");
            row[0]["City"] = obj.City;
            row[0]["State"] = obj.State;
            var commandBuilder = new SqlCommandBuilder(adapter);
            adapter.UpdateCommand = commandBuilder.GetUpdateCommand();
            return dataSet;
        }

        public void Save(SqlDataAdapter adapter, DataSet dataSet)
        {
            _connection.Open();
            adapter.Update(dataSet);
            _connection.Close();
        }

        public DataSet Delete(DataSet dataSet, SqlDataAdapter adapter, int id)
        {
            var row =
                dataSet.Tables["Warehouse"].Select($"Id = '{id}'");
            row[0].Delete();
            var commandBuilder = new SqlCommandBuilder(adapter);
            adapter.UpdateCommand = commandBuilder.GetUpdateCommand();
            return dataSet;
        }
    }
}
