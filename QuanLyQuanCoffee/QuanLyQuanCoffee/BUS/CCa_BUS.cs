using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCoffee.BUS
{
    public class CCa_BUS
    {
        private string maNhanVien;
        private DateTime gioBatDau;

        public CCa_BUS()
        {
            this.MaNhanVien = "";
            this.GioBatDau = DateTime.Now;
        }

        public CCa_BUS(string maNhanVien, DateTime gioBatDau)
        {
            this.MaNhanVien = maNhanVien;
            this.GioBatDau = gioBatDau;
        }

        public string MaNhanVien { get => maNhanVien; set => maNhanVien = value; }
        public DateTime GioBatDau { get => gioBatDau; set => gioBatDau = value; }
    }
}
