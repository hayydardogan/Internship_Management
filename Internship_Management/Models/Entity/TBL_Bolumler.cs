//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Internship_Management.Models.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class TBL_Bolumler
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TBL_Bolumler()
        {
            this.TBL_Akademisyenler = new HashSet<TBL_Akademisyenler>();
            this.TBL_Ogrenciler = new HashSet<TBL_Ogrenciler>();
        }
    
        public int bolumID { get; set; }
        public string bolumAdi { get; set; }
        public Nullable<byte> fakulteID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_Akademisyenler> TBL_Akademisyenler { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_Ogrenciler> TBL_Ogrenciler { get; set; }
    }
}
