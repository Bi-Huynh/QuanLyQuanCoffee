using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCoffee.DTO
{
    class CBangXepHang
    {
        //private int stt;
        //private string hoTen;
        //private int soLuongHoaDon;
        //private int soLuongBan;
        //private double tongTien;
        public int Stt { get; set; }
        public string HoTen { get; set; }
        public string ChuVu { get; set; }
        public int SoLuongHoaDon { get; set; }
        public int SoLuongBan { get; set; }
        public double TongTien { get; set; }

        public CBangXepHang()
        {
            Stt = 0;
            HoTen = "";
            ChuVu = "";
            SoLuongHoaDon = 0;
            //SoLuongBan = 0;
            TongTien = 0;
        }

        public CBangXepHang(int stt, string hoTen, string chuVu, int soLuongHoaDon, double tongTien)
        {
            this.Stt = stt;
            this.HoTen = hoTen;
            this.ChuVu = chuVu;
            this.SoLuongHoaDon = soLuongHoaDon;
            //this.SoLuongBan = soLuongBan;
            this.TongTien = tongTien;
        }
    }
}
