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
    using System.Collections.Generic;
    
    public partial class DriverLicenseCategories
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DriverLicenseCategories()
        {
            this.DriverLicenseCategory = new HashSet<DriverLicenseCategory>();
        }
    
        public int Id { get; set; }
        public string Code { get; set; }
        public System.DateTime DateCreated { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DriverLicenseCategory> DriverLicenseCategory { get; set; }
    }
}
