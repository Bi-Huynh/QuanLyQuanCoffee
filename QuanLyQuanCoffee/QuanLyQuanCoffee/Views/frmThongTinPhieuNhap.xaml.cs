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
        private ChiTietPhieuNhap ChiTietPhieuNhapSelect;
        private List<ChiTietPhieuNhap> chiTietPhieuNhaps;
        private List<ChiTietNguyenLieu> chiTietNguyenLieus;

        public frmThongTinPhieuNhap(NhanVien nhanVien = null, PhieuNhapNguyenLieu phieuNhapNguyenLieu = null, int flag = 1)
        {
            InitializeComponent();
            nhanVienSelect = nhanVien;
            chiTietNguyenLieus = CChiTietNguyenLieu_BUS.toList();
            phieuNhapNguyenLieuSelect = phieuNhapNguyenLieu;
            chiTietPhieuNhaps = new List<ChiTietPhieuNhap>();

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
            txtMaNhanVien.Text = phieuNhap.NhanVien.maNhanVien;
            txtTenNhanVien.Text = phieuNhap.NhanVien.hoNhanVien + " " + phieuNhap.NhanVien.tenNhanVien;
            cmbTenNguyenLieu.ItemsSource = CNguyenLieu_BUS.toListTen();

            chiTietPhieuNhaps = CChiTietPhieuNhapNguyenLieu_BUS.toList(phieuNhap.maPhieuNhap);

            txtTongThanhTien.Text = phieuNhap.tongThanhTien.ToString();

            hienThiDSChiTietPhieuNhap(chiTietPhieuNhaps);
        }

        private void hienThiThongTin(ChiTietPhieuNhap chiTietPhieuNhap)
        {
            txtMaChiTietNguyenLieu.Text = chiTietPhieuNhap.maChitietNguyenLieu
                .Substring(0, chiTietPhieuNhap.maChitietNguyenLieu.Length - 10);
            cmbTenNguyenLieu.SelectedItem = chiTietPhieuNhap.ChiTietNguyenLieu.NguyenLieu.tenNguyenLieu.Trim();
            dateNgayHetHan.SelectedDate = chiTietPhieuNhap.ChiTietNguyenLieu.ngayHetHan;
            cmbDonViTinh.SelectedItem = chiTietPhieuNhap.ChiTietNguyenLieu.donViTinh.Trim();
            txtSoLuong.Text = chiTietPhieuNhap.soLuong.ToString();
            txtDonGia.Text = chiTietPhieuNhap.donGia.ToString();
            txtThanhTien.Text = chiTietPhieuNhap.thanhTien.ToString();
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

        public void hienThiDSChiTietPhieuNhap(List<ChiTietPhieuNhap> list)
        {
            if (list.Count() > 0)
            {
                try
                {
                    dgDSChiTietPhieuNhap.ItemsSource = list.Select(x => new
                    {
                        maChiTietPhieuNhap = x.maChiTietPhieuNhap,
                        maChiTietNguyenLieu = x.maChitietNguyenLieu,//.Substring(0, x.maChitietNguyenLieu.Length - 10),
                        tenNguyenLieu = CNguyenLieu_BUS.find(x.ChiTietNguyenLieu.maNguyenLieu).tenNguyenLieu,
                        ngayHetHan = x.ChiTietNguyenLieu.ngayHetHan.Value.ToString("dd/MM/yyyy"),
                        donViTinh = x.ChiTietNguyenLieu.donViTinh,
                        soLuong = x.soLuong,
                        donGia = x.donGia,
                        thanhTien = x.thanhTien
                    });
                }
                catch (ArgumentNullException)
                {
                    MessageBox.Show("Chi tiết phiếu nhập rỗng");
                }
            }
        }

        private double tinhTongThanhTien(List<ChiTietPhieuNhap> list)
        {
            double tongThanhTien = 0;
            list.ForEach(x => tongThanhTien += x.thanhTien.Value);
            return tongThanhTien;
        }

        private void dgDSChiTietPhieuNhap_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgDSChiTietPhieuNhap.SelectedItem != null)
            {
                int index = dgDSChiTietPhieuNhap.SelectedIndex;
                ChiTietPhieuNhapSelect = chiTietPhieuNhaps.ElementAt(index);

                hienThiThongTin(ChiTietPhieuNhapSelect);
            }
        }

        private void btnTaoPhieuNhap_Click(object sender, RoutedEventArgs e)
        {
            if (chiTietPhieuNhaps.Count() == 0)
            {
                MessageBox.Show("Không thể tạo phiếu nhập rỗng");
            }
            else
            {
                PhieuNhapNguyenLieu phieuNhapNguyenLieu = new PhieuNhapNguyenLieu();
                phieuNhapNguyenLieu.maPhieuNhap = txtMaPhieuNhap.Text;
                phieuNhapNguyenLieu.maNhanVien = nhanVienSelect.maNhanVien;
                phieuNhapNguyenLieu.ngayNhap = dateNgayNhap.SelectedDate.Value;
                phieuNhapNguyenLieu.tongThanhTien = double.Parse(txtTongThanhTien.Text);
                phieuNhapNguyenLieu.trangThai = 0;

                if (!CPhieuNhapNguyenLieu_BUS.add(phieuNhapNguyenLieu))
                {
                    MessageBox.Show("Lỗi! Thêm Phiếu Nhập không thành công");
                    //this.Close();
                    return;
                }

                foreach (var x in chiTietPhieuNhaps)
                {
                    ChiTietNguyenLieu chiTietNguyenLieu = new ChiTietNguyenLieu();
                    chiTietNguyenLieu.maChiTietNguyenLieu = x.maChitietNguyenLieu;
                    chiTietNguyenLieu.maNguyenLieu = x.ChiTietNguyenLieu.maNguyenLieu;
                    chiTietNguyenLieu.ngayHetHan = x.ChiTietNguyenLieu.ngayHetHan.Value;
                    chiTietNguyenLieu.soLuong = x.soLuong;
                    chiTietNguyenLieu.donViTinh = x.ChiTietNguyenLieu.donViTinh;

                    if (!CChiTietNguyenLieu_BUS.add(chiTietNguyenLieu))
                    {
                        MessageBox.Show("Lỗi, không thêm chi tiết nguyên liệu không thành công");
                        return;
                    }

                    ChiTietPhieuNhap chiTiet = new ChiTietPhieuNhap();
                    if (true)
                    {

                    }
                    chiTiet.maChiTietPhieuNhap = CServices.taoMa<ChiTietPhieuNhap>(CChiTietPhieuNhapNguyenLieu_BUS.toList());
                    chiTiet.maPhieuNhap = x.maPhieuNhap;
                    chiTiet.maChitietNguyenLieu = x.maChitietNguyenLieu;
                    chiTiet.soLuong = x.soLuong;
                    chiTiet.donGia = x.donGia;
                    chiTiet.thanhTien = x.thanhTien;

                    if (!CChiTietPhieuNhapNguyenLieu_BUS.add(chiTiet))
                    {
                        MessageBox.Show("Lỗi, không thêm chi tiết phiếu nhập không thành công");
                        return;
                    }
                }
                chiTietPhieuNhaps = new List<ChiTietPhieuNhap>();
                MessageBox.Show("Thêm thành công");
                this.Close();
            }
        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            if (chiTietPhieuNhaps.Count() > 0)
            {
                string tenNguyenLieu = cmbTenNguyenLieu.SelectedItem.ToString();
                NguyenLieu nguyenLieu = CNguyenLieu_BUS.findNguyenLieuByTen(tenNguyenLieu);

                ChiTietPhieuNhap chiTietPhieuNhap = chiTietPhieuNhaps.ElementAt(dgDSChiTietPhieuNhap.SelectedIndex);
                chiTietPhieuNhap.maChiTietPhieuNhap = ChiTietPhieuNhapSelect.maChiTietPhieuNhap;

                string maCTNL = ChiTietPhieuNhapSelect.maChitietNguyenLieu;
                chiTietPhieuNhap.maChitietNguyenLieu = txtMaChiTietNguyenLieu.Text + maCTNL.Substring(maCTNL.Length - 10);
                chiTietPhieuNhap.soLuong = int.Parse(txtSoLuong.Text);
                chiTietPhieuNhap.donGia = double.Parse(txtDonGia.Text);
                chiTietPhieuNhap.thanhTien = double.Parse(txtThanhTien.Text);
                chiTietPhieuNhap.maPhieuNhap = txtMaPhieuNhap.Text;

                ChiTietNguyenLieu chiTietNguyenLieu = new ChiTietNguyenLieu();
                chiTietNguyenLieu.maChiTietNguyenLieu = chiTietPhieuNhap.maChitietNguyenLieu;
                chiTietNguyenLieu.maNguyenLieu = nguyenLieu.maNguyenLieu;
                chiTietNguyenLieu.ngayHetHan = dateNgayHetHan.SelectedDate.Value;
                chiTietNguyenLieu.soLuong = chiTietPhieuNhap.soLuong;
                chiTietNguyenLieu.donViTinh = cmbDonViTinh.Text;

                chiTietPhieuNhap.ChiTietNguyenLieu = chiTietNguyenLieu;

                ChiTietPhieuNhapSelect = chiTietPhieuNhap;
                txtTongThanhTien.Text = tinhTongThanhTien(chiTietPhieuNhaps).ToString();
                hienThiDSChiTietPhieuNhap(chiTietPhieuNhaps);
            }
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            if (ChiTietPhieuNhapSelect != null)
            {
                chiTietPhieuNhaps.Remove(ChiTietPhieuNhapSelect);
                hienThiDSChiTietPhieuNhap(chiTietPhieuNhaps);
            }
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
                        ChiTietPhieuNhap chiTiet = new ChiTietPhieuNhap();

                        // tạo mã chi tiết phiếu nhập
                        var list1 = CChiTietPhieuNhapNguyenLieu_BUS.toList();
                        if (list1.Count() == 0)
                        {
                            chiTiet.maChiTietPhieuNhap = CServices.taoMa<ChiTietPhieuNhap>(chiTietPhieuNhaps);
                        }
                        else
                        {
                            chiTiet.maChiTietPhieuNhap = CServices.taoMa<ChiTietPhieuNhap>(list1);
                        }

                        // tạo mã chi tiết nguyên liệu
                        if (chiTietNguyenLieus.Count() == 0)
                        {
                            List<ChiTietNguyenLieu> temp = new List<ChiTietNguyenLieu>();
                            foreach (var item in chiTietPhieuNhaps)
                            {
                                temp.Add(item.ChiTietNguyenLieu);
                            }
                            chiTiet.maChitietNguyenLieu = CChiTietNguyenLieu_BUS
                                .taoMa(txtMaChiTietNguyenLieu.Text, temp);
                        }
                        else
                        {
                            chiTiet.maChitietNguyenLieu = CChiTietNguyenLieu_BUS
                                .taoMa(txtMaChiTietNguyenLieu.Text, chiTietNguyenLieus);
                            chiTietNguyenLieus = new List<ChiTietNguyenLieu>();
                        }
                        chiTiet.maPhieuNhap = txtMaPhieuNhap.Text;
                        chiTiet.soLuong = int.Parse(txtSoLuong.Text);
                        chiTiet.donGia = double.Parse(txtDonGia.Text);
                        chiTiet.thanhTien = double.Parse(txtThanhTien.Text);

                        ChiTietNguyenLieu chiTietNguyenLieu = new ChiTietNguyenLieu();
                        chiTietNguyenLieu.maChiTietNguyenLieu = chiTiet.maChitietNguyenLieu;
                        chiTietNguyenLieu.maNguyenLieu = nguyenLieu.maNguyenLieu;
                        chiTietNguyenLieu.ngayHetHan = dateNgayHetHan.SelectedDate.Value;
                        chiTietNguyenLieu.soLuong = chiTiet.soLuong;
                        chiTietNguyenLieu.donViTinh = cmbDonViTinh.Text;

                        chiTiet.ChiTietNguyenLieu = chiTietNguyenLieu;

                        chiTietPhieuNhaps.Add(chiTiet);
                        txtTongThanhTien.Text = tinhTongThanhTien(chiTietPhieuNhaps).ToString();

                        hienThiDSChiTietPhieuNhap(chiTietPhieuNhaps);
                    }
                }
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Lỗi! Không được để dữ liệu nhập vào là rỗng");
            }
            catch (FormatException)
            {
                MessageBox.Show("Lỗi! Số lượng và Đơn giá phải là số");
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Lỗi! Không được để dữ liệu nhập vào là rỗng");
            }
            catch (OverflowException)
            {
                MessageBox.Show("Lỗi! Dữ liệu nhập vào vượt quá giới hạn cho phép");
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
