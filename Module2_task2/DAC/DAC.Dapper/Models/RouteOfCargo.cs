namespace DAC.Dapper.Models
{
    public class RouteOfCargo
    {
        public int Id { get; set; }
        public int OriginWarehouseId { get; set; }
        public int DestinationWarehouseId { get; set; }
        public double Distance { get; set; }
    }
}
