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
    class CLoaiTaiKhoan_BUS
    {
        private static QuanLyQuanCoffeeEntities1 quanLyQuanCoffee = LoadDatabase.Instance();

        public static List<LoaiTaiKhoan> toList()
        {
            List<LoaiTaiKhoan> list = quanLyQuanCoffee.LoaiTaiKhoans
                .Where(x => x.trangThai == 0 && x.maLoaiTaiKhoan != "00001").ToList();
            return list == null ? new List<LoaiTaiKhoan>() : list;
        }

        public static List<string> toListTenLoai()
        {
            List<string> list = quanLyQuanCoffee.LoaiTaiKhoans.Select(x => x.tenLoaiTaiKhoan).ToList();
            return list == null ? new List<string>() : list;
        }

        public static List<LoaiTaiKhoan> DSLoaiTK()
        {
            List<LoaiTaiKhoan> list = quanLyQuanCoffee.LoaiTaiKhoans.ToList();
            return list == null ? new List<LoaiTaiKhoan>() : list;
        }

        public static LoaiTaiKhoan find(string maLoaiTaiKhoan)
        {
            LoaiTaiKhoan loaiTaiKhoan = quanLyQuanCoffee.LoaiTaiKhoans.Find(maLoaiTaiKhoan);
            return loaiTaiKhoan;
        }

        public static LoaiTaiKhoan findTen(string tenLoaiTaiKhoan)
        {
            LoaiTaiKhoan loaiTaiKhoan = quanLyQuanCoffee.LoaiTaiKhoans.Where(x => x.tenLoaiTaiKhoan == tenLoaiTaiKhoan).FirstOrDefault();
            return loaiTaiKhoan;
        }

        public static bool add(LoaiTaiKhoan loaiTaiKhoan)
        {
            try
            {

                quanLyQuanCoffee.LoaiTaiKhoans.Add(loaiTaiKhoan);
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
        public static bool remove(LoaiTaiKhoan loaiTaiKhoan)
        {
            try
            {
                LoaiTaiKhoan temp = find(loaiTaiKhoan.maLoaiTaiKhoan);
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
        public static bool edit(LoaiTaiKhoan loaiTaiKhoan)
        {
            LoaiTaiKhoan temp = find(loaiTaiKhoan.maLoaiTaiKhoan);
            if (temp != null)
            {
                try
                {
                    temp.tenLoaiTaiKhoan = loaiTaiKhoan.tenLoaiTaiKhoan;
                    temp.trangThai = loaiTaiKhoan.trangThai;
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
        public static bool KTRong(LoaiTaiKhoan loaiTaiKhoan)
        {
            if (loaiTaiKhoan.tenLoaiTaiKhoan == "")
            {
                return false;
            }
            return true;
        }
    }
}
