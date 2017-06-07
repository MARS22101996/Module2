namespace DAC.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ShipmentContext : DbContext
    {
        public ShipmentContext()
            : base("name=ShipmentContext")
        {
        }

        public virtual DbSet<Cargo> Cargoes { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Driver> Drivers { get; set; }
        public virtual DbSet<DriverTruck> DriverTrucks { get; set; }
        public virtual DbSet<RouteOfCargo> RouteOfCargoes { get; set; }
        public virtual DbSet<Shipment> Shipments { get; set; }
        public virtual DbSet<Truck> Trucks { get; set; }
        public virtual DbSet<Warehouse> Warehouses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cargo>()
                .Property(e => e.CargoName)
                .IsUnicode(false);

            modelBuilder.Entity<Cargo>()
                .Property(e => e.PriceOfCargo)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Cargo>()
                .HasMany(e => e.Shipments)
                .WithMany(e => e.Cargoes)
                .Map(m => m.ToTable("ShipmentCargo").MapLeftKey("CargoId").MapRightKey("ShipmentId"));

            modelBuilder.Entity<Contact>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Contact>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Contact>()
                .Property(e => e.CellPhone)
                .IsUnicode(false);

            modelBuilder.Entity<Contact>()
                .HasMany(e => e.Cargoes)
                .WithRequired(e => e.Contact)
                .HasForeignKey(e => e.RecipientId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Contact>()
                .HasMany(e => e.Cargoes1)
                .WithRequired(e => e.Contact1)
                .HasForeignKey(e => e.SenderId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Driver>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Driver>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<DriverTruck>()
                .HasMany(e => e.Shipments)
                .WithOptional(e => e.DriverTruck)
                .HasForeignKey(e => new { e.TruckId, e.DriverId })
                .WillCascadeOnDelete();

            modelBuilder.Entity<RouteOfCargo>()
                .HasMany(e => e.Cargoes)
                .WithRequired(e => e.RouteOfCargo)
                .HasForeignKey(e => e.RouteId);

            modelBuilder.Entity<RouteOfCargo>()
                .HasMany(e => e.Shipments)
                .WithRequired(e => e.RouteOfCargo)
                .HasForeignKey(e => e.RouteId);

            modelBuilder.Entity<Shipment>()
                .Property(e => e.PriceOfShipment)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Shipment>()
                .Property(e => e.PriceOfTotalCargo)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Truck>()
                .Property(e => e.Brand)
                .IsUnicode(false);

            modelBuilder.Entity<Truck>()
                .Property(e => e.RegistrationNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Warehouse>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<Warehouse>()
                .Property(e => e.State)
                .IsUnicode(false);

            modelBuilder.Entity<Warehouse>()
                .HasMany(e => e.RouteOfCargoes)
                .WithRequired(e => e.Warehouse)
                .HasForeignKey(e => e.DestinationWarehouseId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Warehouse>()
                .HasMany(e => e.RouteOfCargoes1)
                .WithRequired(e => e.Warehouse1)
                .HasForeignKey(e => e.OriginWarehouseId)
                .WillCascadeOnDelete(false);
        }
    }
}
