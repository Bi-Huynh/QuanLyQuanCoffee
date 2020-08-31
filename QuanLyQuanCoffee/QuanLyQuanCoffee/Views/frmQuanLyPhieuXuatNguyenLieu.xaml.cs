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
    /// Interaction logic for frmQuanLyPhieuXuatNguyenLieu.xaml
    /// </summary>
    public partial class frmQuanLyPhieuXuatNguyenLieu : Page
    {
        private PhieuXuatNguyenLieu phieuXuatnguyenlieuSelect;


        public frmQuanLyPhieuXuatNguyenLieu()
        {
            InitializeComponent();

            hienThiPhieuXuat(CPhieuXuatNguyenLieu_BUS.toList());
        }
        public void hienThiPhieuXuat()
        {
            List<PhieuXuatNguyenLieu> list = CPhieuXuatNguyenLieu_BUS.toList();
            dgDSPhieuXuat.ItemsSource = list.Select(x => new
            {
                maPhieuXuat = x.maPhieuXuat,
                ngayXuat = x.ngayXuat.Value.ToString("dd/MM/yyyy"),
                tongThanhTien = x.tongThanhTien
            });
        }

        public void hienThiPhieuXuat(List<PhieuXuatNguyenLieu> list)
        {
            dgDSPhieuXuat.ItemsSource = list.Select(x => new
            {
                maPhieuXuat = x.maPhieuXuat,
                ngayXuat = x.ngayXuat.Value.ToString("dd/MM/yyyy"),
                tongThanhTien = x.tongThanhTien
            });
        }

        private void txtTimKiem_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtTimKiem.Text == "")
            {
                hienThiPhieuXuat(CPhieuXuatNguyenLieu_BUS.toList());
                return;
            }

            // nếu combox tìm kiếm là 0 tức là tìm theo mã phiếu nhập
            if (cmbTimKiem.SelectedIndex == 0)
            {
                hienThiPhieuXuat(CPhieuXuatNguyenLieu_BUS.toListMa(txtTimKiem.Text));
            }
            else
            {
                try
                {
                    double tongThanhTien = double.Parse(txtTimKiem.Text);
                    hienThiPhieuXuat(CPhieuXuatNguyenLieu_BUS.toListTongThanhTien(tongThanhTien));
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
            if (phieuXuatnguyenlieuSelect != null)
            {
                frmThongTinChiTietPhieuXuatNL f = new frmThongTinChiTietPhieuXuatNL(phieuXuatnguyenlieuSelect);
                f.Show();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn phiếu xuất");
            }
        }

        private void dgDSPhieuXuat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgDSPhieuXuat.SelectedItem != null)
            {
                string maPhieuXuat = dgDSPhieuXuat.SelectedValue.ToString();
                if (maPhieuXuat == null || maPhieuXuat == "")
                {
                    MessageBox.Show("Không lấy được phiếu xuất đã chọn");
                    return;
                }
                phieuXuatnguyenlieuSelect = CPhieuXuatNguyenLieu_BUS.find(dgDSPhieuXuat.SelectedValue.ToString());
            }
        }

        private void btnRefesh_Click(object sender, RoutedEventArgs e)
        {
            hienThiPhieuXuat();
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            if (phieuXuatnguyenlieuSelect != null)
            {
                var result = MessageBox.Show("Do you want to delete changes?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    if (CPhieuXuatNguyenLieu_BUS.remove(phieuXuatnguyenlieuSelect))
                    {
                        hienThiPhieuXuat(CPhieuXuatNguyenLieu_BUS.toList());
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn phiếu xuất");
            }
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            frmXuatNguyenLieu f = new frmXuatNguyenLieu();
            f.Show();
        }

        private void cmbTimKiem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbTimKiem.SelectedIndex == 0)
            {
                txtTimKiem.Visibility = Visibility.Visible;
                dateTimKiem.Visibility = Visibility.Hidden;
            }
            else if (cmbTimKiem.SelectedIndex == 1)
            {
                txtTimKiem.Visibility = Visibility.Hidden;
                dateTimKiem.Visibility = Visibility.Visible;
            }
            else
            {
                txtTimKiem.Visibility = Visibility.Visible;
                dateTimKiem.Visibility = Visibility.Hidden;
            }
        }

        private void dateTimKiem_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dateTimKiem.SelectedDate != null)
            {
                hienThiPhieuXuat(CPhieuXuatNguyenLieu_BUS.toList(dateTimKiem.SelectedDate.Value));
            }
            if (dateTimKiem.Text == "" || dateTimKiem.Text == null)
            {
                hienThiPhieuXuat(CPhieuXuatNguyenLieu_BUS.toList());
            }
        }
    }
}
