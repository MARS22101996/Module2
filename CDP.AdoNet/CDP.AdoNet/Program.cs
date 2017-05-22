using CDP.AdoNet.Models;
using CDP.AdoNet.Repositories;
using System;
using System.Collections.Generic;
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
            //uow.WarehouseRepositoryConnected.Delete(297, true, System.Data.IsolationLevel.ReadCommitted);
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
            //var w22 = new Warehouse() { Id = 298, City = "Sity", State = "State" };
            //uow.WarehouseRepositoryDisconnected.Create(w22, true, System.Data.IsolationLevel.ReadCommitted);
            var w2 = new Warehouse() { Id = 293, City = "Sity3", State = "State3" };
            uow.WarehouseRepositoryDisconnected.Update(w2, true, System.Data.IsolationLevel.ReadCommitted);
            //uow.WarehouseRepositoryDisconnected.Delete(297, true, System.Data.IsolationLevel.ReadCommitted);

            var list2 = uow.WarehouseRepositoryDisconnected.GetAll().ToList();
            foreach (var item in list2)
            {
                Console.WriteLine(item.Id + " " + item.City + " " + item.State);
            }
            //uow.WarehouseRepositoryConnected.Delete(295, true, System.Data.IsolationLevel.ReadCommitted);


            //var r1 = new RouteOfCargo() { Id = 1, OriginWarehouseId = 1, DestinationWarehouseId = 1, Distance = 100 };
            //uow.RouteRepositoryDisconnected.Update(r1, true, System.Data.IsolationLevel.ReadCommitted);
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
