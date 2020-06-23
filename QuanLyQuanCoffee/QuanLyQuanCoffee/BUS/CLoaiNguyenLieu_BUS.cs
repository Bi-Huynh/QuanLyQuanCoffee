using QuanLyQuanCoffee.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLyQuanCoffee.BUS
{
    class CLoaiNguyenLieu_BUS
    {
        private static QuanLyQuanCoffeeEntities1 quanLyQuanCoffee = new QuanLyQuanCoffeeEntities1();

        public static List<LoaiNguyenLieu> toList()
        {
            List<LoaiNguyenLieu> list = quanLyQuanCoffee.LoaiNguyenLieux.Where(x => x.trangThai == 0).ToList();
            return list == null ? new List<LoaiNguyenLieu>() : list;
        }

        public static List<LoaiNguyenLieu> toListAll()
        {
            List<LoaiNguyenLieu> list = quanLyQuanCoffee.LoaiNguyenLieux.ToList();
            return list == null ? new List<LoaiNguyenLieu>() : list;
        }

        public static List<string> toListTenLoai()
        {
            List<string> list = quanLyQuanCoffee.LoaiNguyenLieux.Select(x => x.tenLoaiNguyenLieu).ToList();
            return list == null ? new List<string>() : list;
        }

        public static List<string> toListMa()
        {
            List<string> list = quanLyQuanCoffee.LoaiNguyenLieux.Select(x => x.maLoaiNguyenLieu).ToList();
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

        public static LoaiNguyenLieu findMaLoaibyTenLoai(string tenLoai)
        {
            // so sánh cái tên của loại nguyên liệu và lấy ra cái mã
            LoaiNguyenLieu loaiNguyenLieu = quanLyQuanCoffee.LoaiNguyenLieux.Where(x => x.tenLoaiNguyenLieu == tenLoai).FirstOrDefault();
            return loaiNguyenLieu == null ? new LoaiNguyenLieu() : loaiNguyenLieu;
        }

        public static bool add(LoaiNguyenLieu loaiNguyenLieu)
        {
            if (CServices.kiemTraThongTin(loaiNguyenLieu))
            {
                quanLyQuanCoffee.LoaiNguyenLieux.Add(loaiNguyenLieu);
                quanLyQuanCoffee.SaveChanges();
                return true;
            }
            return false;
        }

        public static bool edit(LoaiNguyenLieu loaiNguyenLieu)
        {
            LoaiNguyenLieu temp = find(loaiNguyenLieu.maLoaiNguyenLieu);
            if (temp == null || !CServices.kiemTraThongTin(loaiNguyenLieu))
            {
                return false;
            }
            temp.maLoaiNguyenLieu = loaiNguyenLieu.maLoaiNguyenLieu;
            temp.tenLoaiNguyenLieu = loaiNguyenLieu.tenLoaiNguyenLieu;
            quanLyQuanCoffee.SaveChanges();
            return true;
        }

        public static bool remove(LoaiNguyenLieu loaiNguyenLieu)
        {
            LoaiNguyenLieu temp = find(loaiNguyenLieu.maLoaiNguyenLieu);

            if (temp == null)
            {
                MessageBox.Show("Không tìm thấy loại nhân viên để xóa");
                return false;
            }

            if (temp.NguyenLieux.Count > 0)
            {
                MessageBox.Show("Không thể xóa loại nguyên liệu này");
                return false;
            }

            quanLyQuanCoffee.LoaiNguyenLieux.Remove(temp);
            quanLyQuanCoffee.SaveChanges();
            return true;
        }

    }
}
