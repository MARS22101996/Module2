using System;
using System.Diagnostics;
using DAC.Dapper.Repositories;
using DAC.EF;
using DAC.EF.Repositories;

namespace DAC.Shipment
{
    class Program
    {
        static void Main(string[] args)
        {
            //test comment

            //    using (var repository = new RouteRepositoryEf(new ShipmentContext()))
            //    {

            //        var wCreate = new RouteOfCargo() { OriginWarehouseId = 10, DestinationWarehouseId = 20, Distance = 10 };

            //        repository.Create(wCreate);

            //        repository.Save();

            //        var wUpdate = new RouteOfCargo() { Id = 5, OriginWarehouseId = 11, DestinationWarehouseId = 12, Distance = 100 };

            //        repository.Update(wUpdate);

            //        repository.Save();

            //        repository.Delete(26);

            //        repository.Save();
            //    }

            //    var repositoryDapper = new RouteRepositoryDapper();

            //    var wCreate1 = new Dapper.Models.RouteOfCargo() { OriginWarehouseId = 100, DestinationWarehouseId = 21, Distance = 10 };

            //    repositoryDapper.Create(wCreate1);

            //    var wUpdate1 = new Dapper.Models.RouteOfCargo(){ Id = 6, OriginWarehouseId = 11, DestinationWarehouseId = 12, Distance = 100 };

            //    repositoryDapper.Update(wUpdate1);

            //    //repositoryDapper.Delete(20);

            Stopwatch performanceForEf;

            using (var repository = new RouteRepositoryEf(new ShipmentContext()))
            {
                performanceForEf = Stopwatch.StartNew();

                repository.GetAll();

                performanceForEf.Stop();
            }

            Console.WriteLine($"Performance for EF approach is {performanceForEf.ElapsedMilliseconds} ms");

            var repositoryDapper = new RouteRepositoryDapper();

            var performanceForDapper = Stopwatch.StartNew();

            repositoryDapper.GetAll();

            performanceForDapper.Stop();

            Console.WriteLine($"Performance for Dapper approach is {performanceForDapper.ElapsedMilliseconds} ms");
        }

    }
}

