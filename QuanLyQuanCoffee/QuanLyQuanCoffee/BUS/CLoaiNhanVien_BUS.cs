using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCoffee.BUS
{
    class CLoaiNhanVien_BUS
    {
        private static QuanLyQuanCoffeeEntities quanLyQuanCoffee = new QuanLyQuanCoffeeEntities();

        public static LoaiNhanVien find(string maLoaiNhanVien)
        {
            return quanLyQuanCoffee.LoaiNhanViens.Find(maLoaiNhanVien);
        }

        public static string findMaLoaiByTenLoai(string tenLoai)
        {
            // so sánh cái tên của loại nhân viên và lấy ra cái mã
            return quanLyQuanCoffee.LoaiNhanViens.Where(x => tenLoai == x.tenLoai).FirstOrDefault().maLoaiNhanvien;
        }

        public static List<LoaiNhanVien> toList()
        {
            return quanLyQuanCoffee.LoaiNhanViens.ToList();
        }

        public static List<string> toListTenLoai()
        {
            return quanLyQuanCoffee.LoaiNhanViens.Select(x => x.tenLoai).ToList();
        }
        
    }
}
