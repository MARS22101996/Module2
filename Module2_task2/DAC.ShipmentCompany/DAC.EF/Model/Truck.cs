using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAC.EF.Model
{
    [Table("Truck")]
    public partial class Truck
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Truck()
        {
            DriverTrucks = new HashSet<DriverTruck>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Brand { get; set; }

        [Required]
        [StringLength(50)]
        public string RegistrationNumber { get; set; }

        public int YearOfTruck { get; set; }

        public double Payload { get; set; }

        public double FuelConsumption { get; set; }

        public double VolumeCargo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DriverTruck> DriverTrucks { get; set; }
    }
}