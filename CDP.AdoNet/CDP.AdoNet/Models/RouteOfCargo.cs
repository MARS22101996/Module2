using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDP.AdoNet.Models
{
    public class RouteOfCargo
    {
        public int Id { get; set; }

        public int OriginWarehouseId { get; set; }

        public int DestinationWarehouseId { get; set; }

        public float Distance { get; set; }

}
}
