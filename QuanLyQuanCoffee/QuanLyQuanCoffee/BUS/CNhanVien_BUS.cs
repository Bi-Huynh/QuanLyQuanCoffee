using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCoffee.BUS
{
    class CNhanVien_BUS
    {
        private static QuanLyQuanCoffeeEntities quanLyQuanCoffee = new QuanLyQuanCoffeeEntities();

        // Trả về toàn bộ danh sách nhân viên
        public static List<NhanVien> toList()
        {
            return quanLyQuanCoffee.NhanViens.ToList();
        }

        // Trả về những nhân viên có mã loại được truyền vào
        public static List<NhanVien> toListByLoai(string maLoaiNhanVien)
        {
            return quanLyQuanCoffee.NhanViens.Where(x => x.maLoaiNhanVien == maLoaiNhanVien).ToList();
        }

        // tìm kiếm nhân viên theo mã nhân viên
        public static NhanVien find(string maNhanVien)
        {
            NhanVien nhanVien = quanLyQuanCoffee.NhanViens.Find(maNhanVien);
            return nhanVien == null ? new NhanVien() : nhanVien;
        }

        // tìm kiếm nhân viên theo tên nhân viên
        public static List<NhanVien> findTen(string tenNhanVien)
        {
            tenNhanVien = formatChuoi(tenNhanVien);
            List<NhanVien> list = toList().Where(x => x.tenNhanVien == tenNhanVien).ToList();
            return list == null ? new List<NhanVien>() : list;
        }

        // tìm kiếm nhân viên theo tên nhân viên
        public static List<NhanVien> findTenLoai(string tenLoaiNhanVien)
        {
            tenLoaiNhanVien = formatChuoi(tenLoaiNhanVien);
            List<NhanVien> list = toList().Where(x => x.LoaiNhanVien.tenLoai == tenLoaiNhanVien).ToList();
            return list == null ? new List<NhanVien>() : list;
        }

        // chưa viết hàm này
        public static Boolean kiemTraThongTin(NhanVien nhanVien)
        {
            return true;
        }

        // khởi tạo mã tự động cho nhân viên
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

        // định dạng lại chuỗi được truyền vào thành chuỗi chuẩn
        public static string formatChuoi (string strInput)
        {
            if (strInput == "")
            {
                return "";
            }
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

        public static bool add(NhanVien nhanVien)
        {
            if (kiemTraThongTin(nhanVien))
            {
                quanLyQuanCoffee.NhanViens.Add(nhanVien);
                quanLyQuanCoffee.SaveChanges();
                return true;
            }
            return false;
        }

        public static bool edit(NhanVien nhanVien)
        {
            NhanVien temp = find(nhanVien.maNhanVien);
            if (temp == null || !kiemTraThongTin(nhanVien))
            {
                return false;
            }
            temp.copyData(nhanVien);
            quanLyQuanCoffee.SaveChanges();
            return true;
        }

        public static bool remove(NhanVien nhanVien)
        {
            NhanVien temp = find(nhanVien.maNhanVien);
            if (temp == null)
            {
                return false;
            }
            quanLyQuanCoffee.NhanViens.Remove(temp);
            quanLyQuanCoffee.SaveChanges();
            return true;
        }
    }
}
