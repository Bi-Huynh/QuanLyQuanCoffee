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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLyQuanCoffee.Views
{
    /// <summary>
    /// Interaction logic for frmQuanLyHoaDon.xaml
    /// </summary>
    public partial class frmQuanLyHoaDon : Page
    {
        private HoaDon hoaDonSelected;
        private int chosse = 0;

        public frmQuanLyHoaDon()
        {
            InitializeComponent();
            hienthiHoaDon(CHoaDon_BUS.toList()) ;
        }

        public void hienthiHoaDon(List<HoaDon> hoaDons)
        {
            if (hoaDons.Count()>=0)
            {
                dgQlhoadon.ItemsSource = CHoaDon_BUS.toList().Select(x => new
                {
                    maHoaDon = x.maHoaDon,
                    maNhanVien = x.maNhanVien,
                    tenNhanVien = x.NhanVien.hoNhanVien + " " + x.NhanVien.tenNhanVien,
                    ngayLap = x.ngayLap.ToString("dd/MM/yyyy"),
                    tienKhachDua = String.Format("{0:#,###,0 VND;(#,###,0 VND);0 VND}", x.tienKhachDua),
                    tienThua = String.Format("{0:#,###,0 VND;(#,###,0 VND);0 VND}", x.tienThua),
                    tongThanhTien = String.Format("{0:#,###,0 VND;(#,###,0 VND);0 VND}", x.tongThanhTien)
                });
            }
        }

        private void gdQuanLyChitietHoaDon_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (hoaDonSelected != null)
                {
                    frmQuanLyChiTietHoaDon f = new frmQuanLyChiTietHoaDon(hoaDonSelected);
                    f.Show();
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn hóa đơn");
                }
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Bạn Chưa chọn hóa đơn");
            }
        }

        private void dgQlhoadon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgQlhoadon.SelectedItem != null)
            {
                hoaDonSelected = CHoaDon_BUS.find(dgQlhoadon.SelectedValue.ToString());
            }
        }

        private void txtTK_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtTK.Text == "")
            {
                hienthiHoaDon(CHoaDon_BUS.toList());
                return;
            }

            switch (chosse)
            {
                case 0:
                    hienthiHoaDon(CHoaDon_BUS.toListMaNhanVien(txtTK.Text));
                    break;

                case 1:
                    hienthiHoaDon(CHoaDon_BUS.toListMaHoaDon(txtTK.Text));
                    break;

                case 2:
                    hienthiHoaDon(CHoaDon_BUS.toListNgayLap(dateTimKiem.SelectedDate.Value));
                    break;

                case 3:
                    hienthiHoaDon(CHoaDon_BUS.toListTongThanhTien(txtTK.Text));
                    break;

                default:
                    break;
            }
        }

        private void cmbTimKiem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbTimKiem.SelectedIndex == 0)
            {
                dateTimKiem.IsEnabled = false;
                txtTK.IsEnabled = true;
                chosse = 0;
            }

            if (cmbTimKiem.SelectedIndex == 1)
            {
                dateTimKiem.IsEnabled = true;
                txtTK.IsEnabled = false;
                chosse = 1;
            }

            if (cmbTimKiem.SelectedIndex == 2)
            {
                dateTimKiem.IsEnabled = false;
                txtTK.IsEnabled = true;
                chosse = 2;
            }

            if (cmbTimKiem.SelectedIndex == 3)
            {
                dateTimKiem.IsEnabled = false;
                txtTK.IsEnabled = true;
                chosse = 3;
            }
        }
    }
}
