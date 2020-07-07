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
        private List<ChiTietPhieuNhap> list1;
        private List<string> donViTinhs;
        private int flat;

        public frmThongTinPhieuNhap(NhanVien nhanVien = null, PhieuNhapNguyenLieu phieuNhapNguyenLieu = null, int flag = 1)
        {
            InitializeComponent();
            nhanVienSelect = nhanVien;
            chiTietNguyenLieus = CChiTietNguyenLieu_BUS.toList();
            list1 = CChiTietPhieuNhapNguyenLieu_BUS.toList();
            phieuNhapNguyenLieuSelect = phieuNhapNguyenLieu;
            chiTietPhieuNhaps = new List<ChiTietPhieuNhap>();

            donViTinhs = new List<string>();

            if (nhanVienSelect == null)
            {
                nhanVienSelect = new NhanVien();
            }
            if (phieuNhapNguyenLieuSelect == null)
            {
                phieuNhapNguyenLieuSelect = new PhieuNhapNguyenLieu();
            }

            txtMaPhieuNhap.Text = CServices.taoMa<PhieuNhapNguyenLieu>(CPhieuNhapNguyenLieu_BUS.toListAll());
            donViTinhs.Add("Kg");
            donViTinhs.Add("Gam");
            donViTinhs.Add("Lon");
            donViTinhs.Add("Chai");
            donViTinhs.Add("Lit");
            cmbDonViTinh.ItemsSource = donViTinhs;


            if (flag == 1)
            {
                btnSua.IsEnabled = false;
                btnXoa.IsEnabled = false;
                flat = 1;
            }
            // là khi người dùng bấm nút xem chi tiết
            else
            {
                btnThem.IsEnabled = false;
                btnSua.IsEnabled = false;
                btnXoa.IsEnabled = false;
                btnTaoPhieuNhap.IsEnabled = false;
                flat = 0;
                isEnabledThongTin(false);
            }
            hienThiThongTin(phieuNhapNguyenLieuSelect);
        }

        private void hienThiThongTin(PhieuNhapNguyenLieu phieuNhap)
        {
            dateNgayNhap.SelectedDate = DateTime.Now;
            if (phieuNhap.NhanVien == null)
            {
                txtMaNhanVien.Text = nhanVienSelect.maNhanVien;
                txtTenNhanVien.Text = nhanVienSelect.hoNhanVien + " " + nhanVienSelect.tenNhanVien;
            }
            else
            {
                txtMaNhanVien.Text = phieuNhap.NhanVien.maNhanVien;
                txtTenNhanVien.Text = phieuNhap.NhanVien.hoNhanVien + " " + phieuNhap.NhanVien.tenNhanVien;
            }
            cmbTenNguyenLieu.ItemsSource = CNguyenLieu_BUS.toListTen();

            chiTietPhieuNhaps = CChiTietPhieuNhapNguyenLieu_BUS.toList(phieuNhap.maPhieuNhap);

            txtTongThanhTien.Text = String.Format("{0:#,###,0 VND;(#,###,0 VND);0 VND}", phieuNhap.tongThanhTien);

            hienThiDSChiTietPhieuNhap(chiTietPhieuNhaps);
        }

        private void hienThiThongTin(ChiTietPhieuNhap chiTietPhieuNhap)
        {
            if (chiTietPhieuNhap.ChiTietNguyenLieu == null)
            {
                ChiTietNguyenLieu chiTietNguyenLieu = CChiTietNguyenLieu_BUS.findCT(chiTietPhieuNhap.maChitietNguyenLieu);
                cmbTenNguyenLieu.SelectedItem = chiTietNguyenLieu.maNguyenLieu;
                dateNgayHetHan.SelectedDate = chiTietNguyenLieu.ngayHetHan;
                cmbDonViTinh.SelectedItem = chiTietNguyenLieu.donViTinh.Trim();
            }
            else
            {
                cmbTenNguyenLieu.SelectedItem = CNguyenLieu_BUS.findTenNguyenLieu(chiTietPhieuNhap.ChiTietNguyenLieu.maNguyenLieu);
                dateNgayHetHan.SelectedDate = chiTietPhieuNhap.ChiTietNguyenLieu.ngayHetHan;
                cmbDonViTinh.SelectedItem = chiTietPhieuNhap.ChiTietNguyenLieu.donViTinh.Trim();
            }
            txtMaChiTietNguyenLieu.Text = chiTietPhieuNhap.maChitietNguyenLieu.Substring(10);
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
                    foreach (var item in list)
                    {
                        if (item.ChiTietNguyenLieu == null)
                        {
                            item.ChiTietNguyenLieu = CChiTietNguyenLieu_BUS.findCT(item.maChitietNguyenLieu);
                        }
                    }

                    dgDSChiTietPhieuNhap.ItemsSource = list.Select(x => new
                    {
                        maChiTietPhieuNhap = x.maChiTietPhieuNhap,
                        maChiTietNguyenLieu = x.maChitietNguyenLieu.Substring(10),
                        tenNguyenLieu = CNguyenLieu_BUS.findTenNguyenLieu(x.ChiTietNguyenLieu.maNguyenLieu).Trim(),
                        ngayHetHan = x.ChiTietNguyenLieu.ngayHetHan.Value.ToString("dd/MM/yyyy"),
                        donViTinh = x.ChiTietNguyenLieu.donViTinh,
                        soLuong = x.soLuong,
                        donGia = String.Format("{0:#,###,0 VND;(#,###,0 VND);0 VND}", x.donGia),
                        thanhTien = String.Format("{0:#,###,0 VND;(#,###,0 VND);0 VND}", x.thanhTien)
                    }) ;
                }
                catch (ArgumentNullException)
                {
                    MessageBox.Show("Chi tiết phiếu nhập rỗng");
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("Chi tiết null");
                }
            }
        }

        void hienThiDefault()
        {
            txtMaChiTietNguyenLieu.Text = "";
            cmbTenNguyenLieu.SelectedIndex = 0;
            dateNgayHetHan.SelectedDate = null;
            cmbDonViTinh.SelectedIndex = 0;
            txtSoLuong.Text = "";
            txtDonGia.Text = "";
            txtThanhTien.Text = "";
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
                ChiTietPhieuNhapSelect = new ChiTietPhieuNhap();
                ChiTietPhieuNhapSelect = chiTietPhieuNhaps[index] as ChiTietPhieuNhap;

                if (flat == 1)
                {
                    hienThiThongTin(ChiTietPhieuNhapSelect);
                    btnXoa.IsEnabled = true;
                    btnSua.IsEnabled = true;
                }
            }
        }

        private void btnTaoPhieuNhap_Click(object sender, RoutedEventArgs e)
        {
            int flag = 0;
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

                if (CPhieuNhapNguyenLieu_BUS.add(phieuNhapNguyenLieu))
                {
                    foreach (var x in chiTietPhieuNhaps)
                    {
                        ChiTietNguyenLieu chiTietNguyenLieu = new ChiTietNguyenLieu();
                        chiTietNguyenLieu.maChiTietNguyenLieu = x.maChitietNguyenLieu;
                        chiTietNguyenLieu.maNguyenLieu = x.ChiTietNguyenLieu.maNguyenLieu;
                        chiTietNguyenLieu.ngayHetHan = x.ChiTietNguyenLieu.ngayHetHan.Value;
                        chiTietNguyenLieu.soLuong = x.soLuong;
                        chiTietNguyenLieu.donViTinh = x.ChiTietNguyenLieu.donViTinh;

                        if (CChiTietNguyenLieu_BUS.add(chiTietNguyenLieu))
                        {
                            ChiTietPhieuNhap chiTiet = new ChiTietPhieuNhap();
                            chiTiet.maChiTietPhieuNhap = x.maChiTietPhieuNhap;
                            chiTiet.maPhieuNhap = x.maPhieuNhap;
                            chiTiet.maChitietNguyenLieu = x.maChitietNguyenLieu;
                            chiTiet.soLuong = x.soLuong;
                            chiTiet.donGia = x.donGia;
                            chiTiet.thanhTien = x.thanhTien;
                            if (!CChiTietPhieuNhapNguyenLieu_BUS.add(chiTiet))
                            {
                                flag = 1;
                            }
                        }
                        else
                        {
                            flag = 1;
                        }
                    }
                }
                else
                {
                    flag = 1;
                }

                if (flag == 1)
                {
                    rollback(chiTietPhieuNhaps);
                }
                MessageBox.Show("Thêm thành công");
                this.Close();
            }
        }

        private static bool rollback(List<ChiTietPhieuNhap> list)
        {
            foreach (var item in list)
            {
                if (!CChiTietNguyenLieu_BUS.remove(item.maChitietNguyenLieu))
                {
                    MessageBox.Show("Lỗi rollback, xóa chi tiết nguyên liệu");
                    return false;
                }
            }
            foreach (var item in list)
            {
                if (!CChiTietPhieuNhapNguyenLieu_BUS.remove(item.maChiTietPhieuNhap))
                {
                    MessageBox.Show("Lỗi rollback, xóa chi tiết phiêu nhập");
                    return false;
                }
            }
            if (CPhieuNhapNguyenLieu_BUS.remove(list[0].maPhieuNhap))
            {
                MessageBox.Show("Lỗi rollback, xóa phiếu nhập");
                return false;
            }

            return true;
        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            if (ChiTietPhieuNhapSelect != null)
            {
                try
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
                    hienThiDefault();
                }
                catch (InvalidOperationException)
                {
                    MessageBox.Show("Dữ liệu không được để rỗng");
                }
                catch (ArgumentNullException)
                {
                    MessageBox.Show("Dữ liệu không được để rỗng");
                }
                catch (FormatException)
                {
                    MessageBox.Show("Dữ liệu phải là số");
                }
                catch (OverflowException)
                {
                    MessageBox.Show("Dữ liệu có độ lớn vượt quá giới hạn cho phép");
                }
                btnSua.IsEnabled = false;
                btnXoa.IsEnabled = false;
            }
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            if (ChiTietPhieuNhapSelect != null)
            {
                chiTietPhieuNhaps.Remove(ChiTietPhieuNhapSelect);

                btnSua.IsEnabled = false;
                btnXoa.IsEnabled = false;
                hienThiDSChiTietPhieuNhap(chiTietPhieuNhaps);
                hienThiDefault();
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
                        if (list1.Count() == 0)
                        {
                            chiTiet.maChiTietPhieuNhap = CServices.taoMa<ChiTietPhieuNhap>(chiTietPhieuNhaps);
                        }
                        else
                        {
                            chiTiet.maChiTietPhieuNhap = CServices.taoMa<ChiTietPhieuNhap>(list1);
                            list1 = new List<ChiTietPhieuNhap>();
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
                        if (CServices.kiemTraThongTin(chiTiet))
                        {
                            chiTietPhieuNhaps.Add(chiTiet);
                            txtTongThanhTien.Text = tinhTongThanhTien(chiTietPhieuNhaps).ToString();

                            hienThiDSChiTietPhieuNhap(chiTietPhieuNhaps);
                            hienThiDefault();
                        }
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
