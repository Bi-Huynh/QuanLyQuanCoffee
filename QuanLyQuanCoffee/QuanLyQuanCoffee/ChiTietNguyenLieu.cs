//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLyQuanCoffee
{
    using System;
    using System.Collections.Generic;
    
    public partial class ChiTietNguyenLieu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ChiTietNguyenLieu()
        {
            this.ChiTietPhieuNhaps = new HashSet<ChiTietPhieuNhap>();
            this.ChiTietPhieuXuats = new HashSet<ChiTietPhieuXuat>();
        }
    
        public string maChiTietNguyenLieu { get; set; }
        public string maNguyenLieu { get; set; }
        public Nullable<int> soLuong { get; set; }
        public Nullable<System.DateTime> ngayHetHan { get; set; }
        public string donViTinh { get; set; }
    
        public virtual NguyenLieu NguyenLieu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietPhieuXuat> ChiTietPhieuXuats { get; set; }

        public override string ToString()
        {
            return maChiTietNguyenLieu;
        }
    }
}
