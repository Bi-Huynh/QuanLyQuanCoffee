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
            phieuXuatSelected = phieuXuat;
            if (phieuXuatSelected == null)
            {
                phieuXuatSelected = new PhieuXuatNguyenLieu();
            }
            else
            {
                hienThiChitietPX();
                Load();
            }
        }

        public void hienThiChitietPX()
        {
            List<ChiTietPhieuXuat> list = CChiTietPhieuXuatNguyenLieu.toList(phieuXuatSelected.maPhieuXuat);
            if (list.Count() > 0)
            {
                dgQlchitietphieuxuat.ItemsSource = list.Select(x => new
                {

                    maChiTietPhieuXuat = x.maChiTietPhieuXuat,
                    maNguyenLieu = x.ChiTietNguyenLieu.NguyenLieu.maNguyenLieu,
                    tenNguyenLieu = x.ChiTietNguyenLieu.NguyenLieu.tenNguyenLieu,
                    soLuong = x.soLuong,
                    donGia = x.donGia,
                    thanhTien = x.thanhTien
                });
            }
            else
            {
                MessageBox.Show("Phiếu Xuất này không có chi tiết Phiếu Xuất");
                return;
            }
        }

        private void Load()
        {
            if (phieuXuatSelected != null)
            {
                txtMaPhieuXuat.Text = phieuXuatSelected.maPhieuXuat;
                txtNgayxuat.Text = phieuXuatSelected.ngayXuat.Value.ToString("dd/MM/yyyy");
                txtTongthanhtien.Text = phieuXuatSelected.tongThanhTien.ToString();
                //txtNguoilapPhieuXuat.Text = phieuXuatSelected.NhanVien.hoNhanVien + phieuXuatSelected.NhanVien.tenNhanVien;
            }
        }

        
    }
}
