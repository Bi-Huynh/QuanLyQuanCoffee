using QuanLyQuanCoffee.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLyQuanCoffee.BUS
{
    class CPhieuNhapNguyenLieu_BUS
    {
        private static QuanLyQuanCoffeeEntities1 quanLyQuanCoffee = new QuanLyQuanCoffeeEntities1();

        // Trả về toàn bộ danh sách nhân viên
        public static List<PhieuNhapNguyenLieu> toList()
        {
            List<PhieuNhapNguyenLieu> list = quanLyQuanCoffee.PhieuNhapNguyenLieux
                .Where(x => x.trangThai == 0).ToList();
            return list == null ? new List<PhieuNhapNguyenLieu>() : list;
        }

        public static List<PhieuNhapNguyenLieu> toListAll()
        {
            List<PhieuNhapNguyenLieu> list = quanLyQuanCoffee.PhieuNhapNguyenLieux.ToList();
            return list == null ? new List<PhieuNhapNguyenLieu>() : list;
        }

        // Trả về những nhân viên có mã loại được truyền vào
        public static List<PhieuNhapNguyenLieu> toListMa(string maPhieuNhap)
        {
            List<PhieuNhapNguyenLieu> list = quanLyQuanCoffee.PhieuNhapNguyenLieux.
                Where(x => x.maPhieuNhap.Contains(maPhieuNhap) == true && x.trangThai == 0).ToList();
            return list == null ? new List<PhieuNhapNguyenLieu>() : list;
        }

        // tìm kiếm nhân viên theo mã nhân viên
        public static PhieuNhapNguyenLieu find(string maPhieuNhap)
        {
            PhieuNhapNguyenLieu PhieuNhapNguyenLieu = quanLyQuanCoffee.PhieuNhapNguyenLieux.Find(maPhieuNhap);
            return PhieuNhapNguyenLieu == null ? new PhieuNhapNguyenLieu() : PhieuNhapNguyenLieu;
        }

        public static List<PhieuNhapNguyenLieu> toListNgayNhap(DateTime ngayNhap)
        {
            List<PhieuNhapNguyenLieu> list = quanLyQuanCoffee.PhieuNhapNguyenLieux.
                Where(x => x.ngayNhap == ngayNhap && x.trangThai == 0).ToList();

            return list == null ? new List<PhieuNhapNguyenLieu>() : list;
        }

        public static List<PhieuNhapNguyenLieu> toListTongThanhTien(double tongThanhTien)
        {
            List<PhieuNhapNguyenLieu> list = quanLyQuanCoffee.PhieuNhapNguyenLieux.
                Where(x => x.tongThanhTien >= tongThanhTien && x.trangThai == 0).ToList();

            return list == null ? new List<PhieuNhapNguyenLieu>() : list;
        }

        public static List<PhieuNhapNguyenLieu> findNgayNhap(DateTime ngayNhap)
        {
            List<PhieuNhapNguyenLieu> list = quanLyQuanCoffee.PhieuNhapNguyenLieux.
                Where(x => x.ngayNhap == ngayNhap).ToList();
            return list == null ? new List<PhieuNhapNguyenLieu>() : list;
        }

        public static bool add(PhieuNhapNguyenLieu PhieuNhapNguyenLieu)
        {
            if (CServices.kiemTraThongTin(PhieuNhapNguyenLieu))
            {
                try
                {
                    quanLyQuanCoffee.PhieuNhapNguyenLieux.Add(PhieuNhapNguyenLieu);
                    quanLyQuanCoffee.SaveChanges();
                    return true;
                }
                catch (DbUpdateException)
                {
                    MessageBox.Show("Lỗi! Không thể thêm dữ liệu");
                    return false;
                }
                catch (DbEntityValidationException)
                {
                    MessageBox.Show("Lỗi! Kiểu dữ liệu được truyền vào không hợp lệ");
                    return false;
                }
            }
            return false;
        }

        public static bool edit(PhieuNhapNguyenLieu phieuNhapNguyenLieu)
        {
            PhieuNhapNguyenLieu temp = find(phieuNhapNguyenLieu.maPhieuNhap);
            if (temp == null)
            {
                MessageBox.Show("Không tìm thấy phiếu nhập nguyên liệu để sửa thông tin");
                return false;
            }
            if (!CServices.kiemTraThongTin(phieuNhapNguyenLieu))
            {
                MessageBox.Show("Thông tin không hợp lệ");
                return false;
            }
            temp.maPhieuNhap = phieuNhapNguyenLieu.maPhieuNhap;
            temp.maNhanVien = phieuNhapNguyenLieu.maNhanVien;
            temp.ngayNhap = phieuNhapNguyenLieu.ngayNhap;
            temp.tongThanhTien = phieuNhapNguyenLieu.tongThanhTien;
            quanLyQuanCoffee.SaveChanges();
            return true;
        }

        public static bool remove(string maPhieuNhap)
        {
            PhieuNhapNguyenLieu temp = find(maPhieuNhap);
            if (temp == null)
            {
                MessageBox.Show("Không tìm thấy phiếu nhập nguyên liệu để xóa");
                return false;
            }
            try
            {
                if (temp.ChiTietPhieuNhaps.Count > 0)
                {
                    temp.trangThai = 1;
                    quanLyQuanCoffee.SaveChanges();
                }
                else
                {
                    quanLyQuanCoffee.PhieuNhapNguyenLieux.Remove(temp);
                    quanLyQuanCoffee.SaveChanges();
                }
            }
            catch (DbUpdateException)
            {
                MessageBox.Show("Lỗi! Không thể xóa Phiếu nhập này");
                return false;
            }

            return true;
        }

        public static bool remove(PhieuNhapNguyenLieu phieuNhapNguyenLieu)
        {
            return remove(phieuNhapNguyenLieu.maPhieuNhap);
        }
    }
}
