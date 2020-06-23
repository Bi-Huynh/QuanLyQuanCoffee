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
    /// Interaction logic for frmXuatNguyenLieu.xaml
    /// </summary>
    public partial class frmXuatNguyenLieu : Window
    {
        NhanVien nhanVienSelected;
        public frmXuatNguyenLieu(NhanVien nhanVien)
        {
            InitializeComponent();
            HienthiNguyenLieu();
            nhanVienSelected = nhanVien;
            taoMaPhieuXuat();
        }
        public void taoMaPhieuXuat()
        {
            txtMaPhieuXuat.Text = CServices.taoMa<PhieuXuatNguyenLieu>(CPhieuXuatNguyenLieu_BUS.toList());
        }
        private void LstBoxNguyenLieu_Loaded(object sender, RoutedEventArgs e)
        {
            LstBoxNguyenLieu.ItemsSource = CNguyenLieu_BUS.DSNguyenLieuTheoTen();
        }
        public void HienthiNguyenLieu()
        {
            dgChiTietNguyenLieu.ItemsSource = CChiTietNguyenLieu_BUS.toList();
        }

        private void txtBoxNv_Loaded(object sender, RoutedEventArgs e)
        {
            txtBoxNv.Text = nhanVienSelected.hoNhanVien + " " + nhanVienSelected.tenNhanVien;
        }
    }
}
