using QuanLyQuanCoffee.BUS;
using QuanLyQuanCoffee.Services;
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
    /// Interaction logic for frmThongTinPhieuNhap.xaml
    /// </summary>
    public partial class frmThongTinPhieuNhap : Window
    {
        private NhanVien nhanVienSelect;
        private PhieuNhapNguyenLieu phieuNhapNguyenLieuSelect;

        public frmThongTinPhieuNhap(NhanVien nhanVien = null, PhieuNhapNguyenLieu phieuNhapNguyenLieu = null, int flag = 1)
        {
            InitializeComponent();
            nhanVienSelect = nhanVien;
            phieuNhapNguyenLieuSelect = phieuNhapNguyenLieu;
            if (nhanVienSelect == null)
            {
                nhanVienSelect = new NhanVien();
            }
            if (phieuNhapNguyenLieuSelect == null)
            {
                phieuNhapNguyenLieuSelect = new PhieuNhapNguyenLieu();
            }

            if (flag == 1)
            {
                txtMaPhieuNhap.Text = CServices.taoMa<PhieuNhapNguyenLieu>(CPhieuNhapNguyenLieu_BUS.toListAll());
                btnSua.IsEnabled = false;
            }
            // khi người dùng nhấn nút sửa
            else if (flag == 2)
            {
                btnThem.IsEnabled = false;
                btnSua.IsEnabled = true;
            }
            // là khi người dùng bấm nút xem chi tiết
            else
            {
                btnThem.IsEnabled = false;
                isEnabledThongTin(false);
            }
            if (nhanVien != null)
            {
                nhanVienSelect = nhanVien;
                hienThiThongTin(phieuNhapNguyenLieuSelect);
            }
        }

        private void hienThiThongTin(PhieuNhapNguyenLieu phieuNhap)
        {
            dateNgayNhap.SelectedDate = DateTime.Now;
            txtMaNhanVien.Text = nhanVienSelect.maNhanVien;
            txtTenNhanVien.Text = nhanVienSelect.tenNhanVien;
            cmbTenNguyenLieu.ItemsSource = CNguyenLieu_BUS.toListTen();
        }

        private void isEnabledThongTin(bool value)
        {
            dateNgayNhap.IsEnabled = value;
            cmbTenNguyenLieu.IsEnabled = value;
            txtSoLuong.IsEnabled = value;
            txtDonGia.IsEnabled = value;
            dateNgayHetHan.IsEnabled = value;
            cmbDonViTinh.IsEnabled = value;
        }

        private void dgDSChiTietPhieuNhap_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnTaoPhieuNhap_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
