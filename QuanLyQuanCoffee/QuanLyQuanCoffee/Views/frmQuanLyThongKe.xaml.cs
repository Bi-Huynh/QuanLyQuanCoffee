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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLyQuanCoffee.Views
{
    /// <summary>
    /// Interaction logic for frmQuanLyThongKe.xaml
    /// </summary>
    public partial class frmQuanLyThongKe : Page
    {
        private ThongKe thongKeSelect = new ThongKe();

        public frmQuanLyThongKe()
        {
            InitializeComponent();
            labThang.Content = DateTime.Now.Month;
            showBangXepHang();
            hienThi(CThongKe.toList());
        }

        private void hienThi(List<ThongKe> thongKes)
        {
            if (thongKes != null)
            {
                dgPhieuThongKe.ItemsSource = thongKes.Select(x => new
                {
                    maThongKe = x.maThongKe,
                    ngayThongKe = x.ngayLap,
                    tongThanhTien = x.tongThanhTien
                });
            }
        }

        private void btnThemPhieuTK_Click(object sender, RoutedEventArgs e)
        {
            frmQuanLyChiTietThongKe f = new frmQuanLyChiTietThongKe();
            f.Show();
        }

        private void btnXemChiTiet_Click(object sender, RoutedEventArgs e)
        {
            if (thongKeSelect != null && thongKeSelect.ChiTietThongKes != null)
            {
                frmQuanLyChiTietThongKe f = new frmQuanLyChiTietThongKe(thongKeSelect);
                f.Show();
            }
            else
            {
                MessageBox.Show("Không tìm thấy thống kê này trong csdl");
            }
        }

        private void dgPhieuThongKe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgPhieuThongKe.SelectedItem != null)
            {
                thongKeSelect = new ThongKe();
                thongKeSelect = CThongKe.find(dgPhieuThongKe.SelectedValue.ToString());
            }
        }

        private void showBangXepHang()
        {
            List<NhanVien> nhanViens = CNhanVien_BUS.toList();
            List<CBangXepHang> bangXepHangs = new List<CBangXepHang>();
            if (nhanViens.Count() > 0)
            {
                int stt = 0;
                foreach (var nhanVien in nhanViens)
                {
                    int soLuongHoaDon = CHoaDon_BUS.demSoLuongHoaDon(nhanVien.maNhanVien, DateTime.Now.Month);
                    int soLuongBan = CHoaDon_BUS.demSoLuongLyBanDuoc(nhanVien.maNhanVien, DateTime.Now.Month);
                    double tongThanhTien = CHoaDon_BUS.tongTienBan(nhanVien.maNhanVien, DateTime.Now.Month);
                    stt++;
                    bangXepHangs.Add(new CBangXepHang(
                        stt,
                        nhanVien.hoNhanVien + " " + nhanVien.tenNhanVien,
                        soLuongHoaDon,
                        soLuongBan,
                        tongThanhTien));
                }
                dgBangXepHang.ItemsSource = bangXepHangs.Select(x => new
                {
                    stt = x.Stt,
                    hoTen = x.HoTen,
                    soLuongHoaDon = x.SoLuongHoaDon,
                    soLuongBan = x.SoLuongBan,
                    tongTien = String.Format("{0:#,###,0 VND;(#,###,0 VND);0 VND}", x.TongTien)
                });
            }
        }

        private void btnRefect_Click(object sender, RoutedEventArgs e)
        {
            hienThi(CThongKe.toList());
        }
    }
}
