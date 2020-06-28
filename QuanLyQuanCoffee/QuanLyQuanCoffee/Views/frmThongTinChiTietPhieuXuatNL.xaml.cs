using QuanLyQuanCoffee.BUS;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for frmThongTinChiTietPhieuXuatNL.xaml
    /// </summary>
    public partial class frmThongTinChiTietPhieuXuatNL : Window
    {
        PhieuXuatNguyenLieu phieuXuatSelected;
        public frmThongTinChiTietPhieuXuatNL(PhieuXuatNguyenLieu phieuXuat)
        {
            InitializeComponent();
            phieuXuatSelected = phieuXuat;
            hienThiChitietPX();
        }
        public void hienThiChitietPX()
        {

            //List<ChiTietPhieuXuatNguyenLieu> list = CChiTietPhieuXuatNguyenLieu.toList(phieuXuatSelected.maPhieuXuat);
            //if (list.Count() > 0)
            //{
            //    dgQlchitietphieuxuat.ItemsSource = list;
            //}
            //else
            //{
            //    MessageBox.Show("Hóa đơn này không có chi tiết hóa đơn");
            //}
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtMaPhieuXuat.Text = phieuXuatSelected.maPhieuXuat;
            txtNgayxuat.Text = phieuXuatSelected.ngayXuat.ToString();
            txtTongthanhtien.Text = phieuXuatSelected.tongThanhTien.ToString();
            txtNguoilapPhieuXuat.Text = phieuXuatSelected.NhanVien.hoNhanVien + phieuXuatSelected.NhanVien.tenNhanVien;
        }
    }
}
