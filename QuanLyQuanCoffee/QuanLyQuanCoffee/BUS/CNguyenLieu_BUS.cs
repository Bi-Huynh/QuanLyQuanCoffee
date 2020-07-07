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
        private static QuanLyQuanCoffeeEntities1 quanLyQuanCoffee = new QuanLyQuanCoffeeEntities1();

        public static List<NguyenLieu> to_List()
        {
            List<NguyenLieu> list = quanLyQuanCoffee.NguyenLieux
                .Where(x => x.trangThai == 0).ToList();
            return list == null ? new List<NguyenLieu>() : list;
        }

        public static List<NguyenLieu> toList()
        {
            List<NguyenLieu> list = quanLyQuanCoffee.NguyenLieux.ToList();
            return list == null ? new List<NguyenLieu>() : list;
        }

        public static List<string> toListTen()
        {
            List<string> list = quanLyQuanCoffee.NguyenLieux
                .Where(x => x.trangThai == 0).Select(x => x.tenNguyenLieu.Trim()).ToList();
            return list == null ? new List<string>() : list;
        }

        public static NguyenLieu find(string maNguyenLieu)
        {
            NguyenLieu nguyenLieu = quanLyQuanCoffee.NguyenLieux.Find(maNguyenLieu);
            return nguyenLieu == null ? new NguyenLieu() : nguyenLieu;
        }

        public static NguyenLieu find(NguyenLieu nguyenLieu)
        {
            return find(nguyenLieu.maNguyenLieu);
        }

        public static List<NguyenLieu> findTen(string tenNguyenLieu)
        {
            List<NguyenLieu> list = quanLyQuanCoffee.NguyenLieux
                .Where(x => x.tenNguyenLieu.Contains(tenNguyenLieu) && x.trangThai == 0).ToList();
            return list == null ? new List<NguyenLieu>() : list;
        }

        public static string findTenNguyenLieu(string maNguyenLieu)
        {
            string tenNguyenLieu = quanLyQuanCoffee.NguyenLieux
                .Where(x => x.maNguyenLieu == maNguyenLieu && x.trangThai == 0).FirstOrDefault().tenNguyenLieu;
            return tenNguyenLieu == null ? "Null" : tenNguyenLieu.Trim();
        }

        public static string findTenByMaChiTietNguyenLieu(string maChiTietNguyenLieu)
        {
            string tenNguyenLieu = "";
            List<ChiTietNguyenLieu> list = CChiTietNguyenLieu_BUS.toList();
            foreach(var item in list)
            {
                if (item.maChiTietNguyenLieu == maChiTietNguyenLieu)
                {
                    tenNguyenLieu = item.NguyenLieu.tenNguyenLieu;
                    break;
                }
            }
            return tenNguyenLieu == "" ? "Null" : tenNguyenLieu.Trim();
        }

        public static List<NguyenLieu> findMa(string maNguyenLieu)
        {
            List<NguyenLieu> list = quanLyQuanCoffee.NguyenLieux
                .Where(x => x.maNguyenLieu.Contains(maNguyenLieu) == true && x.trangThai == 0).ToList();
            return list == null ? new List<NguyenLieu>() : list;
        }

        public static List<NguyenLieu> findTenLoai(string tenLoai)
        {
            tenLoai = CServices.formatChuoi(tenLoai);
            List<NguyenLieu> list = quanLyQuanCoffee.NguyenLieux
                .Where(x => x.LoaiNguyenLieu.tenLoaiNguyenLieu.Contains(tenLoai) && x.trangThai == 0).ToList();
            return list == null ? new List<NguyenLieu>() : list;
        }

        public static NguyenLieu findNguyenLieuByTen(string tenNguyenLieu)
        {
            tenNguyenLieu = CServices.formatChuoi(tenNguyenLieu);
            NguyenLieu nguyenLieu = quanLyQuanCoffee.NguyenLieux
                .Where(x => x.tenNguyenLieu.Contains(tenNguyenLieu) && x.trangThai == 0)
                .FirstOrDefault();
            return nguyenLieu == null ? new NguyenLieu() : nguyenLieu;
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
            if (temp == null || !CServices.kiemTraThongTin(nguyenLieu))
            {
                return false;
            }

            try
            {
                temp.maNguyenLieu = nguyenLieu.maNguyenLieu;
                temp.tenNguyenLieu = CServices.formatChuoi(nguyenLieu.tenNguyenLieu);
                temp.maLoaiNguyenLieu = nguyenLieu.maLoaiNguyenLieu;
                temp.trangThai = nguyenLieu.trangThai;

                quanLyQuanCoffee.SaveChanges();
            }
            catch (DbUpdateException)
            {
                MessageBox.Show("Lỗi! không thể sửa thông tin nguyên liệu");
                return false;
            }
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
            try
            {
                temp.trangThai = 1;
                quanLyQuanCoffee.SaveChanges();
            }
            catch (DbUpdateException)
            {
                MessageBox.Show("Lỗi! Không thể xóa nguyên liệu");
                return false;
            }
            
            return true;
        }


        //////////HAM DANH CHO PHIEU XUAT NGUYEN LIEU
        public static List<string> DSNguyenLieuTheoTen()
        {
            List<string> list = quanLyQuanCoffee.NguyenLieux.Select(x => x.tenNguyenLieu).ToList();
            return list == null ? new List<string>() : list;
        }
        public static List<ChiTietNguyenLieu> hienthiTheoNL(string maNL)
        {
            List<ChiTietNguyenLieu> list = quanLyQuanCoffee.ChiTietNguyenLieux.Where(x =>x.maNguyenLieu == maNL).ToList();
            return list == null ? new List<ChiTietNguyenLieu>() : list;
        }
        public static string layMaloaitheoSo(int dong)
        {

            string maLoai = quanLyQuanCoffee.NguyenLieux.ToList()[dong].maNguyenLieu;
            return maLoai;
        }
    }
}
