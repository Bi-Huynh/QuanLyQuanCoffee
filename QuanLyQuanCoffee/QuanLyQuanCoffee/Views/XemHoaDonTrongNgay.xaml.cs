using QuanLyQuanCoffee.BUS;
using QuanLyQuanCoffee.DTO;
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
    /// Interaction logic for XemHoaDonTrongNgay.xaml
    /// </summary>
    public partial class XemHoaDonTrongNgay : Page
    {
        private List<HoaDon> hoaDons;
        private HoaDon hoaDonSelect;

        public XemHoaDonTrongNgay()
        {
            InitializeComponent();
            CCa_DTO caLam = CCa_BUS.CaLamViec;
            if (caLam != null)
            {
                hoaDons = CHoaDon_BUS.toList(caLam); // chi lấy danh sách hóa đơn trong ca thực hiện
            }

            if (hoaDons == null || hoaDons.Count == 0)
            {
                hoaDons = new List<HoaDon>();
            }

            hienThi();
        }

        public void hienThi()
        {
            dgHoaDonTrongNgay.ItemsSource = hoaDons.Select(x => new
            {
                maHoaDon = x.maHoaDon,
                tenNhanVien = x.NhanVien.hoNhanVien + " " + x.NhanVien.tenNhanVien,
                ngayLap = x.ngayLap,
                tongThanhTien = x.tongThanhTien
            });
        }

        private void dgHoaDonTrongNgay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgHoaDonTrongNgay.SelectedItem != null)
            {
                hoaDonSelect = new HoaDon();
                string maHoaDon = dgHoaDonTrongNgay.SelectedValue.ToString();
                hoaDonSelect = CHoaDon_BUS.find(maHoaDon);
            }
        }

        private void dgHoaDonTrongNgay_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (hoaDonSelect != null)
                {
                    HoaDon hoaDon = CHoaDon_BUS.find(hoaDonSelect.maHoaDon);

                    // Lỗi NullReferenceException vẫn chưa bắt được
                    dgChiTietHoaDonTrongNgay.ItemsSource = hoaDon.ChiTietHoaDons.Select(x => new
                    {
                        maHoaDon = x.maHoaDon,
                        tenSanPham = x.SanPham.tenSanPham,
                        soLuong = x.soLuong,
                        thanhTien = x.thanhTien
                    });
                }
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Chưa load được dữ liệu");
            }
        }
    }
}
