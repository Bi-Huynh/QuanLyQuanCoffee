using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCoffee.DTO
{
    class CBangXepHangSanPham
    {
        public string MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public int SoLuongBan { get; set; }
        public int DonGia { get; set; }
        public double TongTien {
            get
            {
                return DonGia * SoLuongBan;
            }
        }

        public CBangXepHangSanPham()
        {
            MaSanPham = "";
            TenSanPham = "";
            SoLuongBan = 0;
            DonGia = 0;
        }

        public CBangXepHangSanPham(string maSanPham, string tenSanPham, int soLuongBan, int donGia)
        {
            this.MaSanPham = maSanPham;
            this.TenSanPham = tenSanPham;
            this.SoLuongBan = soLuongBan;
            this.DonGia = donGia;
        }
    }
}
