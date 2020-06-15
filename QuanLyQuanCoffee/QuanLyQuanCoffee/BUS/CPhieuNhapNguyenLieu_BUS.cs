using QuanLyQuanCoffee.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLyQuanCoffee.BUS
{
    class CPhieuNhapNguyenLieu_BUS
    {
        private static QuanLyQuanCoffeeEntities quanLyQuanCoffee = new QuanLyQuanCoffeeEntities();

        // Trả về toàn bộ danh sách nhân viên
        public static List<PhieuNhapNguyenLieu> toList()
        {
            List<PhieuNhapNguyenLieu> list = quanLyQuanCoffee.PhieuNhapNguyenLieus.ToList();
            return list == null ? new List<PhieuNhapNguyenLieu>() : list;
        }

        // Trả về những nhân viên có mã loại được truyền vào
        public static List<PhieuNhapNguyenLieu> toListByLoai(string maPhieuNhap)
        {
            List<PhieuNhapNguyenLieu> list = quanLyQuanCoffee.PhieuNhapNguyenLieus.Where(x => x.maPhieuNhap == maPhieuNhap).ToList();
            return list == null ? new List<PhieuNhapNguyenLieu>() : list;
        }

        // tìm kiếm nhân viên theo mã nhân viên
        public static PhieuNhapNguyenLieu find(string maPhieuNhap)
        {
            PhieuNhapNguyenLieu PhieuNhapNguyenLieu = quanLyQuanCoffee.PhieuNhapNguyenLieus.Find(maPhieuNhap);
            return PhieuNhapNguyenLieu == null ? new PhieuNhapNguyenLieu() : PhieuNhapNguyenLieu;
        }

        public static bool add(PhieuNhapNguyenLieu PhieuNhapNguyenLieu)
        {
            if (CServices.kiemTraThongTin(PhieuNhapNguyenLieu))
            {
                quanLyQuanCoffee.PhieuNhapNguyenLieus.Add(PhieuNhapNguyenLieu);
                quanLyQuanCoffee.SaveChanges();
                return true;
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

        public static bool remove(PhieuNhapNguyenLieu phieuNhapNguyenLieu)
        {
            PhieuNhapNguyenLieu temp = find(phieuNhapNguyenLieu.maPhieuNhap);
            if (temp == null)
            {
                MessageBox.Show("Không tìm thấy phiếu nhập nguyên liệu để xóa");
                return false;
            }
            if (temp.ChiTietPhieuNhapNguyenLieus.Count > 0 ||
                temp.NhanVien != null)
            {
                MessageBox.Show("Không thể xóa phiếu nhập nguyên liệu này");
                return false;
            }
            quanLyQuanCoffee.PhieuNhapNguyenLieus.Remove(temp);
            quanLyQuanCoffee.SaveChanges();
            return true;
        }
    }
}
