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
    
    public partial class TBL_StajTurleri
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TBL_StajTurleri()
        {
            this.TBL_Dosyalar = new HashSet<TBL_Dosyalar>();
        }
    
        public byte stajID { get; set; }
        public string stajAdi { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_Dosyalar> TBL_Dosyalar { get; set; }
    }
}
