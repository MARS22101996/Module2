using System;
using CDP.AdoNet.Infrastructure;
using CDP.AdoNet.Models;
using CDP.AdoNet.Repositories;

namespace CDP.AdoNet
{
    class Program
    {
        static void Main(string[] args)
        {

            //Connected Warehouse example
            using (var uow = TransactionWrapperFactory.Create())
            {
                var repos = new WarehouseRepositoryConnected(uow);

                var checkBeforeCreate = repos.GetById(308, null);

                var checkBeforeUpdate = repos.GetById(200,null);

                var checkBeforeDelete = repos.GetById(289, null);

                Console.WriteLine(
                    checkBeforeCreate != null
                        ? $"Warehouse before creating: Id: {checkBeforeCreate.Id} City: {checkBeforeCreate.City} State: {checkBeforeCreate.State}"
                        : "Warehouse before creating is null");

                Console.WriteLine(
                    checkBeforeUpdate != null
                        ? $"Warehouse before updating: Id: {checkBeforeUpdate.Id} City: {checkBeforeUpdate.City} State: {checkBeforeUpdate.State}"
                        : "Warehouse before updating is null");

                Console.WriteLine(
                    checkBeforeUpdate != null
                        ? $"Warehouse before deleting: Id: {checkBeforeDelete.Id} City: {checkBeforeDelete.City} State: {checkBeforeDelete.State}"
                        : "Warehouse before deleting is null");

                var wCreate = new Warehouse() { Id = 308, City = "SityCreate", State = "StateCreate" };

                repos.Create(wCreate);

                var wUpdate = new Warehouse() { Id = 200, City = "SityUpdate", State = "StateUpdate" };

                repos.Update(wUpdate);

                repos.Delete(289);

                var checkAfterUpdate = repos.GetById(200, null);

                var checkAfterCreate = repos.GetById(308, null);

                var checkAfterDelete = repos.GetById(289, null);

                Console.WriteLine(
                    checkAfterCreate != null
                        ? $"Warehouse after creating: Id: {checkAfterCreate.Id} City: {checkAfterCreate.City} State: {checkAfterCreate.State}"
                        : "Warehouse after creating is null");

                Console.WriteLine(
                    checkAfterUpdate != null
                        ? $"Warehouse after updating: Id: {checkAfterUpdate.Id} City: {checkAfterUpdate.City} State: {checkAfterUpdate.State}"
                        : "Warehouse after updating is null");

                Console.WriteLine(
                    checkAfterDelete != null
                        ? $"Warehouse after deleting: Id: {checkAfterDelete.Id} City: {checkAfterDelete.City} State: {checkAfterDelete.State}"
                        : "Warehouse after deleting is null");

                uow.Rollback();

                var checkCreateAfterRollback = repos.GetById(308, null);

                var checkUpdateAfterRollback = repos.GetById(200, null);

                var checkDeleteAfterRollback = repos.GetById(289, null);

                Console.WriteLine(
                    checkCreateAfterRollback != null
                        ? $"Warehouse after creating: Id: {checkCreateAfterRollback.Id} City: {checkCreateAfterRollback.City} State: {checkCreateAfterRollback.State}"
                        : "Warehouse after creating is null");

                Console.WriteLine(
                    checkUpdateAfterRollback != null
                        ? $"Warehouse after updating: Id: {checkUpdateAfterRollback.Id} City: {checkUpdateAfterRollback.City} State: {checkUpdateAfterRollback.State}"
                        : "Warehouse after updating is null");

                Console.WriteLine(
                    checkDeleteAfterRollback != null
                        ? $"Warehouse after deleting: Id: {checkDeleteAfterRollback.Id} City: {checkDeleteAfterRollback.City} State: {checkDeleteAfterRollback.State}"
                        : "Warehouse after deleting is null");

            }


            //Connected Route
            //using (var uow = UnitOfWorkFactory.Create())
            //{
            //    var repos = new RouteRepositoryConnected(uow);

            //    var checkBeforeCreate = repos.GetById(86732);

            //    var checkBeforeUpdate = repos.GetById(2);

            //    var checkBeforeDelete = repos.GetById(3);

            //    var routeCreateConnected = new RouteOfCargo() { Id = 86732, OriginWarehouseId = 1, DestinationWarehouseId = 1, Distance = 100 };

            //    repos.Create(routeCreateConnected);

            //    var routeUpdateConnected = new RouteOfCargo() { Id = 2, OriginWarehouseId = 1, DestinationWarehouseId = 1, Distance = 1 };

            //    repos.Update(routeUpdateConnected);

            //    repos.Delete(3);

            //    var checkAfterCreate = repos.GetById(86732);

            //    var checkAfterUpdate = repos.GetById(2);

            //    var checkAfterDelete = repos.GetById(3);

            //    uow.Rollback();

            //    var checkCreateAfterRollback = repos.GetById(86732);

            //    var checkUpdateAfterRollback = repos.GetById(2);

            //    var checkDeleteAfterRollback = repos.GetById(3);

            //}


            //Disconnected Warehouse
            //var uoW = new UnitOfWork();
            //var adapter = new SqlDataAdapter();
            //var dataSet = uoW.RouteRepositoryDisconnected.GetAll(adapter);
            //var adapter1 = new SqlDataAdapter();
            //var dataSet1 = uoW.WarehouseRepositoryDisconnected.GetAll(adapter1);
            //using (var uow = UnitOfWorkFactory.Create())
            //{
            //    var repos = new WarehouseRepositoryConnected(uow);

            //    var checkBeforeCreateDisconnected = repos.GetById(308);

            //    var checkBeforeUpdateDisconnected = repos.GetById(304);

            //    var checkBeforeDeleteDisconnected = repos.GetById(289);
            //}

            //var wCreate = new Warehouse() { Id = 308, City = "Sity308", State = "State308" };

            //var created = uoW.WarehouseRepositoryDisconnected.Create(dataSet1, adapter1, wCreate);

            //var wUpdate = new Warehouse() { Id = 304, City = "SityUpdate", State = "StateUpdate" };

            //var updated = uoW.WarehouseRepositoryDisconnected.Update(dataSet1, adapter1, wUpdate);

            //var deleted = uoW.RouteRepositoryDisconnected.DeleteByWarehouseId(dataSet, adapter, 289);

            //uoW.RouteRepositoryDisconnected.ApplyChanges(adapter, dataSet, false);

            //var deleted1 = uoW.WarehouseRepositoryDisconnected.Delete(dataSet1, adapter1, 289);

            //uoW.WarehouseRepositoryDisconnected.ApplyChanges(adapter1, dataSet1, true);

            //using (var uow1 = UnitOfWorkFactory.Create())
            //{
            //    var repos = new WarehouseRepositoryConnected(uow1);

            //    var checkAfterCreateDisconnected = repos.GetById(308);

            //    var checkAfterUpdateDisconnected = repos.GetById(304);

            //    var checkAfterDeleteDisconnected = repos.GetById(289);
            //}

            //uoW.WarehouseRepositoryDisconnected.Rollback();

            //using (var uow2 = UnitOfWorkFactory.Create())
            //{
            //    var repos = new WarehouseRepositoryConnected(uow2);

            //    var checkCreateAfterRollbackDisconnected = repos.GetById(308);

            //    var checkUpdateAfterRollbackDisconnected = repos.GetById(304);

            //    var checkDeleteAfterRollbackDisconnected = repos.GetById(289);
            //}

            //Disconnected Route

            //int maxId;

            //int id;

            //using (var uow = UnitOfWorkFactory.Create())
            //{
            //    var repos = new RouteRepositoryConnected(uow);

            //    maxId = repos.GetAll().ToList().FindLast(x => x.Id != 1).Id;

            //    id = maxId + 1;

            //    var checkBeforeCreate = repos.GetById(maxId + 1);

            //    var checkBeforeUpdate = repos.GetById(2);

            //    var checkBeforeDelete = repos.GetById(3);
            //}

            //var routeCreate = new RouteOfCargo()
            //{
            //    Id = id,
            //    OriginWarehouseId = 1,
            //    DestinationWarehouseId = 1,
            //    Distance = 100
            //};

            //var createdRoute = uoW.RouteRepositoryDisconnected.Create(dataSet, adapter, routeCreate);

            //var routeUpdate = new RouteOfCargo()
            //{
            //    Id = 2,
            //    OriginWarehouseId = 111,
            //    DestinationWarehouseId = 111,
            //    Distance = 111
            //};

            //var updatedRoute = uoW.RouteRepositoryDisconnected.Update(dataSet, adapter, routeUpdate);

            //var deletedRoute = uoW.RouteRepositoryDisconnected.Delete(dataSet, adapter, 4);

            //uoW.RouteRepositoryDisconnected.ApplyChanges(adapter, dataSet, true);

            //using (var uow = UnitOfWorkFactory.Create())
            //{
            //    var repos = new RouteRepositoryConnected(uow);

            //    var checkAfterUpdate = repos.GetById(id, null);

            //    var checkAfterCreate = repos.GetById(2, null);

            //    var checkAfterDelete = repos.GetById(4, null);
            //}

            //uoW.RouteRepositoryDisconnected.Rollback();

            //using (var uow = UnitOfWorkFactory.Create())
            //{
            //    var repos = new RouteRepositoryConnected(uow);

            //    var checkCreateAfterRollback = repos.GetById(id, null);

            //    var checkUpdateAfterRollback = repos.GetById(2, null);

            //    var checkDeleteAfterRollback = repos.GetById(4, null);
            //}


           // var uoW = new FacadeDisconnected();

           // var adapterWarehouse = new SqlDataAdapter();

           // var dataSetWarehouse = uoW.WarehouseRepositoryDisconnected.GetAll(adapterWarehouse);

           // var adapterRoute = new SqlDataAdapter();

           // var dataSetRoute = uoW.RouteRepositoryDisconnected.GetAll(adapterRoute);

           // using (var uow = TransactionWrapperFactory.Create())
           // {
           //     var repos = new WarehouseRepositoryConnected(uow);

           //     var checkBeforeCreationOfFirst = repos.GetById(308, null);

           //     var checkBeforeCreationOfSecond = repos.GetById(309, null);
           // }

           // using (var uow = TransactionWrapperFactory.Create())
           // {
           //     var repos = new RouteRepositoryConnected(uow);

           //     var checkBeforeCreationRoute = repos.GetById(308, 309);
           // }

           // var wCreateFirst = new Warehouse() { Id = 308, City = "SityFirst", State = "StateFirst" };

           // var wCreateSecond = new Warehouse() { Id = 309, City = "SitySecond", State = "StateSecond" };

           // uoW.WarehouseRepositoryDisconnected.Create(dataSetWarehouse, adapterWarehouse, wCreateFirst);

           // uoW.WarehouseRepositoryDisconnected.Create(dataSetWarehouse, adapterWarehouse, wCreateSecond);

           // var routeCreate = new RouteOfCargo()
           // {
           //     Id = 1,
           //     OriginWarehouseId = 308,
           //     DestinationWarehouseId = 309,
           //     Distance = 100
           // };

           //uoW.RouteRepositoryDisconnected.Create(dataSetRoute, adapterRoute, routeCreate);

           // using (var connection = uoW.Connection())
           // {
           //     connection.Open();

           //     var transaction = connection.BeginTransaction(IsolationLevel.ReadUncommitted);

           //     uoW.WarehouseRepositoryDisconnected.ApplyChanges(adapterWarehouse, dataSetWarehouse, transaction);

           //     uoW.RouteRepositoryDisconnected.ApplyChanges(adapterRoute, dataSetRoute, transaction);

           //     using (var uow = TransactionWrapperFactory.Create())
           //     {
           //         var repos = new WarehouseRepositoryConnected(uow);

           //         var checkAfterCreationOfFirst = repos.GetById(308, null);

           //         var checkAfterCreationOfSecond = repos.GetById(309, null);
           //     }


           //     using (var uow = TransactionWrapperFactory.Create())
           //     {
           //         var repos = new RouteRepositoryConnected(uow);

           //         var checkAfterCreationRoute = repos.GetById(308, 309);
           //     }

           //     transaction.Rollback();

           //     connection.Close();
           // }


           // using (var uow = TransactionWrapperFactory.Create())
           // {
           //     var repos = new WarehouseRepositoryConnected(uow);

           //     var checkAfterRollbackfFirst = repos.GetById(308, null);

           //     var checkAfterRollbackOfSecond = repos.GetById(309, null);
           // }

           // using (var uow = TransactionWrapperFactory.Create())
           // {
           //     var repos = new RouteRepositoryConnected(uow);

           //     var checkAfterRollbackRoute = repos.GetById(308, 309);
           // }

        }
    }
}
