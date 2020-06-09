using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCoffee.BUS
{
    class CLoaiNguyenLieu_BUS
    {
        private static QuanLyQuanCoffeeEntities quanLyQuanCoffee = new QuanLyQuanCoffeeEntities();

        public static List<LoaiNguyenLieu> toList()
        {
            List<LoaiNguyenLieu> list = quanLyQuanCoffee.LoaiNguyenLieux.ToList();
            return list == null ? new List<LoaiNguyenLieu>() : list;
        }

        public static List<string> toListTenLoai()
        {
            List<string> list = quanLyQuanCoffee.LoaiNguyenLieux.Select(x => x.tenLoaiNguyenLieu).ToList();
            return list == null ? new List<string>() : list;
        }

        public static LoaiNguyenLieu find(string maNguyenLieu)
        {
            return quanLyQuanCoffee.LoaiNguyenLieux.Find(maNguyenLieu);
        }

        public static LoaiNguyenLieu find(LoaiNguyenLieu loaiNguyenLieu)
        {
            return find(loaiNguyenLieu.maLoaiNguyenLieu);
        }

        public static string findMaLoaibyTenLoai(string tenLoai)
        {
            // so sánh cái tên của loại nguyên liệu và lấy ra cái mã
            return quanLyQuanCoffee.LoaiNguyenLieux.Where(x => x.tenLoaiNguyenLieu == tenLoai).FirstOrDefault().maLoaiNguyenLieu;

        }
    }
}
