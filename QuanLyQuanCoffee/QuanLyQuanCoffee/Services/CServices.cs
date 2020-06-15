using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCoffee.Services
{
    class CServices
    {
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

        public static bool kiemTraThongTin(NhanVien nhanVien)
        {
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
    }
}
