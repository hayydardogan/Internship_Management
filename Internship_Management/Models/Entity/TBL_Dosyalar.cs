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
    
    public partial class TBL_Dosyalar
    {
        public int dosyaID { get; set; }
        public string dosyaAdi { get; set; }
        public Nullable<System.DateTime> dosyaTarihi { get; set; }
        public Nullable<int> dosyaGonderen { get; set; }
        public string dosyaAciklama { get; set; }
        public string dosyaDurum { get; set; }
        public Nullable<byte> stajTuru { get; set; }
        public Nullable<int> dosyaDegerlendiren { get; set; }
        public string dosyaYolu { get; set; }
    
        public virtual TBL_Akademisyenler TBL_Akademisyenler { get; set; }
        public virtual TBL_Ogrenciler TBL_Ogrenciler { get; set; }
        public virtual TBL_StajTurleri TBL_StajTurleri { get; set; }
    }
}
