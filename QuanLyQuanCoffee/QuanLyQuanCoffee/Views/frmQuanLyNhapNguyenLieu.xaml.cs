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
    /// Interaction logic for frmQuanLyNhapNguyenLieu.xaml
    /// </summary>
    public partial class frmQuanLyNhapNguyenLieu : Page
    {
        private PhieuNhapNguyenLieu PhieuNhapNguyenLieuSelect = new PhieuNhapNguyenLieu();
        private NhanVien nhanVienSelect = new NhanVien();

        public frmQuanLyNhapNguyenLieu()
        {
            InitializeComponent();
            hienThiDSPhieuNhap(CPhieuNhapNguyenLieu_BUS.toList());
        }

        private void hienThiDSPhieuNhap(List<PhieuNhapNguyenLieu> list)
        {
            dgDSPhieuNhap.ItemsSource = list.Select(x => new
            {
                maPhieuNhap = x.maPhieuNhap,
                ngayNhap = x.ngayNhap.Value.ToString("dd/MM/yyyy"),
                tongThanhTien = String.Format("{0:#,###,0 VND;(#,###,0 VND);0 VND}", x.tongThanhTien)
            });
        }

        private void cmbTimKiem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbTimKiem.SelectedIndex == 1)
            {
                txtTimKiem.Visibility = Visibility.Hidden;
                dateNgayNhap.Visibility = Visibility.Visible;
            }
            else
            {
                txtTimKiem.Visibility = Visibility.Visible;
                dateNgayNhap.Visibility = Visibility.Hidden;
            }
        }

        private void dateNgayNhap_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab || e.Key == Key.Enter)
            {
                try
                {
                    DateTime ngayNhap = DateTime.Parse(dateNgayNhap.Text);
                    hienThiDSPhieuNhap(CPhieuNhapNguyenLieu_BUS.toListNgayNhap(ngayNhap));
                }
                catch (ArgumentNullException)
                {
                    MessageBox.Show("Dữ liệu ngày nhập không được để rỗng");
                }
                catch (FormatException)
                {
                    MessageBox.Show("Dữ liệu ngày nhập không hợp lệ 'MM/dd/yyyy'");
                }
            }
        }

        private void txtTimKiem_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtTimKiem.Text == "")
            {
                hienThiDSPhieuNhap(CPhieuNhapNguyenLieu_BUS.toList());
                return;
            }

            // nếu combox tìm kiếm là 0 tức là tìm theo mã phiếu nhập
            if (cmbTimKiem.SelectedIndex == 0)
            {
                hienThiDSPhieuNhap(CPhieuNhapNguyenLieu_BUS.toListMa(txtTimKiem.Text));
            }
            else
            {
                try
                {
                    double tongThanhTien = double.Parse(txtTimKiem.Text);
                    hienThiDSPhieuNhap(CPhieuNhapNguyenLieu_BUS.toListTongThanhTien(tongThanhTien));
                }
                catch (ArgumentNullException)
                {
                    MessageBox.Show("Dữ liệu không được để rỗng");
                }
                catch (FormatException)
                {
                    MessageBox.Show("Dữ liệu phải là số");
                }
                catch (OverflowException)
                {
                    MessageBox.Show("Dữ liệu có độ lớn vượt quá giới hạn cho phép");
                }
            }
        }

        private void btnXemThongTinChiTiet_Click(object sender, RoutedEventArgs e)
        {
            if (PhieuNhapNguyenLieuSelect != null)
            {
                new frmThongTinPhieuNhap(nhanVienSelect, PhieuNhapNguyenLieuSelect, 0).Show();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn phiếu nhập");
            }
        }

        private void dgDSPhieuNhap_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgDSPhieuNhap.SelectedItem == null)
            {
                return;
            }
            var maPhieuNhap = dgDSPhieuNhap.SelectedValue.ToString();
            PhieuNhapNguyenLieuSelect = CPhieuNhapNguyenLieu_BUS.find(maPhieuNhap);
        }

        private void btnRefesh_Click(object sender, RoutedEventArgs e)
        {
            hienThiDSPhieuNhap(CPhieuNhapNguyenLieu_BUS.toList());
        }

        //private void btnSua_Click(object sender, RoutedEventArgs e)
        //{
        //    if (PhieuNhapNguyenLieuSelect != null)
        //    {
        //        new frmThongTinPhieuNhap(nhanVienSelect, PhieuNhapNguyenLieuSelect, 2).Show();
        //    }
        //    else
        //    {
        //        MessageBox.Show("Vui lòng chọn phiếu nhập");
        //    }
        //}

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            if (PhieuNhapNguyenLieuSelect != null)
            {
                var result = MessageBox.Show("Do you want to delete changes?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    if (CPhieuNhapNguyenLieu_BUS.remove(PhieuNhapNguyenLieuSelect))
                    {
                        hienThiDSPhieuNhap(CPhieuNhapNguyenLieu_BUS.toList());
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn nhân viên");
            }
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            new frmThongTinPhieuNhap(nhanVienSelect).Show();
        }

        
    }
}
