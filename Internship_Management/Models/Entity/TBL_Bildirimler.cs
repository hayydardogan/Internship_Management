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
    
    public partial class TBL_Bildirimler
    {
        public int bildirimID { get; set; }
        public Nullable<int> bildirimGonderen { get; set; }
        public Nullable<int> bildirimAlan { get; set; }
        public Nullable<System.DateTime> bildirimTarih { get; set; }
        public string bildirimIcerik { get; set; }
        public Nullable<bool> bildirimOkunduMu { get; set; }
    
        public virtual TBL_Akademisyenler TBL_Akademisyenler { get; set; }
        public virtual TBL_Ogrenciler TBL_Ogrenciler { get; set; }
    }
}