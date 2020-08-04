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
        NhanVien nhanVienSelected = new NhanVien();
        PhieuXuatNguyenLieu phieuXuatnguyenlieuSelect;

        public frmQuanLyPhieuXuatNguyenLieu()
        {
            InitializeComponent();
            hienThiPhieuXuat();
        }
        public void hienThiPhieuXuat()
        {
            List<PhieuXuatNguyenLieu> list= CPhieuXuatNguyenLieu_BUS.toList();
            dgDSPhieuXuat.ItemsSource = list;
            //dgDSPhieuXuat.ItemsSource = list.Select(x => new
            //     {
            //         maPhieuXuat = x.maPhieuXuat,
            //         ngayXuat = x.ngayXuat.Value.ToString("dd/MM/yyyy"),
            //         tongThanhTien = x.tongThanhTien
            //     });
        }
        private void txtTimKiem_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void btnXemThongTinChiTiet_Click(object sender, RoutedEventArgs e)
        {
            frmThongTinChiTietPhieuXuatNL f = new frmThongTinChiTietPhieuXuatNL(phieuXuatnguyenlieuSelect);
            f.Show();

        }

        private void dgDSPhieuXuat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            phieuXuatnguyenlieuSelect = CPhieuXuatNguyenLieu_BUS.find(dgDSPhieuXuat.SelectedItem.ToString());
        }

        private void btnRefesh_Click(object sender, RoutedEventArgs e)
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
            frmXuatNguyenLieu f = new frmXuatNguyenLieu(nhanVienSelected);
            f.Show();
        }
    }
}
