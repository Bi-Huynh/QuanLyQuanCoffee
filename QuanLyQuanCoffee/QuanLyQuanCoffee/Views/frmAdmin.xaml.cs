using QuanLyQuanCoffee.BUS;
using QuanLyQuanCoffee.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QuanLyQuanCoffee.Views
{
    /// <summary>
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class frmAdmin : Window
    {
        private TaiKhoan taiKhoanSelect;

        public frmAdmin(TaiKhoan taiKhoan = null)
        {
            InitializeComponent();
            if (taiKhoan != null)
            {
                taiKhoanSelect = taiKhoan;
            }
        }

        private void gd_QuanLyNhanVien_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new frmQuanLyNhanVien();
        }

        private void gd_QuanLySanPham_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new frmQuanLySanPham();
        }

        private void gd_QuanLyNguyenLieu_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new frmQuanLyNguyenLieu();
        }

        private void gd_QuanLyNhapNguyenLieu_Click(object sender, RoutedEventArgs e)
        {
            NhanVien nhanVien = CNhanVien_BUS.find(taiKhoanSelect.maNhanVien);
            Main.Content = new frmQuanLyNhapNguyenLieu(nhanVien);
        }

        private void gd_quanlyTaiKhoan_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new frmQuanLyTaiKhoan();
        }

        private void gd_quanlyLoaiTaiKhoan_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new frmQuanLyLoaiTaiKhoan();
        }

        public void dangXuat_Click(object sender, RoutedEventArgs e)
        {
            frmDangNhap f = new frmDangNhap();
            f.Show();
            taiKhoan = null;
            this.Close();
        }

        private void gd_QuanLyLoaiSanPham_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new frmQuanLyloaiSanPham();
        }

        private void gd_QuanLyHoaDon_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new frmQuanLyHoaDon();
        }

        private void gd_QuanLyXuatNguyenLieu_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new frmQuanLyPhieuXuatNguyenLieu();
        }

        private void quanLyKetCa_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new frmQuanLyKetCa();
        }

        private void gd_QuanLyThongKe_Click(object sender, RoutedEventArgs e)
        {
           Main.Content=new frmQuanLyThongKe();
        }

        private void gd_doiMatKhau_Click(object sender, RoutedEventArgs e)
        {
            new frmDoiTaiKhoan(taiKhoanSelect).Show();
        }
    }
}
