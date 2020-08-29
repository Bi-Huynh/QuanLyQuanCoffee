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
    /// Interaction logic for frmXemHoaDonKetCa.xaml
    /// </summary>
    public partial class frmXemHoaDonKetCa : Window
    {
        private KetCa ketCaSelect;
        private HoaDon hoaDonSelect;

        public frmXemHoaDonKetCa(KetCa ketCa = null)
        {
            InitializeComponent();
            if (ketCa == null)
            {
                ketCaSelect = new KetCa();
            }
            else
            {
                ketCaSelect = ketCa;
                hienThiHoaDon(ketCaSelect.HoaDons.ToList());
                txtTongDoanhThu.Text = String.Format("{0:#,###,0 VND;(#,###,0 VND);0 VND}", ketCaSelect.tongTienBan);
            }
        }

        public void hienThiHoaDon(List<HoaDon> hoaDons)
        {
            if (hoaDons.Count() >= 0)
            {
                dgHoaDonTrongNgay.ItemsSource = hoaDons.Select(x => new
                {
                    maHoaDon = x.maHoaDon,
                    tenNhanVien = x.NhanVien.hoNhanVien + " " + x.NhanVien.tenNhanVien,
                    ngayLap = x.ngayLap.Date.ToString("dd/MM/yyyy"),
                    thoiGian = x.ngayLap.ToString("hh:mm:ss"),
                    tienKhachDua = String.Format("{0:#,###,0 VND;(#,###,0 VND);0 VND}", x.tienKhachDua),
                    tienThua = String.Format("{0:#,###,0 VND;(#,###,0 VND);0 VND}", x.tienThua),
                    tongThanhTien = String.Format("{0:#,###,0 VND;(#,###,0 VND);0 VND}", x.tongThanhTien)
                });
            }
        }

        public void hienThiChiTietHoaDon(List<ChiTietHoaDon> chiTietHoaDons)
        {
            if (chiTietHoaDons.Count() >= 0)
            {
                dgChiTietHoaDonTrongNgay.ItemsSource = chiTietHoaDons.Select(x => new
                {
                    maHoaDon = x.maHoaDon,
                    tenSanPham = x.SanPham.tenSanPham,
                    soLuong = x.soLuong,
                    thanhTien = String.Format("{0:#,###,0 VND;(#,###,0 VND);0 VND}", x.thanhTien)
                });
            }
        }

        private void dgHoaDonTrongNgay_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (dgHoaDonTrongNgay.SelectedItem != null)
                {
                    hoaDonSelect = new HoaDon();
                    string maHoaDon = dgHoaDonTrongNgay.SelectedValue.ToString();
                    hoaDonSelect = CHoaDon_BUS.find(maHoaDon);
                    if (hoaDonSelect != null)
                    {
                        HoaDon hoaDon = CHoaDon_BUS.find(hoaDonSelect.maHoaDon);

                        // Lỗi NullReferenceException vẫn chưa bắt được
                        dgChiTietHoaDonTrongNgay.ItemsSource = hoaDon.ChiTietHoaDons.Select(x => new
                        {
                            maHoaDon = x.maHoaDon,
                            tenSanPham = x.SanPham.tenSanPham,
                            soLuong = x.soLuong,
                            donGia = String.Format("{0:#,###,0 VND;(#,###,0 VND);0 VND}", x.SanPham.donGia),
                            thanhTien = String.Format("{0:#,###,0 VND;(#,###,0 VND);0 VND}", x.thanhTien)
                        });
                    }
                }
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Chưa load được dữ liệu");
            }
        }
    }
}
