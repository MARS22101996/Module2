using DAC.Dapper.Models;
using System.Collections.Generic;

namespace DAC.Dapper.Interfaces
{
    public interface IRoutesRepository
    {
        void Create(RouteOfCargo item);
        void Update(RouteOfCargo item);
        void Delete(int id);
        IEnumerable<RouteOfCargo> GetAll();
        RouteOfCargo GetById(int id);
    }
}