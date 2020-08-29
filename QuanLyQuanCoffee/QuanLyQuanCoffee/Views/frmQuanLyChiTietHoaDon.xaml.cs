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
using System.Windows.Shapes;

namespace QuanLyQuanCoffee.Views
{
    /// <summary>
    /// Interaction logic for frmQuanLyChiTietHoaDon.xaml
    /// </summary>
    public partial class frmQuanLyChiTietHoaDon : Window
    {
        HoaDon hoaDonSelected;
        public frmQuanLyChiTietHoaDon(HoaDon hoaDon)
        {
            InitializeComponent();
            hoaDonSelected = hoaDon;
            hienthiHoaDon();
            hienthiChiTietHD(hoaDonSelected);
        }
        public void hienthiHoaDon()
        {
            txtMahoadon.Text = hoaDonSelected.maHoaDon;
            txtNguoilaphoadon.Text = hoaDonSelected.NhanVien.hoNhanVien + " " + hoaDonSelected.NhanVien.tenNhanVien;
            txtNgaylap.Text = hoaDonSelected.ngayLap.ToString("dd/MM/yyyy");
            txtTongthanhtien.Text = String.Format("{0:#,###,0 VND;(#,###,0 VND);0 VND}", hoaDonSelected.tongThanhTien);
        }
        public void hienthiChiTietHD(HoaDon hoadon)
        {
            List<ChiTietHoaDon> list = CChiTietHoaDon_BUS.toList(hoadon.maHoaDon);
            if (list.Count() > 0)
            {
                dgQlchitiethoadon.ItemsSource = list.Select(x => new { 
                    maHoaDon = x.maHoaDon,
                    maSanPham = x.maSanPham,
                    tenSanPham = x.SanPham.tenSanPham,
                    soLuong = x.soLuong,
                    donGia = String.Format("{0:#,###,0 VND;(#,###,0 VND);0 VND}", x.SanPham.donGia),
                    thanhTien = String.Format("{0:#,###,0 VND;(#,###,0 VND);0 VND}", x.thanhTien)
                });
            }
            else
            {
                MessageBox.Show("Hóa đơn này không có chi tiết hóa đơn");
            }

        }
    }
}
