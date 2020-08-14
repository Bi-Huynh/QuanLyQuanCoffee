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
        public frmThongTinChiTietPhieuXuatNL(PhieuXuatNguyenLieu phieuXuat = null)
        {
            InitializeComponent();
            phieuXuatSelected = CPhieuXuatNguyenLieu_BUS.find(phieuXuat.maPhieuXuat);
            if (phieuXuatSelected == null)
            {
                phieuXuatSelected = new PhieuXuatNguyenLieu();
            }
            hienThiChitietPX();
        }
        public void hienThiChitietPX()
        {

            List<ChiTietPhieuXuat> list = CChiTietPhieuXuatNguyenLieu.toList(phieuXuatSelected.maPhieuXuat);
            if (list.Count() > 0)
            {
                dgQlchitietphieuxuat.ItemsSource = list.Select(x => new {

                    maChiTietPhieuXuat = x.maChiTietPhieuXuat,
                    maNguyenLieu = x.maChitietNguyenLieu.Substring(0, 13),
                    tenNguyenLieu = x.ChiTietNguyenLieu.NguyenLieu.tenNguyenLieu,
                    soLuong = x.soLuong,
                    donGia = x.donGia,
                    thanhTien=x.thanhTien
                });
            }
            else
            {
                MessageBox.Show("Phiếu Xuất này không có chi tiết Phiếu Xuất");
                return;
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtMaPhieuXuat.Text = phieuXuatSelected.maPhieuXuat;
            txtNgayxuat.Text = phieuXuatSelected.ngayXuat.Value.ToString("dd/MM/yyyy");
            txtTongthanhtien.Text = phieuXuatSelected.tongThanhTien.ToString();
            txtNguoilapPhieuXuat.Text = phieuXuatSelected.NhanVien.hoNhanVien + phieuXuatSelected.NhanVien.tenNhanVien;
        }
    }
}
