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
    
    public partial class Fines
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Fines()
        {
            this.FineStatusHistory = new HashSet<FineStatusHistory>();
        }
    
        public int Id { get; set; }
        public int ExternalId { get; set; }
        public string CarNum { get; set; }
        public string LicenseNum { get; set; }
        public System.DateTime CreateDate { get; set; }
        public int PhotoId { get; set; }
    
        public virtual Photos Photos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FineStatusHistory> FineStatusHistory { get; set; }
    }
}