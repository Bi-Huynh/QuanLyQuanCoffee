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
            hienthiHoaDon(CHoaDon_BUS.toList());
        }

        public void hienthiHoaDon(List<HoaDon> hoaDons)
        {
            if (hoaDons.Count() >= 0)
            {
                dgQlhoadon.ItemsSource = hoaDons.Select(x => new
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

                case 2:
                    hienthiHoaDon(CHoaDon_BUS.toListMaHoaDon(txtTK.Text));
                    break;

                case 3:
                    try
                    {
                        double tongThanhTien = double.Parse(txtTK.Text);
                        hienthiHoaDon(CHoaDon_BUS.toListTongThanhTien(tongThanhTien));
                    }
                    catch (ArgumentNullException)
                    {
                        MessageBox.Show("Số tiền nhập tìm kiếm rỗng");
                    }
                    catch (FormatException)
                    {
                        MessageBox.Show("Số tiền nhập tìm kiếm phải là số");
                    }
                    catch (OverflowException)
                    {
                        MessageBox.Show("Số tiền nhập tìm kiếm có dộ dài vượt mức");
                    }
                    break;

                default:
                    break;
            }
        }

        private void cmbTimKiem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbTimKiem.SelectedIndex == 0)
            {
                dateTimKiem.Visibility = Visibility.Hidden;
                txtTK.Visibility = Visibility.Visible;
                chosse = 0;
            }

            if (cmbTimKiem.SelectedIndex == 1)
            {
                dateTimKiem.Visibility = Visibility.Visible;
                txtTK.Visibility = Visibility.Hidden;
                chosse = 1;
            }

            if (cmbTimKiem.SelectedIndex == 2)
            {
                dateTimKiem.Visibility = Visibility.Hidden;
                txtTK.Visibility = Visibility.Visible;
                chosse = 2;
            }

            if (cmbTimKiem.SelectedIndex == 3)
            {
                dateTimKiem.Visibility = Visibility.Hidden;
                txtTK.Visibility = Visibility.Visible;
                chosse = 3;
            }
        }

        private void dateTimKiem_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dateTimKiem.SelectedDate == null)
            {
                hienthiHoaDon(CHoaDon_BUS.toList());
            }
            else
            {
                hienthiHoaDon(CHoaDon_BUS.toListNgayLap(dateTimKiem.SelectedDate.Value));
            }
        }
    }
}
