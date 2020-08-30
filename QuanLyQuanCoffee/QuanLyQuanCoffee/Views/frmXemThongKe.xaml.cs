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
        //private List<CBangXepHangSanPham> dsSanPham;
        //private List<CBangXepHangNguyenLieuNhap> dsNguyenLieu;

        public frmXemThongKe()
        {
            InitializeComponent();
            monthSelect = DateTime.Now.Month;

            //dsSanPham = new List<CBangXepHangSanPham>();
            //dsNguyenLieu = new List<CBangXepHangNguyenLieuNhap>();

            List<CBangXepHangNguyenLieuNhap> nguyenLieuNhaps = getDSNguyenLieuNhap(monthSelect);
            showListNguyenLieuNhap(nguyenLieuNhaps);

            List<CBangXepHangNguyenLieuNhap> nguyenLieuXuats = getDSNguyenLieuXuat(monthSelect);
            showListNguyenLieuXuat(nguyenLieuXuats);

            showBangXepHang(monthSelect);
        }

        private List<CBangXepHangSanPham> getDSSanPham(int month)
        {
            List<CBangXepHangSanPham> dsSanPham = new List<CBangXepHangSanPham>();
            List<SanPham> sanPhams = CSanPham_BUS.toList();
            if (sanPhams.Count() > 0)
            {
                foreach (var sanPham in sanPhams)
                {
                    int soLuongBan = CHoaDon_BUS.demSoLuongSanPham(sanPham.maSanPham, month);

                    dsSanPham.Add(new CBangXepHangSanPham(
                        sanPham.maSanPham,
                        sanPham.tenSanPham.ToString(),
                        soLuongBan,
                        sanPham.donGia.Value));
                }
            }
            return dsSanPham;
        }

        private void showListSanPham(List<CBangXepHangSanPham> sanPhams)
        {
            dgBangThongKeSanPham.ItemsSource = sanPhams.Select(x => new
            {
                maSanPham = x.MaSanPham,
                tenSanPham = x.TenSanPham.ToString(),
                soLuongBan = x.SoLuongBan,
                donGia = String.Format("{0:#,###,0 VND;(#,###,0 VND);0 VND}", x.DonGia),
                thanhTien = String.Format("{0:#,###,0 VND;(#,###,0 VND);0 VND}", x.TongTien)
            });

            double tongTien = 0;
            foreach (var item in sanPhams)
            {
                tongTien += item.TongTien;
            }

            txtTongThanhTienBan.Text = String.Format("{0:#,###,0 VND;(#,###,0 VND);0 VND}", tongTien);
        }

        private List<CBangXepHangNguyenLieuNhap> getDSNguyenLieuNhap(int month)
        {
            List<CBangXepHangNguyenLieuNhap> dsNguyenLieuNhap = new List<CBangXepHangNguyenLieuNhap>();
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
                                dsNguyenLieuNhap.Add(new CBangXepHangNguyenLieuNhap(
                                    nguyenLieu.maNguyenLieu,
                                    nguyenLieu.tenNguyenLieu,
                                    soLuong,
                                    donGia));
                            }
                        }
                    }
                }
            }
            return dsNguyenLieuNhap;
        }

        private void showListNguyenLieuNhap(List<CBangXepHangNguyenLieuNhap> nguyenLieuNhaps)
        {
            dgBangThongKeNguyenLieuNhap.ItemsSource = nguyenLieuNhaps.Select(x => new
            {
                maNguyenLieuNhap = x.MaNguyenLieu,
                tenNguyenLieuNhap = x.TenNguyenLieu,
                soLuongNhap = x.SoLuong,
                donGiaNhap = String.Format("{0:#,###,0 VND;(#,###,0 VND);0 VND}", x.DonGia),
                thanhTienNhap = String.Format("{0:#,###,0 VND;(#,###,0 VND);0 VND}", x.TongTien)
            });

            double tongTien = 0;
            foreach (var item in nguyenLieuNhaps)
            {
                tongTien += item.TongTien;
            }

            txtTongThanhTienNhap.Text = String.Format("{0:#,###,0 VND;(#,###,0 VND);0 VND}", tongTien);
        }

        private List<CBangXepHangNguyenLieuNhap> getDSNguyenLieuXuat(int month)
        {
            List<CBangXepHangNguyenLieuNhap> dsNguyenLieuXuat = new List<CBangXepHangNguyenLieuNhap>();
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

                                dsNguyenLieuXuat.Add(new CBangXepHangNguyenLieuNhap(
                                    nguyenLieu.maNguyenLieu,
                                    nguyenLieu.tenNguyenLieu,
                                    soLuong,
                                    donGia));
                            }
                        }
                    }
                }
                
            }
            return dsNguyenLieuXuat;
        }

        private void showListNguyenLieuXuat(List<CBangXepHangNguyenLieuNhap> nguyenLieuXuats)
        {
            dgBangThongKeNguyenLieuXuat.ItemsSource = nguyenLieuXuats.Select(x => new
            {
                maNguyenLieuXuat = x.MaNguyenLieu,
                tenNguyenLieuXuat = x.TenNguyenLieu,
                soLuongXuat = x.SoLuong,
                donGiaXuat = String.Format("{0:#,###,0 VND;(#,###,0 VND);0 VND}", x.DonGia),
                thanhTienXuat = String.Format("{0:#,###,0 VND;(#,###,0 VND);0 VND}", x.TongTien)
            });

            double tongTien = 0;
            foreach (var item in nguyenLieuXuats)
            {
                tongTien += item.TongTien;
            }

            txtTongThanhTienXuat.Text = String.Format("{0:#,###,0 VND;(#,###,0 VND);0 VND}", tongTien);
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

                List<CBangXepHangSanPham> sanPhams = new List<CBangXepHangSanPham>();
                sanPhams = getDSSanPham(monthSelect);
                showListSanPham(sanPhams);

                List<CBangXepHangNguyenLieuNhap> nguyenLieuNhaps = new List<CBangXepHangNguyenLieuNhap>();
                nguyenLieuNhaps = getDSNguyenLieuNhap(monthSelect);
                showListNguyenLieuNhap(nguyenLieuNhaps);

                List<CBangXepHangNguyenLieuNhap> nguyenLieuXuats = new List<CBangXepHangNguyenLieuNhap>();
                nguyenLieuXuats = getDSNguyenLieuXuat(monthSelect);
                showListNguyenLieuXuat(nguyenLieuXuats);

                showBangXepHang(monthSelect);
            }
        }

        private void cmbThang_Loaded(object sender, RoutedEventArgs e)
        {
            cmbThang.SelectedIndex = DateTime.Now.Month - 1;

            List<CBangXepHangSanPham> sanPhams = getDSSanPham(DateTime.Now.Month);
            showListSanPham(sanPhams);
        }

        private void cmbLocSanPham_Loaded(object sender, RoutedEventArgs e)
        {
            cmbLocSanPham.ItemsSource = CSanPham_BUS.toListTenSanPham();
        }

        private void cmbLocSanPham_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbLocSanPham.SelectedItem != null)
            {
                string tenSanPham = cmbLocSanPham.SelectedItem.ToString();
                if (tenSanPham != null && tenSanPham != "")
                {
                    // Hiển thị bảng thống kê sản phẩm
                    SanPham sanPham = CSanPham_BUS.findSPbyTen(tenSanPham);
                    if (sanPham.tenSanPham != null)
                    {
                        List<CBangXepHangSanPham> list = new List<CBangXepHangSanPham>();
                        int soLuongBan = CHoaDon_BUS.demSoLuongSanPham(sanPham.maSanPham, monthSelect);
                        list.Add(new CBangXepHangSanPham(
                            sanPham.maSanPham,
                            sanPham.tenSanPham.ToString(),
                            soLuongBan,
                            sanPham.donGia.Value));

                        showListSanPham(list);
                    }

                    // Hiển thị bảng thống kê phiếu nhập nguyên liệu
                    List<CBangXepHangNguyenLieuNhap> dsNguyenLieuNhap = new List<CBangXepHangNguyenLieuNhap>();
                    List<PhieuNhapNguyenLieu> phieuNhapNguyenLieus = new List<PhieuNhapNguyenLieu>();

                    phieuNhapNguyenLieus = CPhieuNhapNguyenLieu_BUS.toListInMonth(monthSelect);
                    if (phieuNhapNguyenLieus.Count() > 0)
                    {
                        foreach (ThanhPhan thanhPhan in sanPham.ThanhPhans.ToList())
                        {
                            foreach (PhieuNhapNguyenLieu phieuNhap in phieuNhapNguyenLieus)
                            {
                                foreach (ChiTietPhieuNhap chiTiet in phieuNhap.ChiTietPhieuNhaps.ToList())
                                {
                                    if (chiTiet.ChiTietNguyenLieu.maNguyenLieu == thanhPhan.maNguyenLieu)
                                    {
                                        int soLuong = chiTiet.soLuong.Value;
                                        double donGia = chiTiet.donGia.Value;
                                        dsNguyenLieuNhap.Add(new CBangXepHangNguyenLieuNhap(
                                            thanhPhan.maNguyenLieu,
                                            thanhPhan.NguyenLieu.tenNguyenLieu,
                                            soLuong,
                                            donGia));
                                    }
                                }
                            }
                        }
                        showListNguyenLieuNhap(dsNguyenLieuNhap);
                    }

                    // Hiển thị bảng thống kê phiếu xuất nguyên liệu
                    List<CBangXepHangNguyenLieuNhap> dsNguyenLieuXuat = new List<CBangXepHangNguyenLieuNhap>();
                    List<PhieuXuatNguyenLieu> phieuXuatNguyenLieus = new List<PhieuXuatNguyenLieu>();

                    phieuXuatNguyenLieus = CPhieuXuatNguyenLieu_BUS.toListInMonth(monthSelect);
                    if (phieuXuatNguyenLieus.Count() > 0)
                    {
                        foreach (ThanhPhan thanhPhan in sanPham.ThanhPhans.ToList())
                        {
                            foreach (PhieuXuatNguyenLieu phieuXuat in phieuXuatNguyenLieus)
                            {
                                foreach (ChiTietPhieuXuat chiTiet in phieuXuat.ChiTietPhieuXuats.ToList())
                                {
                                    if (chiTiet.ChiTietNguyenLieu.maNguyenLieu == thanhPhan.maNguyenLieu)
                                    {
                                        int soLuong = chiTiet.soLuong.Value;
                                        double donGia = chiTiet.donGia.Value;

                                        dsNguyenLieuXuat.Add(new CBangXepHangNguyenLieuNhap(
                                            thanhPhan.maNguyenLieu,
                                            thanhPhan.NguyenLieu.tenNguyenLieu,
                                            soLuong,
                                            donGia));
                                    }
                                }
                            }
                        }
                        showListNguyenLieuXuat(dsNguyenLieuXuat);
                    }

                }
                else
                {
                    MessageBox.Show("Không lấy được nguyên liệu đã chọn");
                    return;
                }
            }
        }
    }
}
