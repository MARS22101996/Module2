using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAC.EF.Interfaces;

namespace DAC.EF.Repositories
{
    public class RouteRepositoryEf : IRoutesRepositoryEf, IDisposable
    {
        private  ShipmentContext _context;

        public RouteRepositoryEf(ShipmentContext context)
        {
            _context = context;
        }

        public IEnumerable<RouteOfCargo> GetAll()
        {
            return _context.RouteOfCargoes.ToList();
        }

        public RouteOfCargo GetById(int originId, int? destinationId)
        {
            //return _context.RouteOfCargoes.FirstOrDefault(x=>x.OriginWarehouseId==originId && x.DestinationWarehouseId == destinationId);
            return _context.RouteOfCargoes.FirstOrDefault(x => x.Id ==originId);
        }

        public void Create(RouteOfCargo item)
        {
            _context.RouteOfCargoes.Add(item);
        }

        public void Delete(int id)
        {
            var item = _context.RouteOfCargoes.Find(id);
            if (item == null) return;
            _context.RouteOfCargoes.Remove(item);
        }

        public void Update(RouteOfCargo item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
                _context = null;
            }
        }
    }
}
