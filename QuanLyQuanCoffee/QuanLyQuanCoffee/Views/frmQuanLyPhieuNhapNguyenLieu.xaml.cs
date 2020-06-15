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
    /// Interaction logic for frmQuanLyPhieuNhapNguyenLieu.xaml
    /// </summary>
    public partial class frmQuanLyPhieuNhapNguyenLieu : Page
    {
        private PhieuNhapNguyenLieu phieuNhapNguyenLieuSelect;

        public frmQuanLyPhieuNhapNguyenLieu()
        {
            InitializeComponent();
            hienThiDSPhieuNhap(CPhieuNhapNguyenLieu_BUS.toList());
        }

        private void hienThiDSPhieuNhap(List<PhieuNhapNguyenLieu> list)
        {
            dgDSPhieuNhap.ItemsSource = list.Select(x => new
            {
                maPhieuNhap = x.maPhieuNhap,
                hoNhanVien = x.NhanVien.hoNhanVien,
                tenNhanVien = x.NhanVien.tenNhanVien,
                ngayNhap = x.ngayNhap.ToString("dd/MM/yyyy"),
                tongThanhTien = x.tongThanhTien
            });
        }

        private void btnXemThongTinChiTiet_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dgDSPhieuNhap_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgDSPhieuNhap.SelectedItem != null)
            {
                string maPhieuNhap = dgDSPhieuNhap.SelectedValue.ToString();
                phieuNhapNguyenLieuSelect = CPhieuNhapNguyenLieu_BUS.find(maPhieuNhap);
            }
        }

        private void btnRefesh_Click(object sender, RoutedEventArgs e)
        {
            hienThiDSPhieuNhap(CPhieuNhapNguyenLieu_BUS.toList());
        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            if (phieuNhapNguyenLieuSelect != null)
            {
                if (CPhieuNhapNguyenLieu_BUS.remove(phieuNhapNguyenLieuSelect))
                {
                    MessageBox.Show("Xóa thành công");
                    hienThiDSPhieuNhap(CPhieuNhapNguyenLieu_BUS.toList());
                }
                else
                {
                    MessageBox.Show("Xóa không thành công");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn phiếu nhập nguyên liệu");
            }
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {

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
                List<PhieuNhapNguyenLieu> list = new List<PhieuNhapNguyenLieu>();
                list.Add(CPhieuNhapNguyenLieu_BUS.find(txtTimKiem.Text));
                hienThiDSPhieuNhap(list);
            }
            //nếu combox tìm kiếm là 1 tức là tìm theo tên nhân viên
            else if (cmbTimKiem.SelectedIndex == 1)
            {
                hienThiDSPhieuNhap(CPhieuNhapNguyenLieu_BUS.findTenNhanVien(txtTimKiem.Text));
            }
            else
            {

                hienThiDSPhieuNhap(CPhieuNhapNguyenLieu_BUS.findNgayNhap(dateNgayNhap.SelectedDate.Value));
            }
        }

        private void cmbTimKiem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbTimKiem.SelectedIndex == 2)
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
    }
}
