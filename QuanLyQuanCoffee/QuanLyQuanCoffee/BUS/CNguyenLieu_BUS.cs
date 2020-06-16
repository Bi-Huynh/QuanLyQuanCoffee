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
    class CNguyenLieu_BUS
    {
        private static QuanLyQuanCoffeeEntities quanLyQuanCoffee = new QuanLyQuanCoffeeEntities();

        public static List<NguyenLieu> toList()
        {
            List<NguyenLieu> list = quanLyQuanCoffee.NguyenLieux.ToList();
            return list == null ? new List<NguyenLieu>() : list;
        }

        public static NguyenLieu find(string maNguyenLieu)
        {
            return quanLyQuanCoffee.NguyenLieux.Find(maNguyenLieu);
        }

        public static NguyenLieu find(NguyenLieu nguyenLieu)
        {
            return find(nguyenLieu.maNguyenLieu);
        }

        public static List<NguyenLieu> findTen(string tenNguyenLieu)
        {
            List<NguyenLieu> list = quanLyQuanCoffee.NguyenLieux.Where(x => x.tenNguyenLieu == tenNguyenLieu).ToList();
            return list == null ? new List<NguyenLieu>() : list;
        }

        public static List<NguyenLieu> findMa(string maNguyenLieu)
        {
            List<NguyenLieu> list = quanLyQuanCoffee.NguyenLieux.Where(x => x.maNguyenLieu.Contains(maNguyenLieu) == true).ToList();
            return list == null ? new List<NguyenLieu>() : list;
        }

        public static List<NguyenLieu> findTenLoai(string tenLoai)
        {
            tenLoai = CServices.formatChuoi(tenLoai);
            List<NguyenLieu> list = quanLyQuanCoffee.NguyenLieux.Where(x => x.LoaiNguyenLieu.tenLoaiNguyenLieu == tenLoai).ToList();
            return list == null ? new List<NguyenLieu>() : list;
        }

        public static bool add(NguyenLieu nguyenLieu)
        {
            if (CServices.kiemTraThongTin(nguyenLieu))
            {
                try
                {
                    nguyenLieu.tenNguyenLieu = CServices.formatChuoi(nguyenLieu.tenNguyenLieu);

                    quanLyQuanCoffee.NguyenLieux.Add(nguyenLieu);
                    quanLyQuanCoffee.SaveChanges();
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
            return true;
        }

        public static bool edit(NguyenLieu nguyenLieu)
        {
            NguyenLieu temp = find(nguyenLieu.maNguyenLieu);
            if (temp == null)
            {
                return false;
            }
            if (!CServices.kiemTraThongTin(nguyenLieu))
            {
                return false;
            }

            temp.maNguyenLieu = nguyenLieu.maNguyenLieu;
            temp.tenNguyenLieu = nguyenLieu.tenNguyenLieu;
            temp.donGia = nguyenLieu.donGia;
            temp.soLuong = nguyenLieu.soLuong;
            temp.donViTinh = nguyenLieu.donViTinh;
            temp.ngayHetHan = nguyenLieu.ngayHetHan;
            temp.ngayNhap = nguyenLieu.ngayNhap;
            temp.maLoaiNguyenLieu = nguyenLieu.maLoaiNguyenLieu;

            quanLyQuanCoffee.SaveChanges();
            return true;
        }

        public static bool remove(NguyenLieu nguyenLieu)
        {
            NguyenLieu temp = find(nguyenLieu);
            if (temp == null)
            {
                MessageBox.Show("Không tìm thấy nguyên liệu để xóa");
                return false;
            }
            if (temp.ChiTietPhieuNhapNguyenLieux.Count > 0  || temp.ChiTietXuatNhapNguyenLieux.Count > 0)
            {
                MessageBox.Show("Không thể xóa nguyên liệu này");
                return false;
            }
            quanLyQuanCoffee.NguyenLieux.Remove(temp);
            quanLyQuanCoffee.SaveChanges();
            return true;
        }
    }
}
