using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCoffee.DTO
{
    class CBangXepHang
    {
        public string MaNhanVien { get; set; }
        public string HoTen { get; set; }
        public int SoLuongHoaDon { get; set; }
        public double TongTien { get; set; }

        public CBangXepHang()
        {
            MaNhanVien = "";
            HoTen = "";
            SoLuongHoaDon = 0;
            TongTien = 0;
        }

        public CBangXepHang(string maNhanVien, string hoTen, int soLuongHoaDon, double tongTien)
        {
            this.MaNhanVien = maNhanVien;
            this.HoTen = hoTen;
            this.SoLuongHoaDon = soLuongHoaDon;
            this.TongTien = tongTien;
        }
    }
}
