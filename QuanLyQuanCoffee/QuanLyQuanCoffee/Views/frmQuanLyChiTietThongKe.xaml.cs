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
    /// Interaction logic for frmQuanLyChiTietThongKe.xaml
    /// </summary>
    public partial class frmQuanLyChiTietThongKe : Window
    {
        public frmQuanLyChiTietThongKe(ThongKe thongKe = null)
        {
            InitializeComponent();
        }

        private ThongKe taoThongKe()
        {
            ThongKe thongKe = new ThongKe();
            List<KetCa> ketCas = CCa_BUS.toList();
            return thongKe;
        }

        private void dateNgayBatDau_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key.ToString() == "Tab" || e.Key == Key.Enter)
            {
                try
                {
                    string ngayBatDau = dateNgayBatDau.Text;
                    if (DateTime.Parse(ngayBatDau) > DateTime.Now)
                    {
                        MessageBox.Show("Ngày bắt đầu không thể lớn hơn ngày hiện tại");
                    }
                    else
                    {
                        dateNgayBatDau.SelectedDate = DateTime.Parse(ngayBatDau);
                    }
                }
                catch (ArgumentNullException)
                {
                    MessageBox.Show("Ngày bắt đầu không được rỗng");
                }
                catch (FormatException)
                {
                    MessageBox.Show("Lỗi Định dạng! Ngày bắt đầu phải là: MM/dd/yyyy");
                }
            }
        }

        private void dateNgayKetThuc_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key.ToString() == "Tab" || e.Key == Key.Enter)
            {
                try
                {
                    string ngayKetThuc = dateNgayKetThuc.Text;
                    if (DateTime.Parse(ngayKetThuc) > DateTime.Now)
                    {
                        MessageBox.Show("Ngày kết thúc không thể lớn hơn ngày hiện tại");
                    }
                    else
                    {
                        dateNgayKetThuc.SelectedDate = DateTime.Parse(ngayKetThuc);
                    }
                }
                catch (ArgumentNullException)
                {
                    MessageBox.Show("Ngày kết thúc không được rỗng");
                }
                catch (FormatException)
                {
                    MessageBox.Show("Lỗi Định dạng! Ngày kết thúc phải là: MM/dd/yyyy");
                }
            }
        }

        private void btnTao_Click(object sender, RoutedEventArgs e)
        {
            if (dateNgayBatDau.SelectedDate != null && dateNgayKetThuc.SelectedDate != null)
            {
                hienThi();
            }
            else
            {
                MessageBox.Show("Ngày bắt đầu và ngày kết thúc phải có đầy đủ");
            }
        }

        private void hienThi()
        {
            if (dateNgayBatDau.SelectedDate > DateTime.Now || dateNgayKetThuc.SelectedDate > DateTime.Now)
            {
                MessageBox.Show("Ngày bắt đầu và ngày kết thúc không được lớn hơn ngày hiện tại");
                return;
            }
            List<HoaDon> hoaDons = new List<HoaDon>();
            List<NhanVien> nhanViens = new List<NhanVien>();
            hoaDons = CHoaDon_BUS.toList(dateNgayBatDau.SelectedDate.Value, dateNgayKetThuc.SelectedDate.Value);
            nhanViens = CNhanVien_BUS.toList();
            if (hoaDons.Count() > 0)
            {
                txtMaThongKe.Text = CServices.taoMa<ThongKe>(CThongKe.toList());
                txtNgayLapThongKe.Text = DateTime.Now.ToString("dd/MM/yyyy");
                double tongThanhTien = 0;
                hoaDons.ForEach(x => tongThanhTien += x.tongThanhTien);
                txtTongThanhTien.Text = String.Format("{0:#,###,0 VND;(#,###,0 VND);0 VND}", tongThanhTien);

                var chiTietThongKes = new List<Tuple<string, string, string, int, int, double>>();
                //bool flag = false;
                foreach (var nhanVien in nhanViens)
                {
                    int soLuongHoaDon = 0;
                    int soLuongBan = 0;
                    double thanhTien = 0;
                    foreach (var hoaDon in hoaDons)
                    {
                        if (nhanVien.maNhanVien == hoaDon.maNhanVien)
                        {
                            soLuongBan += hoaDon.ChiTietHoaDons.Count();
                            thanhTien += hoaDon.tongThanhTien;
                            soLuongHoaDon++;
                            //flag = true;
                        }
                    }
                    //if (flag)
                    //{
                        var chiTietThongKe = new Tuple<string, string, string, int, int, double>(
                            nhanVien.maNhanVien,
                            nhanVien.hoNhanVien,
                            nhanVien.tenNhanVien,
                            soLuongHoaDon,
                            soLuongBan,
                            thanhTien);
                        chiTietThongKes.Add(chiTietThongKe);
                    //}
                    //flag = false;
                }

                dgChiTietThongKe.ItemsSource = chiTietThongKes.Select(x => new
                {
                    maNhanVien = x.Item1,
                    hoNhanVien = x.Item2,
                    tenNhanVien = x.Item3,
                    soLuongHoaDon = x.Item4,
                    soLuongBan = x.Item5,
                    thanhTien = x.Item6
                });
            }
            else
            {
                MessageBox.Show("Không có hóa đơn nào được bán");
            }
        }
    }
}
