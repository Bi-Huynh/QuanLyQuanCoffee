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
        private QuanLyQuanCoffeeEntities1 quanLyQuanCoffee = new QuanLyQuanCoffeeEntities1();
        private TaiKhoan taiKhoan1;
        private LoaiTaiKhoan ltk;
        NhanVien nhanVien;

        public frmAdmin(TaiKhoan taiKhoan)
        {
            InitializeComponent();
            taiKhoan1 = taiKhoan;
            ltk = quanLyQuanCoffee.LoaiTaiKhoans.Find(taiKhoan.maLoaiTaiKhoan);
            kiemTraQuyen(taiKhoan);
            nhanVien = quanLyQuanCoffee.NhanViens.Find(taiKhoan1.maNhanVien);
            if (nhanVien == null)
            {
                nhanVien = new NhanVien();
            }
        }

        public void kiemTraQuyen(TaiKhoan taiKhoan1)
        {
            if (taiKhoan1.maLoaiTaiKhoan != "00001")
            {
                nhanSu.IsEnabled = false;
                nguyenLieu.IsEnabled = false;
                sanPham.IsEnabled = false;
                hoaDon.IsEnabled = false;
                taiKhoan.IsEnabled = false;
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

        private void order_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new frmOrder(nhanVien);
        }

        private void dangXuat_Click(object sender, RoutedEventArgs e)
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

        }
    }
}
