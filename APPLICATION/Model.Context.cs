//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace APPLICATION
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class GIBDDEntities : DbContext
    {
        public GIBDDEntities()
            : base("name=GIBDDEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AuthorizationAttempts> AuthorizationAttempts { get; set; }
        public virtual DbSet<Drivers> Drivers { get; set; }
        public virtual DbSet<Photos> Photos { get; set; }
        public virtual DbSet<RegionCodeCodes> RegionCodeCodes { get; set; }
        public virtual DbSet<RegionCodes> RegionCodes { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Fines> Fines { get; set; }
        public virtual DbSet<FineStatuses> FineStatuses { get; set; }
        public virtual DbSet<FineStatusHistory> FineStatusHistory { get; set; }
        public virtual DbSet<CarColors> CarColors { get; set; }
        public virtual DbSet<RegNumbers> RegNumbers { get; set; }
        public virtual DbSet<TypeOfDrive> TypeOfDrive { get; set; }
        public virtual DbSet<Vehicles> Vehicles { get; set; }
        public virtual DbSet<EngineTypes> EngineTypes { get; set; }
        public virtual DbSet<DriverLicenseCategories> DriverLicenseCategories { get; set; }
        public virtual DbSet<DriverLicenseCategory> DriverLicenseCategory { get; set; }
        public virtual DbSet<DriverLicenses> DriverLicenses { get; set; }
    }
}
