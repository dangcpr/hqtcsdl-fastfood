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
    
    public partial class DOITAC
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DOITAC()
        {
            this.CHINHANHs = new HashSet<CHINHANH>();
            this.HOPDONGs = new HashSet<HOPDONG>();
            this.THUCPHAMs = new HashSet<THUCPHAM>();
        }
    
        public string MaDT { get; set; }
        public string Email { get; set; }
        public string NgDaiDien { get; set; }
        public Nullable<short> SLChiNhanh { get; set; }
        public string TenQuan { get; set; }
        public string LoaiTP { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHINHANH> CHINHANHs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HOPDONG> HOPDONGs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<THUCPHAM> THUCPHAMs { get; set; }
    }
}
