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
    class CTaiKhoan_BUS
    {
        private static QuanLyQuanCoffeeEntities1 quanLyQuanCoffee = new QuanLyQuanCoffeeEntities1();

        public static List<TaiKhoan> toList()
        {
            List<TaiKhoan> list = quanLyQuanCoffee.TaiKhoans.Where(x => x.trangThai == 0).ToList();
            return list == null ? new List<TaiKhoan>() : list;
        }
        public static TaiKhoan find(string maTaiKhoan)
        {
            TaiKhoan taiKhoan = quanLyQuanCoffee.TaiKhoans.Find(maTaiKhoan);
            return taiKhoan;
        }
        public static Boolean findTrangThai(string maTaiKhoan)
        {
            TaiKhoan taiKhoan = quanLyQuanCoffee.TaiKhoans.Find(maTaiKhoan);
            if(taiKhoan.trangThai==0)
            {
                return false;
            }    
            return true;
        }
        public static string KTtaiKhoanDaXoa(TaiKhoan taiKhoan)
        {
            string a = MessageBox.Show("Nhân viên này đã có tài khoản nhưng đã được xóa, bạn có muốn phục hồi không", "Thông báo", MessageBoxButton.YesNo).ToString();
            return a;
        }
        public static List<string> toListByMaLoaiTK()
        {
            List<string> list = quanLyQuanCoffee.LoaiTaiKhoans.Select(x => x.maLoaiTaiKhoan).ToList();
            return list == null ? new List<string>() : list;
        }
        public static List<string> toListByMaLoaiNV()
        {
            List<string> list = quanLyQuanCoffee.NhanViens.Select(x => x.maNhanVien).ToList();
            return list == null ? new List<string>() : list;
        }

        public static bool add(TaiKhoan taiKhoan)
        {
            if (CServices.kiemTraThongTin(taiKhoan))
            {
                try
                {
                    //taiKhoan.taiKhoan1 = CServices.formatTK_MT(taiKhoan.taiKhoan1);
                    //taiKhoan.matKhau = CServices.formatTK_MT(taiKhoan.matKhau);

                    quanLyQuanCoffee.TaiKhoans.Add(taiKhoan);
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
            return true;
        }
        public static bool remove(TaiKhoan taiKhoan)
        {
            try
            {
                TaiKhoan temp = find(taiKhoan.maNhanVien);
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
        public static bool edit(TaiKhoan taiKhoan)
        {
            TaiKhoan temp = find(taiKhoan.maNhanVien);
            if (temp != null)
            {
                try
                {
                    temp.taiKhoan1 = taiKhoan.taiKhoan1;
                    temp.matKhau = taiKhoan.matKhau;
                    temp.maLoaiTaiKhoan = taiKhoan.maLoaiTaiKhoan;
                    temp.trangThai = taiKhoan.trangThai;
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
            return true;
        }
        public static bool hoiPhucTK(TaiKhoan taiKhoan)
        {
            TaiKhoan temp = find(taiKhoan.maNhanVien);
            if (temp != null)
            {
                try
                {
                    temp.taiKhoan1 = taiKhoan.taiKhoan1;
                    temp.matKhau = taiKhoan.matKhau;
                    temp.LoaiTaiKhoan = taiKhoan.LoaiTaiKhoan;
                    temp.trangThai = 0;
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
            return true;
        }
        public static bool KTRong(TaiKhoan taiKhoan)
        {


            if (taiKhoan.maNhanVien.Length > 10)
            {
                return false;
            }
            if (taiKhoan.taiKhoan1 == "" || taiKhoan.matKhau == "" || taiKhoan.maLoaiTaiKhoan == "")
            {
                return false;
            }
            if (taiKhoan.maLoaiTaiKhoan == null)
            {
                return false;
            }

            return true;
        }
    }
}
