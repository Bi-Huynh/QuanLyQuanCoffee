using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using QuanLyQuanCoffee.Services;

namespace QuanLyQuanCoffee.BUS
{

    class CLoaiSanPham_BUS
    {
        private static QuanLyQuanCoffeeEntities1 quanLyQuanCoffee = new QuanLyQuanCoffeeEntities1();
        public static List<LoaiSanPham> toList()
        {
            List<LoaiSanPham> list = quanLyQuanCoffee.LoaiSanPhams.Where(x => x.trangThai == 0).ToList();
            return list == null ? new List<LoaiSanPham>() : list;
        }
        public static List<LoaiSanPham> DSLoaiSP()
        {
            List<LoaiSanPham> list = quanLyQuanCoffee.LoaiSanPhams.ToList();
            return list == null ? new List<LoaiSanPham>() : list;
        }
        public static List<string> DSLoaiSPtheoTen()
        {
            List<string> list = quanLyQuanCoffee.LoaiSanPhams.Select(x => x.tenLoai).ToList();
            return list == null ? new List<string>() : list;
        }
        public static string layMaloaitheoSo(int dong)
        {
           
            string maLoai = quanLyQuanCoffee.LoaiSanPhams.ToList()[dong].maLoaiSanPham;
            return maLoai;
        }

        public static LoaiSanPham find(string maLoaiSanPham)
        {
            LoaiSanPham LoaisanPham = quanLyQuanCoffee.LoaiSanPhams.Find(maLoaiSanPham);
            return LoaisanPham;
        }
        public static bool add(LoaiSanPham loaisanPham)
        {

            try
            {
                loaisanPham.tenLoai = CServices.formatChuoi(loaisanPham.tenLoai);

                quanLyQuanCoffee.LoaiSanPhams.Add(loaisanPham);
                quanLyQuanCoffee.SaveChanges();
            }
            catch (DbUpdateException)
            {
                MessageBox.Show("Lỗi! không thể lưu dữ liệu");
                return false;
            }
            catch (DbEntityValidationException)
            {
                MessageBox.Show("Lỗi! kiểu dữ liệu");
                return false;
            }

            return true;
        }
        public static bool remove(LoaiSanPham loaisanPham)
        {
            try
            {
                LoaiSanPham temp = find(loaisanPham.maLoaiSanPham);
                if (temp != null)
                {
                    temp.trangThai = 1;
                    quanLyQuanCoffee.SaveChanges();
                }
            }
            catch (DbUpdateException)
            {
                MessageBox.Show("Lỗi! không thể lưu dữ liệu");
                return false;
            }
            catch (DbEntityValidationException)
            {
                MessageBox.Show("Lỗi! kiểu dữ liệu");
                return false;
            }
            return true;
        }
        public static bool edit(LoaiSanPham loaisanPham)
        {
            LoaiSanPham temp = find(loaisanPham.maLoaiSanPham);
            if (temp != null)
            {
                try
                {
                    temp.tenLoai = loaisanPham.tenLoai;
                    temp.trangThai = loaisanPham.trangThai;
                    quanLyQuanCoffee.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    MessageBox.Show("Lỗi! không thể lưu dữ liệu");
                    return false;
                }
                catch (DbEntityValidationException)
                {
                    MessageBox.Show("Lỗi! kiểu dữ liệu");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Không tìm thấy mã loại này");
            }
            return true;
        }
        public static bool KTRong(LoaiSanPham loaisanPham)
        {
            if (loaisanPham.tenLoai == "")
            {
                return false;
            }
            return true;
        }
    }
}
