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
    /// Interaction logic for frmThongTinPhieuNhap.xaml
    /// </summary>
    public partial class frmThongTinPhieuNhap : Window
    {
        private NhanVien nhanVienSelect;
        private PhieuNhapNguyenLieu phieuNhapNguyenLieuSelect;
        private List<ChiTietPhieuNhapNguyenLieu> chiTietPhieuNhapNguyenLieus;

        public frmThongTinPhieuNhap(NhanVien nhanVien = null, PhieuNhapNguyenLieu phieuNhapNguyenLieu = null, int flag = 1)
        {
            InitializeComponent();
            nhanVienSelect = nhanVien;
            phieuNhapNguyenLieuSelect = phieuNhapNguyenLieu;
            chiTietPhieuNhapNguyenLieus = new List<ChiTietPhieuNhapNguyenLieu>();

            if (nhanVienSelect == null)
            {
                nhanVienSelect = new NhanVien();
            }
            if (phieuNhapNguyenLieuSelect == null)
            {
                phieuNhapNguyenLieuSelect = new PhieuNhapNguyenLieu();
            }

            txtMaPhieuNhap.Text = CServices.taoMa<PhieuNhapNguyenLieu>(CPhieuNhapNguyenLieu_BUS.toListAll());
            //if (flag == 1)
            //{
            //    btnSua.IsEnabled = false;
            //}
            //// khi người dùng nhấn nút sửa
            //else if (flag == 2)
            //{
            //    btnThem.IsEnabled = false;
            //    btnSua.IsEnabled = true;
            //}
            //// là khi người dùng bấm nút xem chi tiết
            //else
            //{
            //    btnThem.IsEnabled = false;
            //    isEnabledThongTin(false);
            //}
            //if (nhanVien != null)
            //{
            //    nhanVienSelect = nhanVien;
            //}
            hienThiThongTin(phieuNhapNguyenLieuSelect);
        }

        private void hienThiThongTin(PhieuNhapNguyenLieu phieuNhap)
        {
            dateNgayNhap.SelectedDate = DateTime.Now;
            txtMaNhanVien.Text = nhanVienSelect.maNhanVien;
            txtTenNhanVien.Text = nhanVienSelect.hoNhanVien + " " + nhanVienSelect.tenNhanVien;
            cmbTenNguyenLieu.ItemsSource = CNguyenLieu_BUS.toListTen();

            hienThiDSChiTietPhieuNhap(phieuNhap.ChiTietPhieuNhapNguyenLieux.ToList());
        }

        private void isEnabledThongTin(bool value)
        {
            dateNgayNhap.IsEnabled = value;
            cmbTenNguyenLieu.IsEnabled = value;
            txtSoLuong.IsEnabled = value;
            txtDonGia.IsEnabled = value;
            dateNgayHetHan.IsEnabled = value;
            cmbDonViTinh.IsEnabled = value;
        }

        public void hienThiDSChiTietPhieuNhap(List<ChiTietPhieuNhapNguyenLieu> list)
        {
            if (list.Count() > 0)
            {
                dgDSChiTietPhieuNhap.ItemsSource = list.Select(x => new
                {
                    maPhieuNhap = x.maPhieuNhap,
                    maNguyenLieu = x.maNguyenLieu,
                    tenNguyenLieu = CNguyenLieu_BUS.find(x.maNguyenLieu).tenNguyenLieu,
                    ngayHetHan = x.ChiTietNguyenLieu.ngayHetHan.Value.ToString("dd/MM/yyyy"),
                    donViTinh = x.ChiTietNguyenLieu.donViTinh,
                    soLuong = x.soLuong,
                    donGia = x.ChiTietNguyenLieu.donGia,
                    thanhTien = x.thanhTien
                });
            }
        }

        private double tinhTongThanhTien(List<ChiTietPhieuNhapNguyenLieu> list)
        {
            double tongThanhTien = 0;
            list.ForEach(x => tongThanhTien += x.thanhTien.Value);
            return tongThanhTien;
        }

        private void dgDSChiTietPhieuNhap_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnTaoPhieuNhap_Click(object sender, RoutedEventArgs e)
        {
            if (chiTietPhieuNhapNguyenLieus.Count() == 0)
            {
                MessageBox.Show("Không thể tạo phiếu nhập rỗng");
            }
            else
            {
                PhieuNhapNguyenLieu phieuNhapNguyenLieu = new PhieuNhapNguyenLieu();
                phieuNhapNguyenLieu.maPhieuNhap = chiTietPhieuNhapNguyenLieus[0].maPhieuNhap;
                phieuNhapNguyenLieu.maNhanVien = nhanVienSelect.maNhanVien;
                phieuNhapNguyenLieu.ngayNhap = chiTietPhieuNhapNguyenLieus[0].ChiTietNguyenLieu.ngayNhap.Value;
                phieuNhapNguyenLieu.tongThanhTien = double.Parse(txtTongThanhTien.Text);
                phieuNhapNguyenLieu.trangThai = 0;

                chiTietPhieuNhapNguyenLieus.ForEach(x =>
                {
                    ChiTietPhieuNhapNguyenLieu chiTiet = new ChiTietPhieuNhapNguyenLieu();
                    chiTiet.maPhieuNhap = x.maPhieuNhap;
                    chiTiet.maNguyenLieu = x.maNguyenLieu;
                    chiTiet.soLuong = x.soLuong;
                    chiTiet.thanhTien = x.thanhTien;
                    //chiTiet = x;

                    chiTiet.ChiTietNguyenLieu = new ChiTietNguyenLieu();
                    chiTiet.ChiTietNguyenLieu = x.ChiTietNguyenLieu;

                    phieuNhapNguyenLieu.ChiTietPhieuNhapNguyenLieux.Add(chiTiet);
                });

                if (CPhieuNhapNguyenLieu_BUS.add(phieuNhapNguyenLieu))
                {
                    MessageBox.Show("Thêm thành công");
                    this.Close();
                }
            }
        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dateNgayNhap.SelectedDate.Value != null &&
                    dateNgayHetHan.SelectedDate.Value != null &&
                    cmbTenNguyenLieu.SelectedItem.ToString() != null)
                {
                    string tenNguyenLieu = cmbTenNguyenLieu.SelectedItem.ToString();
                    NguyenLieu nguyenLieu = CNguyenLieu_BUS.findNguyenLieuByTen(tenNguyenLieu);
                    if (nguyenLieu != null)
                    {
                        ChiTietPhieuNhapNguyenLieu chiTiet = new ChiTietPhieuNhapNguyenLieu();
                        chiTiet.maNguyenLieu = nguyenLieu.maNguyenLieu;
                        chiTiet.maPhieuNhap = txtMaPhieuNhap.Text;
                        chiTiet.soLuong = int.Parse(txtSoLuong.Text);
                        chiTiet.thanhTien = double.Parse(txtThanhTien.Text);

                        chiTiet.ChiTietNguyenLieu = new ChiTietNguyenLieu();
                        chiTiet.ChiTietNguyenLieu.maNguyenLieu = nguyenLieu.maNguyenLieu;
                        chiTiet.ChiTietNguyenLieu.ngayHetHan = dateNgayHetHan.SelectedDate.Value;
                        chiTiet.ChiTietNguyenLieu.ngayNhap = dateNgayNhap.SelectedDate.Value;
                        chiTiet.ChiTietNguyenLieu.soLuong = int.Parse(txtSoLuong.Text);
                        chiTiet.ChiTietNguyenLieu.donGia = double.Parse(txtDonGia.Text);
                        chiTiet.ChiTietNguyenLieu.donViTinh = cmbDonViTinh.Text;

                        chiTietPhieuNhapNguyenLieus.Add(chiTiet);
                        txtTongThanhTien.Text = tinhTongThanhTien(chiTietPhieuNhapNguyenLieus).ToString();

                        hienThiDSChiTietPhieuNhap(chiTietPhieuNhapNguyenLieus);
                    }
                }
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Lỗi! Không được để dữ liệu nhập vào là rỗng");
            }
        }

        private void txtDonGia_KeyUp(object sender, KeyEventArgs e)
        {
            int soLuong;
            double donGia;
            double thanhTien = 0;
            if (txtSoLuong.Text != null && txtSoLuong.Text != "")
            {
                soLuong = int.Parse(txtSoLuong.Text);
            }
            else
            {
                soLuong = 0;
            }
            if (txtDonGia.Text != null && txtDonGia.Text != "")
            {
                donGia = double.Parse(txtDonGia.Text);
            }
            else
            {
                donGia = 0;
            }
            thanhTien = soLuong * donGia;
            txtThanhTien.Text = thanhTien.ToString();
        }

        private void txtSoLuong_KeyUp(object sender, KeyEventArgs e)
        {
            int soLuong;
            double donGia;
            double thanhTien = 0;
            if (txtSoLuong.Text != null && txtSoLuong.Text != "")
            {
                soLuong = int.Parse(txtSoLuong.Text);
            }
            else
            {
                soLuong = 0;
            }
            if (txtDonGia.Text != null && txtDonGia.Text != "")
            {
                donGia = double.Parse(txtDonGia.Text);
            }
            else
            {
                donGia = 0;
            }
            thanhTien = soLuong * donGia;
            txtThanhTien.Text = thanhTien.ToString();
        }
    }
}
