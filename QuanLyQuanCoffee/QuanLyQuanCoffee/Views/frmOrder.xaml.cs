using QuanLyQuanCoffee.BUS;
using QuanLyQuanCoffee.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
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
    /// Interaction logic for frmOrder.xaml
    /// </summary>
    public partial class frmOrder : Page
    {
        NhanVien nhanVienSelect;
        private List<ChiTietHoaDon> chiTietHoaDons;
        //private HoaDon hoaDonTreo;
        private double tongThanhTien;

        public frmOrder(NhanVien nhanVien = null)
        {
            InitializeComponent();
            nhanVienSelect = nhanVien;
            if (nhanVienSelect == null)
            {
                nhanVienSelect = new NhanVien();
            }
            chiTietHoaDons = new List<ChiTietHoaDon>();
            txtTenNhanVien.Content = nhanVienSelect.hoNhanVien + " " + nhanVienSelect.tenNhanVien;
        }

        public void hienthitheoListBOX(string maloai)
        {
            List<SanPham> listLoaiSP = CSanPham_BUS.hienthiTheoMa(maloai);
            if (listLoaiSP.Count() > 0)
            {
                dgDanhsachsanpham.ItemsSource = listLoaiSP;
            }
            else
            {
                MessageBox.Show("Không có loại sản phẩm nào");
            }

        }

        private void LstBoxLoaisanpham_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int dongthu = LstBoxLoaisanpham.SelectedIndex;
            if (dongthu < 0)
            {
                MessageBox.Show("Chưa có loại sản phẩm nào được cập nhập");
                return;
            }
            string maLoai = CLoaiSanPham_BUS.layMaloaitheoSo(dongthu);
            hienthitheoListBOX(maLoai);
        }

        private void LstBoxLoaisanpham_Loaded(object sender, RoutedEventArgs e)
        {
            LstBoxLoaisanpham.ItemsSource = CLoaiSanPham_BUS.DSLoaiSPtheoTen();//hien thi ra list ten do uong
        }

        private void hienThiDSChiTietHD(List<ChiTietHoaDon> chiTietHoaDons)
        {
            if (chiTietHoaDons != null)
            {
                dgChitiethoadon.ItemsSource = chiTietHoaDons.Select(x => new
                {
                    maHoaDon = x.maHoaDon,
                    maSanPham = x.maSanPham,
                    tenSanPham = x.SanPham.tenSanPham,
                    soLuong = x.soLuong,
                    donGia = String.Format("{0:#,###,0 VND;(#,###,0 VND);0 VND}", x.SanPham.donGia),
                    thanhTien = String.Format("{0:#,###,0 VND;(#,###,0 VND);0 VND}", CChiTietHoaDon_BUS.tinhThanhTien(x))
                }).ToList();
                tongThanhTien = tinhTongThanhTien(chiTietHoaDons);
                txtTongTien.Text = String.Format("{0:#,###,0 VND;(#,###,0 VND);0 VND}", tongThanhTien);
            }
        }

        public double tinhTongThanhTien(List<ChiTietHoaDon> chiTietHoaDons)
        {
            double tongThanhTien = 0;
            chiTietHoaDons.ForEach(item => tongThanhTien += CChiTietHoaDon_BUS.tinhThanhTien(item));
            return tongThanhTien;
        }

        private void txtTenNhanVien_Loaded(object sender, RoutedEventArgs e)
        {
            txtTenNhanVien.Content = nhanVienSelect.hoNhanVien + " " + nhanVienSelect.tenNhanVien;
        }

        private void btnChonsanpham_Click(object sender, RoutedEventArgs e)
        {
            if (dgDanhsachsanpham.SelectedItem == null)
            {
                MessageBox.Show("Bạn chưa chọn sản phẩm");
                return;
            }
            SanPham sanPham = CSanPham_BUS.find(dgDanhsachsanpham.SelectedValue.ToString());
            if (sanPham == null)
            {
                MessageBox.Show("khong co san pham");
            }
            else
            {
                ChiTietHoaDon chiTietHoaDon = new ChiTietHoaDon();
                chiTietHoaDon.maSanPham = sanPham.maSanPham;
                chiTietHoaDon.soLuong = 1;
                chiTietHoaDon.SanPham = sanPham;
                if (chiTietHoaDons.Where(x => x.maSanPham == chiTietHoaDon.maSanPham).Count() == 0)
                {
                    chiTietHoaDons.Add(chiTietHoaDon);
                    hienThiDSChiTietHD(chiTietHoaDons);
                }
                else
                {
                    ChiTietHoaDon a = new ChiTietHoaDon();
                    a = chiTietHoaDons.Where(x => x.maSanPham == chiTietHoaDon.maSanPham).FirstOrDefault();
                    a.soLuong += 1;
                    hienThiDSChiTietHD(chiTietHoaDons);
                }
            }
        }

        private void btnBochon_Click(object sender, RoutedEventArgs e)
        {
            if (dgChitiethoadon.SelectedItem == null)
            {
                return;
            }

            string maSanPham = dgChitiethoadon.SelectedValue.ToString();
            ChiTietHoaDon chiTietHoaDon = chiTietHoaDons.Where(x => x.maSanPham == maSanPham).FirstOrDefault();

            if (chiTietHoaDon == null)
            {
                MessageBox.Show("chua chon san pham trong chi tiet hoa don");
            }
            else
            {
                if (chiTietHoaDon.soLuong > 1)
                {
                    ChiTietHoaDon a = new ChiTietHoaDon();
                    a = chiTietHoaDons.Where(x => x.maSanPham == chiTietHoaDon.maSanPham).FirstOrDefault();
                    a.soLuong -= 1;
                    hienThiDSChiTietHD(chiTietHoaDons);
                }
                else
                {
                    chiTietHoaDons.Remove(chiTietHoaDon);
                    hienThiDSChiTietHD(chiTietHoaDons);
                }
            }
        }

        private void btnHuy_Click(object sender, RoutedEventArgs e)
        {
            if (dgChitiethoadon.SelectedItem != null)
            {
                string maSanPham = dgChitiethoadon.SelectedValue.ToString();
                ChiTietHoaDon chiTietHoaDon = chiTietHoaDons.Where(x => x.maSanPham == maSanPham).FirstOrDefault();

                if (chiTietHoaDon == null)
                {
                    MessageBox.Show("chua chon san pham trong chi tiet hoa don");
                }
                else
                {
                    chiTietHoaDons.Remove(chiTietHoaDon);
                    hienThiDSChiTietHD(chiTietHoaDons);
                }
            }
        }

        private void btnTinhtien_Click(object sender, RoutedEventArgs e)
        {
            if (chiTietHoaDons.Count() == 0)
            {
                MessageBox.Show("Hóa Đơn chưa có chi tiết hóa đơn");
                return;
            }

            string maHoaDon = CServices.taoMa<HoaDon>(CHoaDon_BUS.toList());
            if (CHoaDon_BUS.find(maHoaDon) == null)
            {
                try
                {
                    HoaDon hoaDon = new HoaDon();
                    hoaDon.maHoaDon = maHoaDon;
                    hoaDon.maNhanVien = nhanVienSelect.maNhanVien;
                    hoaDon.ngayLap = DateTime.Now;
                    hoaDon.tongThanhTien = tongThanhTien;

                    hoaDon.trangThai = 0;

                    foreach (var item in chiTietHoaDons)
                    {
                        ChiTietHoaDon a = new ChiTietHoaDon();
                        a.maHoaDon = hoaDon.maHoaDon;
                        a.maSanPham = item.maSanPham;
                        a.soLuong = item.soLuong;
                        a.thanhTien = CChiTietHoaDon_BUS.tinhThanhTien(item);
                        hoaDon.ChiTietHoaDons.Add(a);
                    }

                    if (CHoaDon_BUS.add(hoaDon))
                    {
                        MessageBox.Show("Xuất hóa đơn thành công");
                    }

                    chiTietHoaDons.Clear();
                    hienThiDSChiTietHD(chiTietHoaDons);

                    tongThanhTien = 0;
                    txtTongTien.Text = "";
                    txtTienKhachDua.Text = "";
                    txtTienThoiLai.Text = "";
                }
                catch (DbEntityValidationException)
                {
                    MessageBox.Show("loi kieu du lieu");
                }
                catch (DbUpdateException)
                {
                    MessageBox.Show("data k update");
                }
                catch (ArgumentNullException)
                {
                    MessageBox.Show("Không được để rỗng đơn giá");
                }
                catch (FormatException)
                {
                    MessageBox.Show("Đơn giá phải là số");
                }
                catch (OverflowException)
                {
                    MessageBox.Show("Đơn giá vượt quá giới hạn lưu trữ");
                }
            }
            else
            {
                MessageBox.Show("Ma hoa don da ton tai");
            }

        }

        public void themChiTietHoaDon()
        {
            foreach (ChiTietHoaDon item in chiTietHoaDons)
            {
                CChiTietHoaDon_BUS.add(item);
            }
        }

        private void dgDanhsachsanpham_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            btnChonsanpham_Click(sender, e);
        }

        private void dgDanhsachsanpham_Loaded(object sender, RoutedEventArgs e)
        {
            dgDanhsachsanpham.ItemsSource = CSanPham_BUS.toList().Select(x => new
            {
                maSanPham = x.maSanPham,
                tenSanPham = x.tenSanPham,
                donGia = String.Format("{0:#,###,0 VND;(#,###,0 VND);0 VND}", x.donGia)
            }).ToList();
        }

        private void txtTienKhachDua_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    double tienKhachDua = int.Parse(txtTienKhachDua.Text);
                    double tienThoiLai = tienKhachDua - tongThanhTien;
                    if (tienThoiLai > 0)
                    {
                        txtTienThoiLai.Text = String.Format("{0:#,###,0 VND;(#,###,0 VND);0 VND}", tienThoiLai);
                    }
                    else
                    {
                        txtTienThoiLai.Text = "";
                    }
                }
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Tiền khách đưa null");
            }
            catch (FormatException)
            {
                MessageBox.Show("Nhập vào phải là số");
            }
            catch (OverflowException)
            {
                MessageBox.Show("Số nhập vào dài quá giới hạn cho phép");
            }
        }

        private void btnTreoHoaDon_Click(object sender, RoutedEventArgs e)
        {
            if (CHoaDon_BUS.hoaDonTreo == null)
            {
                try
                {
                    CHoaDon_BUS.hoaDonTreo = new HoaDon();
                    CHoaDon_BUS.hoaDonTreo.maNhanVien = nhanVienSelect.maNhanVien;
                    CHoaDon_BUS.hoaDonTreo.ngayLap = DateTime.Now;
                    CHoaDon_BUS.hoaDonTreo.tongThanhTien = tongThanhTien;

                    CHoaDon_BUS.hoaDonTreo.trangThai = 0;

                    foreach (var item in chiTietHoaDons)
                    {
                        ChiTietHoaDon a = new ChiTietHoaDon();
                        a.maHoaDon = CHoaDon_BUS.hoaDonTreo.maHoaDon;
                        a.maSanPham = item.maSanPham;
                        a.SanPham = CSanPham_BUS.find(item.maSanPham);
                        if (a.SanPham == null)
                        {
                            MessageBox.Show("Lỗi, không thể treo hóa đơn");
                            return;
                        }
                        a.soLuong = item.soLuong;
                        a.thanhTien = CChiTietHoaDon_BUS.tinhThanhTien(item);
                        CHoaDon_BUS.hoaDonTreo.ChiTietHoaDons.Add(a);
                    }
                    chiTietHoaDons.Clear();
                    hienThiDSChiTietHD(chiTietHoaDons);
                }
                catch (ArgumentNullException)
                {
                    MessageBox.Show("Tổng tiền null");
                }
                catch (FormatException)
                {
                    MessageBox.Show("Tổng tiền không thể format");
                }
                catch (OverflowException)
                {
                    MessageBox.Show("Tổng tiền vượt quá giới hạn cho phép");
                }
            }
            else
            {
                MessageBox.Show("Chỉ có thể treo 1 hóa đơn, vui lòng hoàn tác hóa đơn trước");
            }
        }

        private void btnHoanTac_Click(object sender, RoutedEventArgs e)
        {
            if (dgChitiethoadon.Items.Count == 0)
            {
                if (CHoaDon_BUS.hoaDonTreo != null)
                {
                    txtTongTien.Text = String.Format("{0:#,###,0 VND;(#,###,0 VND);0 VND}", CHoaDon_BUS.hoaDonTreo.tongThanhTien);

                    foreach (var item in CHoaDon_BUS.hoaDonTreo.ChiTietHoaDons)
                    {
                        chiTietHoaDons.Add(item);
                    }
                    hienThiDSChiTietHD(chiTietHoaDons);
                    CHoaDon_BUS.hoaDonTreo = null;
                }
            }
            else
            {
                MessageBox.Show("Không thể hoàn tác hóa đơn khi đang lập hóa đơn mới");
            }
        }
    }
}
