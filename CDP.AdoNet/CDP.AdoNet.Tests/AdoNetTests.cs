﻿using System.Data.SqlClient;
using CDP.AdoNet.Models;
using CDP.AdoNet.Repositories;
using NUnit.Framework;
using System.Data;
using CDP.AdoNet.Infrastructure;

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

            using (var uow = TransactionWrapperFactory.Create())
            {
                var repositoryWarehouse = new WarehouseRepositoryConnected(uow);

                checkBeforeCreationOfFirst = repositoryWarehouse.GetById(308, null);

                checkBeforeCreationOfSecond = repositoryWarehouse.GetById(309, null);

                var repositoryRoute = new RouteRepositoryConnected(uow);

                checkBeforeCreationRoute = repositoryRoute.GetById(308, 309);
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

                using (var uow = TransactionWrapperFactory.Create())
                {
                    var repositoryWarehouse = new WarehouseRepositoryConnected(uow);

                    checkAfterCreationOfFirst = repositoryWarehouse.GetById(308, null);

                    checkAfterCreationOfSecond = repositoryWarehouse.GetById(309, null);

                    var repositoryRoute = new RouteRepositoryConnected(uow);

                    checkAfterCreationRoute = repositoryRoute.GetById(308, 309);
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

            using (var uow = TransactionWrapperFactory.Create())
            {
                var repositoryWarehouse = new WarehouseRepositoryConnected(uow);

                checkAfterRollbackfFirst = repositoryWarehouse.GetById(308, null);

                checkAfterRollbackSecond = repositoryWarehouse.GetById(309, null);

                var repositoryRoute = new RouteRepositoryConnected(uow);

                checkAfterCreationRoute = repositoryRoute.GetById(308, 309);
            }

            Assert.AreEqual(null, checkAfterRollbackfFirst);
            Assert.AreEqual(null, checkAfterRollbackSecond);
            Assert.AreEqual(null, checkAfterCreationRoute);
        }
    }
}
