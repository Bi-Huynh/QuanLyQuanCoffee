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
    
    public partial class LoaiNhanVien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LoaiNhanVien()
        {
            this.NhanViens = new HashSet<NhanVien>();
        }
    
        public string maLoaiNhanvien { get; set; }
        public string tenLoai { get; set; }
        public double luongCoBan { get; set; }
        public Nullable<int> trangThai { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NhanVien> NhanViens { get; set; }

        public override string ToString()
        {
            return maLoaiNhanvien;
        }
    }
}
