using CDP.AdoNet.Models;
using CDP.AdoNet.Repositories;
using CDP.AdoNet.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDP.AdoNet
{
    class Program
    {
        static void Main(string[] args)
        {
            var uow = new UnitOfWork();
            //var w = new Warehouse() { Id = 297, City = "Sity", State = "State" };
            //uow.WarehouseRepositoryConnected.Create(w, true, System.Data.IsolationLevel.ReadCommitted);
            //var w1 = new Warehouse() { Id = 297, City = "Sity1", State = "State1" };
            //uow.WarehouseRepositoryConnected.Update(w1, true, System.Data.IsolationLevel.ReadCommitted);
            uow.WarehouseRepositoryConnected.Delete(290, true, System.Data.IsolationLevel.ReadCommitted);
            //var list = uow.WarehouseRepositoryConnected.GetAll().ToList();
            //foreach (var item in list)
            //{
            //    Console.WriteLine(item.Id + " " + item.City + " " + item.State);
            //}

            //var r1 = new RouteOfCargo() { Id = 1, OriginWarehouseId = 1, DestinationWarehouseId = 1, Distance = 100 };
            //uow.RouteRepositoryConnected.Update(r1, true, System.Data.IsolationLevel.ReadCommitted);
            //uow.RouteRepositoryConnected.Delete(1, true, System.Data.IsolationLevel.ReadCommitted);

            //var list1 = uow.RouteRepositoryConnected.GetAll().ToList();
            //foreach (var item in list1)
            //{
            //    Console.WriteLine(item.Id + " " + item.OriginWarehouseId + " " + item.DestinationWarehouseId);
            //}

            //Disconnected
            ////////
            //var w22 = new Warehouse() { Id = 299, City = "Sity", State = "State" };
            //uow.WarehouseRepositoryDisconnected.Create(w22, true, System.Data.IsolationLevel.ReadCommitted);
            //var w2 = new Warehouse() { Id = 293, City = "Sity3", State = "State3" };
            //uow.WarehouseRepositoryDisconnected.Update(w2, true, System.Data.IsolationLevel.ReadCommitted);
            //uow.WarehouseRepositoryDisconnected.Delete(298, true, System.Data.IsolationLevel.ReadCommitted);

            //var list2 = uow.WarehouseRepositoryDisconnected.GetAll().ToList();
            //foreach (var item in list2)
            //{
            //    Console.WriteLine(item.Id + " " + item.City + " " + item.State);
            //}
            //uow.WarehouseRepositoryConnected.Delete(295, true, System.Data.IsolationLevel.ReadCommitted);




            var adapter = new SqlDataAdapter();
            var dataSet = uow.RouteRepositoryDisconnected.GetAll(adapter);
            //var r1 = new RouteOfCargo() { Id = 100000, OriginWarehouseId = 1, DestinationWarehouseId = 1, Distance = 100 };
            //var r2 = new RouteOfCargo() { Id = 6, OriginWarehouseId = 1, DestinationWarehouseId = 1, Distance = 100 };
            //var created = uow.RouteRepositoryDisconnected.Create(dataSet, adapter, r1);
            //var updated = uow.RouteRepositoryDisconnected.Update(dataSet, adapter, r2);
            //var deleted = uow.RouteRepositoryDisconnected.Delete(dataSet, adapter, 5);
            //uow.RouteRepositoryDisconnected.Save(adapter, dataSet);

            var adapter1 = new SqlDataAdapter();
            var dataSet1 = uow.WarehouseRepositoryDisconnected.GetAll(adapter1);
            var w = new Warehouse() { Id = 300, City = "Sity299", State = "State" };
            var w1 = new Warehouse() { Id = 291, City = "Sity", State = "State" };
            var created1 = uow.WarehouseRepositoryDisconnected.Create(dataSet1, adapter1, w);
            var updated1 = uow.WarehouseRepositoryDisconnected.Update(dataSet1, adapter1, w1);
            var deleted = uow.RouteRepositoryDisconnected.DeleteByWarehouseId(dataSet, adapter, 293);
            uow.RouteRepositoryDisconnected.Save(adapter, dataSet);
            var deleted1 = uow.WarehouseRepositoryDisconnected.Delete(dataSet1, adapter1, 293);
            uow.WarehouseRepositoryDisconnected.Save(adapter1, dataSet1);

            //uow.RouteRepositoryDisconnected.Delete(1, true, System.Data.IsolationLevel.ReadCommitted);

            //var list1 = uow.RouteRepositoryDisconnected.GetAll().ToList();
            //foreach (var item in list1)
            //{
            //    Console.WriteLine(item.Id + " " + item.OriginWarehouseId + " " + item.DestinationWarehouseId);
            //}


            //uow.WarehouseRepositoryConnected.Create("INSERT[dbo].[Warehouse]([Id], [City], [State]) VALUES(296, N'College Station', N'Texas')");
            //uow.WarehouseRepositoryConnected.Update("UPDATE [dbo].[Warehouse] set [City] = 'College' where [Id] = 296");
            //uow.WarehouseRepositoryConnected.GetById(296);
            //uow.WarehouseRepositoryConnected.Delete("delete from [dbo].[Warehouse] where [Id] = 296");
            //uow.WarehouseRepositoryConnected.GetAll();

            //uow.RouteRepositoryConnected.Create("INSERT[dbo].RouteOfCargo (OriginWarehouseId, DestinationWarehouseId, Distance) VALUES(1, 2, 1000)");
            //uow.RouteRepositoryConnected.GetById(1);
            //uow.RouteRepositoryConnected.GetAll();
        }
    }
}
