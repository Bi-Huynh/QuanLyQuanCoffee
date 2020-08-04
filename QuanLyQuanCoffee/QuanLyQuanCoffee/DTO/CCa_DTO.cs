using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCoffee.DTO
{
    public class CCa_DTO
    {
        private string maNhanVien;
        private DateTime gioBatDau;
        private DateTime gioKetThuc;

        public CCa_DTO()
        {
            this.MaNhanVien = "";
            this.GioBatDau = DateTime.Now;
        }

        public CCa_DTO(string maNhanVien, DateTime gioBatDau)
        {
            this.MaNhanVien = maNhanVien;
            this.GioBatDau = gioBatDau;
        }

        public CCa_DTO(string maNhanVien, DateTime gioBatDau, DateTime gioKetThuc)
        {
            this.MaNhanVien = maNhanVien;
            this.GioBatDau = gioBatDau;
            this.gioKetThuc = gioKetThuc;
        }

        public string MaNhanVien { get => maNhanVien; set => maNhanVien = value; }
        public DateTime GioBatDau { get => gioBatDau; set => gioBatDau = value; }
        public DateTime GioKetThuc { get => gioKetThuc; set => gioKetThuc = value; }
    }
}
