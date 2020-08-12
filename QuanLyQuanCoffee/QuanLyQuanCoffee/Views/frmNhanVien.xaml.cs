using QuanLyQuanCoffee.BUS;
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
    /// Interaction logic for frmNhanVien.xaml
    /// </summary>
    public partial class frmNhanVien : Window
    {
        private NhanVien nhanVienSelect;
        private TaiKhoan taiKhoanSelect;

        public frmNhanVien(NhanVien nhanVien = null, TaiKhoan taiKhoan = null)
        {
            InitializeComponent();

            nhanVienSelect = nhanVien;
            taiKhoanSelect = taiKhoan;

            if (nhanVien == null)
            {
                nhanVienSelect = new NhanVien();
            }

            if (taiKhoanSelect == null)
            {
                taiKhoanSelect = new TaiKhoan();
            }

            if (taiKhoanSelect.trangThai == 3)
            {
                MessageBox.Show("Vui lòng đổi mật khẩu");
                new frmDoiTaiKhoan(taiKhoanSelect).Show();
            }
        }

        private void order_Click(object sender, RoutedEventArgs e)
        {
            if (CCa_BUS.CaLamViec != null)
            {
                MainNhanVien.Content = new frmOrder(nhanVienSelect);
            }
            else
            {
                MessageBox.Show("Vui lòng tạo ca làm việc");
            }
        }

        private void gd_QuanLyHoaDon_Click(object sender, RoutedEventArgs e)
        {
            MainNhanVien.Content = new XemHoaDonTrongNgay();
        }

        private void ketCa_Click(object sender, RoutedEventArgs e)
        {
            new frmKetCa(nhanVienSelect).Show();
        }

        private void dangXuat_Click(object sender, RoutedEventArgs e)
        {
            if (CCa_BUS.CaLamViec == null)
                // chưa tạo ca thì có thể đăng xuất
            {
                new frmDangNhap().Show();
                this.Close();
            }
            else
            {
                if (CCa_BUS.isDaKetCa)
                    // đã kết ca rồi thì mới có thể đăng xuất
                {
                    frmDangNhap f = new frmDangNhap();
                    f.Show();
                    CCa_BUS.isDaKetCa = false;
                    CCa_BUS.CaLamViec = null;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Phải kết ca mới có thể đăng xuất");
                }
            }
        }

        private void doiTaiKhoan_Click(object sender, RoutedEventArgs e)
        {
            new frmDoiTaiKhoan(taiKhoanSelect).Show();
        }

        private void taoCa_Click(object sender, RoutedEventArgs e)
        {
            new frmTaoCa(nhanVienSelect).Show();
        }
    }
}
