﻿using System.Collections.Generic;
using DAC.EF.Models;

namespace DAC.EF.Interfaces
{
    public interface IRoutesRepositoryEf
    {
        void Create(RouteOfCargo item);
        void Update(RouteOfCargo item);
        void Delete(int id);
        IEnumerable<RouteOfCargo> GetAll();
        RouteOfCargo GetById(int id);
        void Save();
    }
}