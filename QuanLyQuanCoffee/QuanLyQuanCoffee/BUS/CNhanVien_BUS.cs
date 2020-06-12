using QuanLyQuanCoffee.Services;
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
            List<NhanVien> list = quanLyQuanCoffee.NhanViens.ToList();
            return list == null ? new List<NhanVien>() : list;
        }

        // Trả về những nhân viên có mã loại được truyền vào
        public static List<NhanVien> toListByLoai(string maLoaiNhanVien)
        {
            List<NhanVien> list = quanLyQuanCoffee.NhanViens.Where(x => x.maLoaiNhanVien == maLoaiNhanVien).ToList();
            return list == null ? new List<NhanVien>() : list;
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
            tenNhanVien = CServices.formatChuoi(tenNhanVien);
            List<NhanVien> list = toList().Where(x => x.tenNhanVien == tenNhanVien).ToList();
            return list == null ? new List<NhanVien>() : list;
        }

        // tìm kiếm nhân viên theo tên nhân viên
        public static List<NhanVien> findTenLoai(string tenLoaiNhanVien)
        {
            tenLoaiNhanVien = CServices.formatChuoi(tenLoaiNhanVien);
            List<NhanVien> list = toList().Where(x => x.LoaiNhanVien.tenLoai == tenLoaiNhanVien).ToList();
            return list == null ? new List<NhanVien>() : list;
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
            if (CServices.kiemTraThongTin(nhanVien))
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
            if (temp == null || !CServices.kiemTraThongTin(nhanVien))
            {
                return false;
            }
            if (!CServices.kiemTraThongTin(nhanVien))
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
