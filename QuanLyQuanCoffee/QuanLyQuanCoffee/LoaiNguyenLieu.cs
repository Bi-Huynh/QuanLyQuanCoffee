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
    
    public partial class LoaiNguyenLieu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LoaiNguyenLieu()
        {
            this.NguyenLieux = new HashSet<NguyenLieu>();
        }
    
        public string maLoaiNguyenLieu { get; set; }
        public string tenLoaiNguyenLieu { get; set; }

        public void copyData(LoaiNguyenLieu loaiNguyenLieu)
        {
            this.maLoaiNguyenLieu = loaiNguyenLieu.maLoaiNguyenLieu;
            this.tenLoaiNguyenLieu = loaiNguyenLieu.tenLoaiNguyenLieu;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NguyenLieu> NguyenLieux { get; set; }
    }
}
