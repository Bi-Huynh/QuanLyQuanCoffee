using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCoffee.DTO
{
    class CBangXepHangNguyenLieuNhap
    {
        public string MaNguyenLieu { get; set; }
        public string TenNguyenLieu { get; set; }
        //public DateTime NgayNhap { get; set; }
        public int SoLuong { get; set; }
        public double DonGia { get; set; }
        public double TongTien
        {
            get
            {
                return DonGia * SoLuong;
            }
        }

        public CBangXepHangNguyenLieuNhap()
        {
            MaNguyenLieu = "";
            TenNguyenLieu = "";
            //NgayNhap = DateTime.Now;
            SoLuong = 0;
            DonGia = 0;
        }

        public CBangXepHangNguyenLieuNhap(string maNguyenLieu, string tenNguyenLieu, int soLuong, double donGia)
        {
            this.MaNguyenLieu = maNguyenLieu;
            this.TenNguyenLieu = tenNguyenLieu;
            //this.NgayNhap = ngayNhap;
            this.SoLuong = soLuong;
            this.DonGia = donGia;
        }
    }
}
