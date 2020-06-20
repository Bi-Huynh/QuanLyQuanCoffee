using QuanLyQuanCoffee.Services;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLyQuanCoffee.BUS
{
    class CLoaiNhanVien_BUS
    {
        private static QuanLyQuanCoffeeEntities1 quanLyQuanCoffee = new QuanLyQuanCoffeeEntities1();

        public static LoaiNhanVien find(string maLoaiNhanVien)
        {
            return quanLyQuanCoffee.LoaiNhanViens.Find(maLoaiNhanVien);
        }

        public static LoaiNhanVien find(LoaiNhanVien loaiNhanVien)
        {
            return find(loaiNhanVien.maLoaiNhanvien);
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

        public static bool kiemTraThongTin(LoaiNhanVien loaiNhanVien)
        {
            loaiNhanVien.tenLoai = CServices.formatChuoi(loaiNhanVien.tenLoai);
            return true;
        }

        public static bool add(LoaiNhanVien loaiNhanVien)
        {
            if (CServices.kiemTraThongTin(loaiNhanVien))
            {
                loaiNhanVien.tenLoai = CServices.formatChuoi(loaiNhanVien.tenLoai);
                try
                {
                    quanLyQuanCoffee.LoaiNhanViens.Add(loaiNhanVien);
                    quanLyQuanCoffee.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    MessageBox.Show("Lỗi! Không thể thêm dữ liệu");
                    return false;
                }
            }
            return true;
        }

        public static bool edit(LoaiNhanVien loaiNhanVien)
        {
            LoaiNhanVien temp = find(loaiNhanVien.maLoaiNhanvien);
            if (temp == null || !CServices.kiemTraThongTin(loaiNhanVien))
            {
                return false;
            }
            try
            {
                temp.maLoaiNhanvien = loaiNhanVien.maLoaiNhanvien;
                temp.tenLoai = CServices.formatChuoi(loaiNhanVien.tenLoai);
                temp.luongCoBan = loaiNhanVien.luongCoBan;
                quanLyQuanCoffee.SaveChanges();
            }
            catch (DbUpdateException)
            {
                MessageBox.Show("Lỗi! không thể sửa thông tin loại nhân viên");
                return false;
            }
            return true;
        }

        public static bool remove(LoaiNhanVien loaiNhanVien)
        {
            LoaiNhanVien temp = find(loaiNhanVien.maLoaiNhanvien);

            if (temp == null)
            {
                MessageBox.Show("Không tìm thấy loại nhân viên để xóa");
                return false;
            }

            if (temp.NhanViens.Count > 0)
            {
                MessageBox.Show("Có nhân viên đang thuộc loại này, Không thể xóa");
                return false;
            }

            try
            {
                quanLyQuanCoffee.LoaiNhanViens.Remove(temp);
                quanLyQuanCoffee.SaveChanges();
            }
            catch (DbUpdateException)
            {
                MessageBox.Show("Lỗi! Không thể xóa loại nhân viên");
                return false;
            }
            return true;
        }

    }
}
