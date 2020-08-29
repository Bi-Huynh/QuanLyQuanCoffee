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
    /// Interaction logic for frmXemThongKe.xaml
    /// </summary>
    public partial class frmXemThongKe : Page
    {
        private int monthSelect;
        private List<CBangXepHangSanPham> dsSanPham;
        private List<CBangXepHangNguyenLieuNhap> dsNguyenLieu;

        public frmXemThongKe()
        {
            InitializeComponent();
            monthSelect = DateTime.Now.Month;

            dsSanPham = new List<CBangXepHangSanPham>();
            dsNguyenLieu = new List<CBangXepHangNguyenLieuNhap>();

            hienThiTKNguyenLieuNhap(monthSelect);
            hienThiTKNguyenLieuXuat(monthSelect);
            showBangXepHang(monthSelect);
        }

        private void hienThiTKSanPham(int month)
        {
            List<SanPham> sanPhams = CSanPham_BUS.toList();
            if (sanPhams.Count() > 0)
            {
                foreach (var sanPham in sanPhams)
                {
                    int soLuongBan = CHoaDon_BUS.demSoLuongSanPham(sanPham.maSanPham, month);
                    double tongThanhTien = CHoaDon_BUS.tongTienBanSanPham_(sanPham.maSanPham, soLuongBan);
                    dsSanPham.Add(new CBangXepHangSanPham(
                        sanPham.maSanPham,
                        sanPham.tenSanPham,
                        soLuongBan,
                        sanPham.donGia.Value));
                }

                dgBangThongKeSanPham.ItemsSource = dsSanPham.Select(x => new
                {
                    maSanPham = x.MaSanPham,
                    tenSanPham = x.TenSanPham,
                    soLuongBan = x.SoLuongBan,
                    donGia = String.Format("{0:#,###,0 VND;(#,###,0 VND);0 VND}", x.DonGia),
                    thanhTien = String.Format("{0:#,###,0 VND;(#,###,0 VND);0 VND}", x.TongTien)
                });

                double tongTien = 0;
                foreach (var item in dsSanPham)
                {
                    tongTien += item.TongTien;
                }

                txtTongThanhTienBan.Text = String.Format("{0:#,###,0 VND;(#,###,0 VND);0 VND}", tongTien);
            }
        }

        private void hienThiTKNguyenLieuNhap(int month)
        {
            List<NguyenLieu> nguyenLieus = new List<NguyenLieu>();
            List<PhieuNhapNguyenLieu> phieuNhapNguyenLieus = new List<PhieuNhapNguyenLieu>();
            nguyenLieus = CNguyenLieu_BUS.to_List();
            phieuNhapNguyenLieus = CPhieuNhapNguyenLieu_BUS.toListInMonth(month);
            if (phieuNhapNguyenLieus.Count() > 0)
            {
                foreach (NguyenLieu nguyenLieu in nguyenLieus)
                {
                    foreach (PhieuNhapNguyenLieu phieuNhap in phieuNhapNguyenLieus)
                    {
                        foreach (ChiTietPhieuNhap chiTiet in phieuNhap.ChiTietPhieuNhaps.ToList())
                        {
                            if (chiTiet.ChiTietNguyenLieu.maNguyenLieu == nguyenLieu.maNguyenLieu)
                            {
                                int soLuong = chiTiet.soLuong.Value;
                                double donGia = chiTiet.donGia.Value;
                                dsNguyenLieu.Add(new CBangXepHangNguyenLieuNhap(
                                    nguyenLieu.maNguyenLieu,
                                    nguyenLieu.tenNguyenLieu,
                                    soLuong,
                                    donGia));
                            }
                        }
                    }
                }
                dgBangThongKeNguyenLieuNhap.ItemsSource = dsNguyenLieu.Select(x => new
                {
                    maNguyenLieuNhap = x.MaNguyenLieu,
                    tenNguyenLieuNhap = x.TenNguyenLieu,
                    soLuongNhap = x.SoLuong,
                    donGiaNhap = String.Format("{0:#,###,0 VND;(#,###,0 VND);0 VND}", x.DonGia),
                    thanhTienNhap = String.Format("{0:#,###,0 VND;(#,###,0 VND);0 VND}", x.TongTien)
                });

                double tongTien = 0;
                foreach (var item in dsNguyenLieu)
                {
                    tongTien += item.TongTien;
                }

                txtTongThanhTienNhap.Text = String.Format("{0:#,###,0 VND;(#,###,0 VND);0 VND}", tongTien);
            }
        }

        private void hienThiTKNguyenLieuXuat(int month)
        {
            List<NguyenLieu> nguyenLieus = new List<NguyenLieu>();
            List<PhieuXuatNguyenLieu> phieuXuatNguyenLieus = new List<PhieuXuatNguyenLieu>();
            nguyenLieus = CNguyenLieu_BUS.to_List();
            phieuXuatNguyenLieus = CPhieuXuatNguyenLieu_BUS.toListInMonth(month);
            if (phieuXuatNguyenLieus.Count() > 0)
            {
                foreach (NguyenLieu nguyenLieu in nguyenLieus)
                {
                    foreach (PhieuXuatNguyenLieu phieuXuat in phieuXuatNguyenLieus)
                    {
                        foreach (ChiTietPhieuXuat chiTiet in phieuXuat.ChiTietPhieuXuats.ToList())
                        {
                            if (chiTiet.ChiTietNguyenLieu.maNguyenLieu == nguyenLieu.maNguyenLieu)
                            {
                                int soLuong = chiTiet.soLuong.Value;
                                double donGia = chiTiet.donGia.Value;

                                dsNguyenLieu.Add(new CBangXepHangNguyenLieuNhap(
                                    nguyenLieu.maNguyenLieu,
                                    nguyenLieu.tenNguyenLieu,
                                    soLuong,
                                    donGia));
                            }
                        }
                    }
                }
                dgBangThongKeNguyenLieuXuat.ItemsSource = dsNguyenLieu.Select(x => new
                {
                    maNguyenLieuXuat = x.MaNguyenLieu,
                    tenNguyenLieuXuat = x.TenNguyenLieu,
                    soLuongXuat = x.SoLuong,
                    donGiaXuat = String.Format("{0:#,###,0 VND;(#,###,0 VND);0 VND}", x.DonGia),
                    thanhTienXuat = String.Format("{0:#,###,0 VND;(#,###,0 VND);0 VND}", x.TongTien)
                });

                double tongTien = 0;
                foreach (var item in dsNguyenLieu)
                {
                    tongTien += item.TongTien;
                }

                txtTongThanhTienXuat.Text = String.Format("{0:#,###,0 VND;(#,###,0 VND);0 VND}", tongTien);
            }
        }

        private void showBangXepHang(int month)
        {
            List<NhanVien> nhanViens = CNhanVien_BUS.toList();
            List<CBangXepHang> bangXepHangs = new List<CBangXepHang>();
            if (nhanViens.Count() > 0)
            {
                int stt = 0;
                foreach (var nhanVien in nhanViens)
                {
                    int soLuongHoaDon = CHoaDon_BUS.demSoLuongHoaDon(nhanVien.maNhanVien, month);
                    double tongThanhTien = CHoaDon_BUS.tongTienBan(nhanVien.maNhanVien, month);
                    stt++;
                    bangXepHangs.Add(new CBangXepHang(
                        nhanVien.maNhanVien,
                        nhanVien.hoNhanVien + " " + nhanVien.tenNhanVien,
                        soLuongHoaDon,
                        tongThanhTien));
                }

                dgBangThongKeNhanVien.ItemsSource = bangXepHangs.Select(x => new
                {
                    maNhanVien = x.MaNhanVien,
                    tenNhanVien = x.HoTen,
                    soLuongHoaDon = x.SoLuongHoaDon,
                    tongTienBan = String.Format("{0:#,###,0 VND;(#,###,0 VND);0 VND}", x.TongTien)
                });
            }
        }

        private void cmbThang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbThang.SelectedItem != null)
            {
                monthSelect = cmbThang.SelectedIndex + 1;

                hienThiTKSanPham(monthSelect);
            }
        }

        private void cmbThang_Loaded(object sender, RoutedEventArgs e)
        {
            cmbThang.SelectedIndex = DateTime.Now.Month - 1;

            hienThiTKSanPham(DateTime.Now.Month);
        }

        private void cmbLocSanPham_Loaded(object sender, RoutedEventArgs e)
        {
            cmbLocSanPham.ItemsSource = CSanPham_BUS.toListTenSanPham();
        }
    }
}
