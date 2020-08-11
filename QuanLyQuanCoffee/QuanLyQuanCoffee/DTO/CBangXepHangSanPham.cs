using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCoffee.DTO
{
    class CBangXepHangSanPham
    {
        public int Stt { get; set; }
        public string TenSanPham { get; set; }
        //public int SoLuongHoaDon { get; set; }
        public int SoLuongBan { get; set; }
        public double TongTien { get; set; }

        public CBangXepHangSanPham()
        {
            Stt = 0;
            TenSanPham = "";
            SoLuongBan = 0;
            //SoLuongBan = 0;
            TongTien = 0;
        }

        public CBangXepHangSanPham(int stt, string tenSanPham, int soLuongBan, double tongTien)
        {
            this.Stt = stt;
            this.TenSanPham = tenSanPham;
            this.SoLuongBan = soLuongBan;
            //this.SoLuongBan = soLuongBan;
            this.TongTien = tongTien;
        }
    }
}
