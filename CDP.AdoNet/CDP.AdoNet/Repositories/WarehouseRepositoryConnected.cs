using CDP.AdoNet.Interfaces;
using CDP.AdoNet.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CDP.AdoNet.Infrastructure;


namespace CDP.AdoNet.Repositories
{
    public class WarehouseRepositoryConnected : IRepository<Warehouse>
    {

        private readonly TransactionWrapperConnected _transactionWrapper;

        public WarehouseRepositoryConnected(ITransactionWrapperConnected uow)
        {
            if (uow == null)
                throw new ArgumentNullException();

            _transactionWrapper = uow as TransactionWrapperConnected;
            if (_transactionWrapper == null)
                throw new NotSupportedException();
        }

        public void Create(Warehouse obj)
        {
            using (var cmd = _transactionWrapper.CreateCommand())
            {
                cmd.CommandText= $"INSERT dbo.Warehouse (Id, City, State) VALUES({obj.Id}, '{obj.City}', '{obj.State}')";
                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<Warehouse> GetAll()
        {
            using (var command = _transactionWrapper.CreateCommand())
            {
                command.CommandText = "SELECT Id, City, State FROM dbo.Warehouse";
                using (var reader = command.ExecuteReader())
                {
                    var warehouses = new List<Warehouse>();
                    while (reader.Read())
                    {
                        var warehouse = new Warehouse();
                        Map(reader, warehouse);
                        warehouses.Add(warehouse);
                    }
                    return warehouses;
                }
            }
        }

        public void Update(Warehouse obj)
        {
            using (var cmd = _transactionWrapper.CreateCommand())
            {
                cmd.CommandText = $"UPDATE dbo.Warehouse SET City = '{obj.City}', State = '{obj.State}' WHERE Id = {obj.Id}";
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var cmd = _transactionWrapper.CreateCommand())
            {
                cmd.CommandText = $"DELETE FROM dbo.RouteOfCargo WHERE OriginWarehouseId = {id} " +
                        $"OR DestinationWarehouseId = {id}; DELETE from dbo.Warehouse WHERE Id = {id}";
                cmd.ExecuteNonQuery();
            }
        }

        public Warehouse GetById(int originId, int? destinationId)
        {
            using (var command = _transactionWrapper.CreateCommand())
            {
                command.CommandText = $"SELECT Id, City, State FROM dbo.Warehouse  WHERE Id = {originId}";
                using (var reader = command.ExecuteReader())
                {
                    var warehouses = new List<Warehouse>();
                    while (reader.Read())
                    {
                        var warehouse = new Warehouse();
                        Map(reader, warehouse);
                        warehouses.Add(warehouse);
                    }
                    return warehouses.FirstOrDefault();
                }
            }
        }
        private void Map(IDataRecord record, Warehouse warehouse)
        {
            warehouse.Id = (int)record["Id"];
            warehouse.City = record["City"].ToString();
            warehouse.State = record["State"].ToString();
        }
    }
}