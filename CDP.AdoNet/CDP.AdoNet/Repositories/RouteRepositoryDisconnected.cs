using System;
using System.Collections.Generic;
using System.Data;
using CDP.AdoNet.Interfaces;
using System.Data.SqlClient;
using CDP.AdoNet.Models;

namespace CDP.AdoNet.Repositories
{
    public class RouteRepositoryDisconnected : IRepositoryDisconnected<RouteOfCargo>
    {
        private readonly SqlConnection _connection;

        public RouteRepositoryDisconnected(SqlConnection connectionString)
        {
            _connection = connectionString;
        }

        public DataSet Create(DataSet dataSet, SqlDataAdapter adapter, RouteOfCargo obj)
        {           
            DataRow anyRow = dataSet.Tables["RouteOfCargo"].NewRow();
            anyRow["OriginWarehouseId"] = obj.OriginWarehouseId;
            anyRow["DestinationWarehouseId"] = obj.DestinationWarehouseId;
            anyRow["Distance"] = obj.Distance;
            dataSet.Tables["RouteOfCargo"].Rows.Add(anyRow);
            var commandBuilder = new SqlCommandBuilder(adapter);
            adapter.UpdateCommand = commandBuilder.GetUpdateCommand();
            return dataSet;           
        }


        public DataSet GetAll(SqlDataAdapter adapter)
        {
            const string query = "SELECT Id, OriginWarehouseId, DestinationWarehouseId, Distance FROM dbo.RouteOfCargo";

            using (var command = new SqlCommand(query, _connection))
            {
                _connection.Open();
                adapter.TableMappings.Add("Table", "RouteOfCargo");
                adapter.SelectCommand = command;
                var dataSet = new DataSet("RouteOfCargo");
                adapter.Fill(dataSet);
                _connection.Close();
                return dataSet;
            }
        }

        public DataSet Update(DataSet dataSet, SqlDataAdapter adapter, RouteOfCargo obj)
        {
            var row =
                dataSet.Tables["RouteOfCargo"].Select($"Id = '{obj.Id}'");
            row[0]["OriginWarehouseId"] = obj.OriginWarehouseId;
            row[0]["DestinationWarehouseId"] = obj.DestinationWarehouseId;
            row[0]["Distance"] = obj.Distance;
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
               dataSet.Tables["RouteOfCargo"].Select($"Id = '{id}'");
            row[0].Delete();
            var commandBuilder = new SqlCommandBuilder(adapter);
            adapter.UpdateCommand = commandBuilder.GetUpdateCommand();
            return dataSet;
        }
    }
}

