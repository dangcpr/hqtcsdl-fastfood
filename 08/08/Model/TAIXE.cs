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
    
    public partial class TAIXE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TAIXE()
        {
            this.DONDATHANGs = new HashSet<DONDATHANG>();
        }
    
        public string MaTX { get; set; }
        public string CMND { get; set; }
        public string HoTen { get; set; }
        public string SDT { get; set; }
        public string DiaChi { get; set; }
        public string BienSoXe { get; set; }
        public string KhuVucHoatDong { get; set; }
        public string Email { get; set; }
        public string SoTaiKhoan { get; set; }
        public string NganHang { get; set; }
        public string CNNganHang { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DONDATHANG> DONDATHANGs { get; set; }
    }
}
