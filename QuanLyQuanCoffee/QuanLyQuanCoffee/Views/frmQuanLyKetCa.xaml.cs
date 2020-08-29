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
    /// Interaction logic for frmQuanLyKetCa.xaml
    /// </summary>
    public partial class frmQuanLyKetCa : Page
    {
        private List<KetCa> listDSKetCa;
        private int chosse = 0;
        private KetCa ketCaSelect;

        public frmQuanLyKetCa()
        {
            InitializeComponent();
            listDSKetCa = new List<KetCa>();
            listDSKetCa = CCa_BUS.toList();
            hienthiDSKetCa(listDSKetCa);
            //dateTimNgay.Visibility = Visibility.Hidden;
        }

        public void hienthiDSKetCa(List<KetCa> listKetCa)
        {
            if (listKetCa.Count() > 0)
            {
                try
                {
                    dgDSKetCa.ItemsSource = listKetCa.Select(x => new
                    {
                        maKetCa = x.maKetCa,
                        maNhanVien = x.maNhanVien,
                        tenNhanVien = x.NhanVien.hoNhanVien + " " + x.NhanVien.tenNhanVien,
                        gioBatDau = x.gioBatDau.Value.ToString("hh:MM:ss"),
                        gioKetThuc = x.gioKetThuc.Value.ToString("hh:MM:ss"),
                        ngayLap = x.ngayLap.Value.ToString("dd/MM/yyyy"),
                        soLuong = x.soLuong,
                        tienDauCa = String.Format("{0:#,###,0 VND;(#,###,0 VND);0 VND}", x.tienDauCa),
                        tongTienBan = String.Format("{0:#,###,0 VND;(#,###,0 VND);0 VND}", x.tongTienBan),
                        tongDoanhThu = String.Format("{0:#,###,0 VND;(#,###,0 VND);0 VND}", x.tongDoanhThu)
                    });
                }
                catch (FormatException)
                {
                    MessageBox.Show("Sai định dạng giờ");
                }
            }
            else
            {
                dgDSKetCa.ItemsSource = new List<KetCa>();
            }
        }

        private void cmbTimKiem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbTimKiem.SelectedIndex == 0)
            {
                txtTimKiem.Visibility = Visibility.Visible;
                dateTimNgay.Visibility = Visibility.Hidden;
                chosse = 0;
            }

            if (cmbTimKiem.SelectedIndex == 1)
            {
                txtTimKiem.Visibility = Visibility.Visible;
                dateTimNgay.Visibility = Visibility.Hidden;
                chosse = 1;
            }

            if (cmbTimKiem.SelectedIndex == 2)
            {
                txtTimKiem.Visibility = Visibility.Hidden;
                dateTimNgay.Visibility = Visibility.Visible;
                chosse = 2;
            }

            if (cmbTimKiem.SelectedIndex == 3)
            {
                txtTimKiem.Visibility = Visibility.Visible;
                dateTimNgay.Visibility = Visibility.Hidden;
                chosse = 3;
            }

            if (cmbTimKiem.SelectedIndex == 4)
            {
                txtTimKiem.Visibility = Visibility.Visible;
                dateTimNgay.Visibility = Visibility.Hidden;
                chosse = 4;
            }
        }

        private void txtTimKiem_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtTimKiem.Text == "")
            {
                hienthiDSKetCa(CCa_BUS.toList());
                return;
            }

            switch (chosse)
            {
                case 0:
                    hienthiDSKetCa(CCa_BUS.toListMaNV(txtTimKiem.Text));
                    break;
                case 1:
                    hienthiDSKetCa(CCa_BUS.toListTenNV(txtTimKiem.Text));
                    break;
                case 3:
                    foreach (char item in txtTimKiem.Text)
                    {
                        if (item < 48 && item > 57)
                        {
                            MessageBox.Show("Chỉ được nhập dữ liệu số");
                            return;
                        }
                    }
                    hienthiDSKetCa(CCa_BUS.toListTongTienBan(txtTimKiem.Text));
                    break;
                case 4:
                    foreach (char item in txtTimKiem.Text)
                    {
                        if (item < 48 && item > 57)
                        {
                            MessageBox.Show("Chỉ được nhập dữ liệu số");
                            return;
                        }
                    }
                    hienthiDSKetCa(CCa_BUS.toListTongDoanhThu(txtTimKiem.Text));
                    break;
                default:
                    break;
            }
        }

        private void dateTimNgay_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dateTimNgay.SelectedDate != null)
            {
                hienthiDSKetCa(CCa_BUS.toListNgayLap(dateTimNgay.SelectedDate.Value));
            }
            else
            {
                hienthiDSKetCa(CCa_BUS.toList());
            }
        }

        private void btnLoc_Click(object sender, RoutedEventArgs e)
        {
            if (dateTu.SelectedDate != null && dateDen.SelectedDate != null)
            {
                hienthiDSKetCa(CCa_BUS.toListNgayLap(dateTu.SelectedDate.Value, dateDen.SelectedDate.Value));
            }
            else
            {
                //MessageBox.Show("Vui lòng chọn ngày bắt đầu và ngày kết thúc");
                hienthiDSKetCa(CCa_BUS.toList());
            }
        }

        private void btnXemChiTietKetCa_Click(object sender, RoutedEventArgs e)
        {
            if (ketCaSelect != null)
            {
                frmXemHoaDonKetCa frmXemHoaDonKetCa = new frmXemHoaDonKetCa(ketCaSelect);
                frmXemHoaDonKetCa.Show();
            }
            else
            {
                MessageBox.Show("Không tìm được kết ca đã chọn");
            }
        }

        private void dgDSKetCa_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgDSKetCa.SelectedItem != null)
            {
                ketCaSelect = CCa_BUS.find(dgDSKetCa.SelectedValue.ToString());
            }
        }
    }
}
