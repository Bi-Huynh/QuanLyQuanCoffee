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
    
    public partial class ChiTietPhieuNhap
    {
        public string maChiTietPhieuNhap { get; set; }
        public string maChitietNguyenLieu { get; set; }
        public Nullable<int> soLuong { get; set; }
        public Nullable<double> donGia { get; set; }
        public Nullable<double> thanhTien { get; set; }
        public string maPhieuNhap { get; set; }
    
        public virtual ChiTietNguyenLieu ChiTietNguyenLieu { get; set; }
        public virtual PhieuNhapNguyenLieu PhieuNhapNguyenLieu { get; set; }

        public override string ToString()
        {
            return maChiTietPhieuNhap;
        }
    }
}
