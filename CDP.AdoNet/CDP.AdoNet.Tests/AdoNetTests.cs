using System.Data.SqlClient;
using CDP.AdoNet.Models;
using CDP.AdoNet.Repositories;
using CDP.AdoNet.UnitOfWorks;
using NUnit.Framework;
using System.Data;

namespace CDP.AdoNet.Tests
{
    [TestFixture]
    public class AdoNetTests
    {
        [Test]
        public void GetData_ReturnsNoData_WhenDataIsNotAdded()
        {
            Warehouse checkBeforeCreationOfFirst;
            Warehouse checkBeforeCreationOfSecond;
            RouteOfCargo checkBeforeCreationRoute;

            using (var uow = UnitOfWorkFactory.Create())
            {
                var repos = new WarehouseRepositoryConnected(uow);

                 checkBeforeCreationOfFirst = repos.GetById(308, null);

                 checkBeforeCreationOfSecond = repos.GetById(309, null);
            }

            using (var uow = UnitOfWorkFactory.Create())
            {
                var repos = new RouteRepositoryConnected(uow);

                checkBeforeCreationRoute = repos.GetById(308, 309);
            }

            Assert.AreEqual(null, checkBeforeCreationOfFirst);
            Assert.AreEqual(null, checkBeforeCreationOfSecond);
            Assert.AreEqual(null, checkBeforeCreationRoute);
        }

        [Test]
        public void GetData_ReturnsData_WhenTransactionIsNotCommited()
        {
            Warehouse checkAfterCreationOfFirst;
            Warehouse checkAfterCreationOfSecond;
            RouteOfCargo checkAfterCreationRoute;

            var uoW = new FacadeDisconnected();

            var adapterWarehouse = new SqlDataAdapter();

            var dataSetWarehouse = uoW.WarehouseRepositoryDisconnected.GetAll(adapterWarehouse);

            var adapterRoute = new SqlDataAdapter();

            var dataSetRoute = uoW.RouteRepositoryDisconnected.GetAll(adapterRoute);

            var wCreateFirst = new Warehouse() { Id = 308, City = "SityFirst", State = "StateFirst" };

            var wCreateSecond = new Warehouse() { Id = 309, City = "SitySecond", State = "StateSecond" };

            uoW.WarehouseRepositoryDisconnected.Create(dataSetWarehouse, adapterWarehouse, wCreateFirst);

            uoW.WarehouseRepositoryDisconnected.Create(dataSetWarehouse, adapterWarehouse, wCreateSecond);

            var routeCreate = new RouteOfCargo()
            {
                Id = 1,
                OriginWarehouseId = 308,
                DestinationWarehouseId = 309,
                Distance = 100
            };

            uoW.RouteRepositoryDisconnected.Create(dataSetRoute, adapterRoute, routeCreate);

            using (var connection = uoW.Connection())
            {
                connection.Open();

                var transaction = connection.BeginTransaction(IsolationLevel.ReadUncommitted);

                uoW.WarehouseRepositoryDisconnected.ApplyChanges(adapterWarehouse, dataSetWarehouse, transaction);

                uoW.RouteRepositoryDisconnected.ApplyChanges(adapterRoute, dataSetRoute, transaction);

                using (var uow = UnitOfWorkFactory.Create())
                {
                    var repos = new WarehouseRepositoryConnected(uow);

                    checkAfterCreationOfFirst = repos.GetById(308, null);

                    checkAfterCreationOfSecond = repos.GetById(309, null);
                }

                using (var uow = UnitOfWorkFactory.Create())
                {
                    var repos = new RouteRepositoryConnected(uow);

                    checkAfterCreationRoute = repos.GetById(308, 309);
                }

                transaction.Rollback();

                connection.Close();
            }

            Assert.AreNotEqual(null, checkAfterCreationOfFirst);
            Assert.AreNotEqual(null, checkAfterCreationOfSecond);
            Assert.AreNotEqual(null, checkAfterCreationRoute);
        }

        [Test]
        public void GetData_ReturnsNoData_WhenTransactionIsRollbacked()
        {
            Warehouse checkAfterRollbackfFirst;
            Warehouse checkAfterRollbackSecond;
            RouteOfCargo checkAfterCreationRoute;

            using (var uow = UnitOfWorkFactory.Create())
            {
                var repos = new WarehouseRepositoryConnected(uow);

                checkAfterRollbackfFirst = repos.GetById(308, null);

                checkAfterRollbackSecond = repos.GetById(309, null);
            }

            using (var uow = UnitOfWorkFactory.Create())
            {
                var repos = new RouteRepositoryConnected(uow);

                checkAfterCreationRoute = repos.GetById(308, 309);
            }

            Assert.AreEqual(null, checkAfterRollbackfFirst);
            Assert.AreEqual(null, checkAfterRollbackSecond);
            Assert.AreEqual(null, checkAfterCreationRoute);
        }
    }
}
