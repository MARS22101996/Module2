using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAC.EF.Models
{
    [Table("Cargo")]
    public partial class Cargo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cargo()
        {
            Shipments = new HashSet<Shipment>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string CargoName { get; set; }

        public double CargoWeight { get; set; }

        public double Volume { get; set; }

        public int SenderId { get; set; }

        public int RecipientId { get; set; }

        public int RouteId { get; set; }

        public decimal PriceOfCargo { get; set; }

        public virtual Contact Contact { get; set; }

        public virtual RouteOfCargo RouteOfCargo { get; set; }

        public virtual Contact Contact1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Shipment> Shipments { get; set; }
    }
}