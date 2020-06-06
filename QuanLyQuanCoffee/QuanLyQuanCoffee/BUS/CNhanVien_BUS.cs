using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCoffee.BUS
{
    class CNhanVien_BUS
    {
        public static Boolean kiemTraThongTin(NhanVien nhanVien)
        {
            return true;
        }

        public static string randomMaNhanVien()
        {
            Random random = new Random();
            string ma = "";
            for (int i = 0; i < 10; i++)
            {
                ma += Convert.ToString((char)random.Next(65, 90));
            }
            return ma;
        }

        public static string chuanChuoi (string strInput)
        {
            string result = "";
            result = strInput.Trim();
            while(result.Contains("  ") == true)
            {
                result = result.Replace("  ", " ");
            }
            result = result.ToLower();
            string[] arr = result.Split(' ');
            result = string.Empty;
            foreach(var item in arr)
            {
                String temp = item.Substring(0, 1).ToUpper() + item.Substring(1);
                result += temp + " ";
            }
            result = result.Trim(); 
            return result;
        }

        public static int tinhTuoi(DateTime ngaySinh)
        {
            int tuoi = 0;
            int now = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
            int ns = int.Parse(ngaySinh.ToString("yyyyMMdd"));
            tuoi = (now - ns) / 10000;
            return tuoi;
        }

        public static int tinhTuoi(NhanVien nhanVien)
        {
            return tinhTuoi(nhanVien.ngaySinh);
        }
    }
}
