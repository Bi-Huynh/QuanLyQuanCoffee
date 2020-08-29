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
        private List<HoaDon> hoaDons;

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

            taoCa();
            hoaDons = new List<HoaDon>();
        }

        private void taoCa()
        {
            CCa_BUS.CaLamViec = new DTO.CCa_DTO(nhanVienSelect.maNhanVien, DateTime.Now);
        }

        private void order_Click(object sender, RoutedEventArgs e)
        {
            if (CCa_BUS.isDaKetCa)
            {
                MessageBox.Show("Bạn đã kết ca. Vui lòng đăng xuất");
                return;
            }
            if (CCa_BUS.CaLamViec != null)
            {
                MainNhanVien.Content = new frmOrder(nhanVienSelect);
            }
        }

        private void gd_QuanLyHoaDon_Click(object sender, RoutedEventArgs e)
        {
            MainNhanVien.Content = new XemHoaDonTrongNgay();
        }

        private void ketCa_Click(object sender, RoutedEventArgs e)
        {
            MainNhanVien.Content = null;
            if (CCa_BUS.isDaKetCa == false && CCa_BUS.CaLamViec != null)
            {
                new frmKetCa(nhanVienSelect).Show();
            }

            if (CCa_BUS.isDaKetCa == false && CCa_BUS.CaLamViec == null)
            {
                MessageBox.Show("Bạn chưa tạo ca làm việc, không thể kết ca");
            }

            if (CCa_BUS.isDaKetCa && CCa_BUS.CaLamViec != null)
            {
                MessageBox.Show("Bạn chỉ được kết ca 1 lần, không thể kết ca thêm lần nữa");
            }
        }

        private void dangXuat_Click(object sender, RoutedEventArgs e)
        {
            if (CCa_BUS.CaLamViec == null)
            // chưa tạo ca thì có thể đăng xuất
            {
                CCa_BUS.isDaKetCa = false;
                CCa_BUS.CaLamViec = null;
                new frmDangNhap().Show();
                this.Close();
            }
            else
            {
                hoaDons = CHoaDon_BUS.DsHoaDon(CCa_BUS.CaLamViec.GioBatDau, DateTime.Now);
                if (hoaDons.Count > 0)
                {
                    if (CCa_BUS.isDaKetCa)
                    // đã kết ca rồi thì mới có thể đăng xuất
                    {
                        CCa_BUS.isDaKetCa = false;
                        CCa_BUS.CaLamViec = null;
                        frmDangNhap f = new frmDangNhap();
                        f.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Phải kết ca mới có thể đăng xuất");
                        return;
                    }
                }
                else
                {
                    CCa_BUS.isDaKetCa = false;
                    CCa_BUS.CaLamViec = null;
                    new frmDangNhap().Show();
                    this.Close();
                }
            }
        }

        private void doiTaiKhoan_Click(object sender, RoutedEventArgs e)
        {
            new frmDoiTaiKhoan(taiKhoanSelect).Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (CCa_BUS.CaLamViec != null && CCa_BUS.isDaKetCa == false && hoaDons.Count > 0)
            {
                e.Cancel = true;
                // không thể tắt ứng dụng khi chưa kết ca
                MessageBox.Show("Không thể tắt ứng dụng khi chưa kết ca");
            }
            else
            // Ngược lại là ca làm việc chưa được tạo hoặc đã được kết ca thì có thể đăng xuất
            {
                e.Cancel = false;
            }
        }
    }
}
