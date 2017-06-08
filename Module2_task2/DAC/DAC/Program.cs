using System;
using System.Diagnostics;
using DAC.Dapper.Repositories;
using DAC.EF;
using DAC.EF.Repositories;

namespace DAC.ShipmentCompany
{
    class Program
    {
        static void Main(string[] args)
        {
            // Test for retrieving all data using EF repository.
            Stopwatch performanceForEf;

            using (var repository = new RouteRepositoryEf(new ShipmentContext()))
            {
                performanceForEf = Stopwatch.StartNew();

                repository.GetAll();

                performanceForEf.Stop();
            }

            Console.WriteLine($"Performance for EF approach is {performanceForEf.ElapsedMilliseconds} ms");

            // Test for retrieving all data using Dapper repository.

            var repositoryDapper = new RouteRepositoryDapper();

            var performanceForDapper = Stopwatch.StartNew();

            repositoryDapper.GetAll();

            performanceForDapper.Stop();

            Console.WriteLine($"Performance for Dapper approach is {performanceForDapper.ElapsedMilliseconds} ms");
        }
    }
}
