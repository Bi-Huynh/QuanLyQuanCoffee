using QuanLyQuanCoffee.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLyQuanCoffee.BUS
{
    class CChiTietPhieuNhapNguyenLieu_BUS
    {
        private static QuanLyQuanCoffeeEntities quanLyQuanCoffee = new QuanLyQuanCoffeeEntities();

        // Trả về toàn bộ danh sách nhân viên
        public static List<ChiTietPhieuNhapNguyenLieu> toList()
        {
            List<ChiTietPhieuNhapNguyenLieu> list = quanLyQuanCoffee.ChiTietPhieuNhapNguyenLieux.ToList();
            return list == null ? new List<ChiTietPhieuNhapNguyenLieu>() : list;
        }

        // Trả về những nhân viên có mã loại được truyền vào
        public static List<ChiTietPhieuNhapNguyenLieu> toListByLoai(string maPhieuNhap)
        {
            List<ChiTietPhieuNhapNguyenLieu> list = quanLyQuanCoffee.ChiTietPhieuNhapNguyenLieux.Where(x => x.maPhieuNhap == maPhieuNhap).ToList();
            return list == null ? new List<ChiTietPhieuNhapNguyenLieu>() : list;
        }

        // tìm kiếm nhân viên theo mã nhân viên
        public static ChiTietPhieuNhapNguyenLieu find(string maPhieuNhap)
        {
            ChiTietPhieuNhapNguyenLieu chiTietPhieuNhapNguyenLieu = quanLyQuanCoffee.ChiTietPhieuNhapNguyenLieux.Find(maPhieuNhap);
            return chiTietPhieuNhapNguyenLieu == null ? new ChiTietPhieuNhapNguyenLieu() : chiTietPhieuNhapNguyenLieu;
        }

        public static bool add(ChiTietPhieuNhapNguyenLieu chiTietPhieuNhapNguyenLieu)
        {
            if (CServices.kiemTraThongTin(chiTietPhieuNhapNguyenLieu))
            {
                quanLyQuanCoffee.ChiTietPhieuNhapNguyenLieux.Add(chiTietPhieuNhapNguyenLieu);
                quanLyQuanCoffee.SaveChanges();
                return true;
            }
            return false;
        }

        public static bool edit(ChiTietPhieuNhapNguyenLieu chiTietPhieuNhapNguyenLieu)
        {
            ChiTietPhieuNhapNguyenLieu temp = find(chiTietPhieuNhapNguyenLieu.maPhieuNhap);
            if (temp == null)
            {
                MessageBox.Show("Không tìm thấy chi tiết phiếu nhập nguyên liệu để sửa thông tin");
                return false;
            }
            if (!CServices.kiemTraThongTin(chiTietPhieuNhapNguyenLieu))
            {
                MessageBox.Show("Thông tin không hợp lệ");
                return false;
            }
            temp.maPhieuNhap = chiTietPhieuNhapNguyenLieu.maPhieuNhap;
            temp.maNguyenLieu = chiTietPhieuNhapNguyenLieu.maNguyenLieu;
            temp.thanhTien = chiTietPhieuNhapNguyenLieu.thanhTien;
            quanLyQuanCoffee.SaveChanges();
            return true;
        }

        public static bool remove(ChiTietPhieuNhapNguyenLieu ChiTietPhieuNhapNguyenLieu)
        {
            ChiTietPhieuNhapNguyenLieu temp = find(ChiTietPhieuNhapNguyenLieu.maPhieuNhap);
            if (temp == null)
            {
                MessageBox.Show("Không tìm thấy chi tiết phiếu nhập nguyên liệu để xóa");
                return false;
            }
            if (temp.PhieuNhapNguyenLieu != null)
            {
                MessageBox.Show("Không thể xóa chi tiết phiếu nhập nguyên liệu này");
                return false;
            }
            quanLyQuanCoffee.ChiTietPhieuNhapNguyenLieux.Remove(temp);
            quanLyQuanCoffee.SaveChanges();
            return true;
        }
    }
}
