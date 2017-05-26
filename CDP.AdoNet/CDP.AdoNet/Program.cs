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
        }
    }
}
