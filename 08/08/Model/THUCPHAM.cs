//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace _08.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class THUCPHAM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public THUCPHAM()
        {
            this.CHITIETDONDATHANGs = new HashSet<CHITIETDONDATHANG>();
        }
    
        public string MaTP { get; set; }
        public string MaDT { get; set; }
        public string TenMon { get; set; }
        public string MieuTa { get; set; }
        public Nullable<decimal> Gia { get; set; }
        public string TinhTrang { get; set; }
        public string TuyChon { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETDONDATHANG> CHITIETDONDATHANGs { get; set; }
        public virtual DOITAC DOITAC { get; set; }
    }
}
