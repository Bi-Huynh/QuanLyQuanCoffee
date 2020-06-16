using Prism.Regions;
using QuanLyQuanCoffee.BUS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLyQuanCoffee.Services
{
    class CServices
    {
        private static QuanLyQuanCoffeeEntities quanLyQuanCoffee = new QuanLyQuanCoffeeEntities();

        // khởi tạo mã tự động
        public static string randomMa()
        {
            Random random = new Random();
            string ma = "";
            for (int i = 0; i < 10; i++)
            {
                ma += Convert.ToString((char)random.Next(65, 90));
            }
            return ma;
        }

        public static string taoMaNhanVien()
        {
            string ma = "NV";
            var list = CNhanVien_BUS.toList();
            if (list.Count == 0)
            {
                return "NV00000001";
            }
            NhanVien nhanVien = list[list.Count() - 1];
            string chuoi1 = nhanVien.maNhanVien;
            chuoi1 = chuoi1.Remove(0, 2);   // bỏ 2 ký tự đầu tiên đi - NV
            int temp = int.Parse(chuoi1);
            temp++;
            chuoi1 = String.Format("{0:00000000}", temp);
            ma += chuoi1;

            return ma;
        }

        // định dạng lại chuỗi được truyền vào thành chuỗi chuẩn
        public static string formatChuoi(string strInput)
        {
            if (strInput == "")
            {
                return "";
            }
            string result = "";
            result = strInput.Trim();
            while (result.Contains("  ") == true)
            {
                result = result.Replace("  ", " ");
            }
            result = result.ToLower();
            string[] arr = result.Split(' ');
            result = string.Empty;
            foreach (var item in arr)
            {
                String temp = item.Substring(0, 1).ToUpper() + item.Substring(1);
                result += temp + " ";
            }
            result = result.Trim();
            return result;
        }

        private static bool kiemTraTonTaiSo(string chuoi)
        {
            foreach (char item in chuoi)
            {
                if ((int)item >= 48 && (int)item <= 57)
                {
                    return true;
                }
            }
            return false;
        }

        private static bool kiemTraTonTaiChu(string chuoi)
        {
            foreach (char item in chuoi)
            {
                if ((int)item < 48 || (int)item > 57)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool kiemTraThongTin(NhanVien nhanVien)
        {
            if (kiemTraTonTaiSo(nhanVien.hoNhanVien) == true ||
                kiemTraTonTaiSo(nhanVien.tenNhanVien) == true)
            {
                MessageBox.Show("Họ Tên không được có ký tự số");
                return false;
            }
            if (kiemTraTonTaiChu(nhanVien.soDienThoai.Trim()) == true ||
                kiemTraTonTaiChu(nhanVien.cMND.Trim()) == true)
            {
                MessageBox.Show("số điện thoại và CMND chỉ có ký tự số");
                return false;
            }
            if (nhanVien.cMND.Trim().Count() != 9 && nhanVien.cMND.Trim().Count() != 12)
            {
                MessageBox.Show("CMND chỉ có 9 ký tự số hoặc 12 ký tự số");
                return false;
            }
            if (nhanVien.soDienThoai.Trim().Count() != 10)
            {
                MessageBox.Show("Số điện thoại chỉ có 10 số");
                return false;
            }
            return true;
        }

        public static bool kiemTraThongTin(LoaiNhanVien loaiNhanVien)
        {
            return true;
        }

        public static bool kiemTraThongTin(NguyenLieu nguyenLieu)
        {
            return true;
        }

        public static bool kiemTraThongTin(LoaiNguyenLieu loaiNguyenLieu)
        {
            return true;
        }

        public static bool kiemTraThongTin(PhieuNhapNguyenLieu phieuNhapNguyenLieu)
        {
            return true;
        }

        public static bool kiemTraThongTin(ChiTietPhieuNhapNguyenLieu chiTietPhieuNhapNguyenLieu)
        {
            return true;
        }
    }
}
