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
    
    public partial class PhieuXuatNguyenLieu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PhieuXuatNguyenLieu()
        {
            this.ChiTietPhieuXuats = new HashSet<ChiTietPhieuXuat>();
        }
    
        public string maPhieuXuat { get; set; }
        public Nullable<System.DateTime> ngayXuat { get; set; }
        public Nullable<double> tongThanhTien { get; set; }
        public string maNhanVien { get; set; }
        public Nullable<int> trangThai { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietPhieuXuat> ChiTietPhieuXuats { get; set; }
        public virtual NhanVien NhanVien { get; set; }

        public override string ToString()
        {
            return maPhieuXuat;
        }
    }
}
