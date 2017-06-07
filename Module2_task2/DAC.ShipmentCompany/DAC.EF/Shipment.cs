namespace DAC.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Shipment")]
    public partial class Shipment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Shipment()
        {
            Cargoes = new HashSet<Cargo>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? CompletedDate { get; set; }

        public int RouteId { get; set; }

        public int? TruckId { get; set; }

        public int? DriverId { get; set; }

        public decimal? PriceOfShipment { get; set; }

        public decimal? PriceOfTotalCargo { get; set; }

        public virtual DriverTruck DriverTruck { get; set; }

        public virtual RouteOfCargo RouteOfCargo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cargo> Cargoes { get; set; }
    }
}
