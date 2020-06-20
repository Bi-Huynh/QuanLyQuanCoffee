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
    
    public partial class NhanVien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NhanVien()
        {
            this.ChiTietChamCongs = new HashSet<ChiTietChamCong>();
            this.HoaDons = new HashSet<HoaDon>();
            this.Luongs = new HashSet<Luong>();
            this.PhieuNhapNguyenLieux = new HashSet<PhieuNhapNguyenLieu>();
            this.PhieuXuatNguyenLieux = new HashSet<PhieuXuatNguyenLieu>();
        }
    
        public string maNhanVien { get; set; }
        public string hoNhanVien { get; set; }
        public string tenNhanVien { get; set; }
        public string soDienThoai { get; set; }
        public System.DateTime ngaySinh { get; set; }
        public bool phai { get; set; }
        public string cMND { get; set; }
        public string thuongTru { get; set; }
        public string tamTru { get; set; }
        public System.DateTime ngayVaoLam { get; set; }
        public string maLoaiNhanVien { get; set; }
        public Nullable<int> trangThai { get; set; }
        public string urlAnh { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietChamCong> ChiTietChamCongs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoaDon> HoaDons { get; set; }
        public virtual LoaiNhanVien LoaiNhanVien { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Luong> Luongs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuNhapNguyenLieu> PhieuNhapNguyenLieux { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuXuatNguyenLieu> PhieuXuatNguyenLieux { get; set; }
        public virtual TaiKhoan TaiKhoan { get; set; }
    }
}
