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
    /// Interaction logic for frmThongTinNguyenLieu.xaml
    /// </summary>
    public partial class frmThongTinNguyenLieu : Window
    {
        private NguyenLieu NguyenLieuSelect;

        public frmThongTinNguyenLieu(NguyenLieu nguyenLieu = null, int flag = 1)
        {
            InitializeComponent();
            hienThi();
            if (NguyenLieuSelect != null)
            {
                hienThiThongTin(NguyenLieuSelect);
            }
            // khi người dùng nhấn thêm thì ấn nút sửa đi
            if (flag == 1)
            {
                txtMaNguyenLieu.Text = CServices.taoMa<NguyenLieu>(CNguyenLieu_BUS.toList());
                btnSua.IsEnabled = false;
                btnLuu.IsEnabled = false;
            }
            // khi người dùng nhấn nút sửa
            else if (flag == 2)
            {
                btnThem.IsEnabled = false;
                btnSua.IsEnabled = false;
            }
            // là khi người dùng bấm nút xem chi tiết
            else
            {
                btnThem.IsEnabled = false;
                btnLuu.IsEnabled = false;
                isEnabledThongTin(false);
            }
            if (nguyenLieu != null)
            {
                NguyenLieuSelect = nguyenLieu;
                hienThiThongTin(NguyenLieuSelect);
            }
        }

        private void hienThi()
        {
            dateNgayNhap.SelectedDate = DateTime.Now;
            cmbLoaiNguyenLieu.ItemsSource = CLoaiNguyenLieu_BUS.toListTenLoai();
        }

        private void hienThiThongTin(NguyenLieu nguyenLieu)
        {
            txtMaNguyenLieu.Text = nguyenLieu.maNguyenLieu;
            txtTenNguyenLieu.Text = nguyenLieu.tenNguyenLieu;
            txtDonGia.Text = nguyenLieu.donGia.ToString();
            txtDonViTinh.Text = nguyenLieu.donViTinh;
            txtSoLuong.Text = nguyenLieu.soLuong.ToString();
            dateNgayHetHan.SelectedDate = nguyenLieu.ngayHetHan;
            dateNgayNhap.SelectedDate = nguyenLieu.ngayNhap;
            cmbLoaiNguyenLieu.SelectedItem = nguyenLieu.LoaiNguyenLieu.tenLoaiNguyenLieu;
        }

        private void isEnabledThongTin(bool value)
        {
            txtTenNguyenLieu.IsEnabled = value;
            txtDonGia.IsEnabled = value;
            txtDonViTinh.IsEnabled = value;
            txtSoLuong.IsEnabled = value;
            dateNgayHetHan.IsEnabled = value;
            dateNgayNhap.IsEnabled = value;
            cmbLoaiNguyenLieu.IsEnabled = value;
        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            btnLuu.IsEnabled = true;
            isEnabledThongTin(true);
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NguyenLieu nguyenLieu = new NguyenLieu();
                nguyenLieu.maNguyenLieu = txtMaNguyenLieu.Text;
                nguyenLieu.tenNguyenLieu = txtTenNguyenLieu.Text;
                nguyenLieu.donGia = double.Parse(txtDonGia.Text);
                nguyenLieu.soLuong = int.Parse(txtSoLuong.Text);
                nguyenLieu.donViTinh = txtDonViTinh.Text;
                nguyenLieu.ngayHetHan = dateNgayHetHan.SelectedDate.Value.Date;
                nguyenLieu.ngayNhap = dateNgayNhap.SelectedDate.Value.Date;

                if (CNguyenLieu_BUS.add(nguyenLieu))
                {
                    MessageBox.Show("Thêm thành công!");
                    this.Close();
                }
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Lỗi! Để dữ liệu rỗng");
            }
            catch (FormatException)
            {
                MessageBox.Show("Lỗi! Không đúng kiểu dữ liệu, Đơn giá và số lượng phải là số");
            }
            catch (OverflowException)
            {
                MessageBox.Show("Lỗi! Đơn giá hoặc số lượng có độ dài quá giới hạn cho phép");
            }
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NguyenLieu nguyenLieu = new NguyenLieu();
                nguyenLieu.maNguyenLieu = txtMaNguyenLieu.Text;
                nguyenLieu.tenNguyenLieu = txtTenNguyenLieu.Text;
                nguyenLieu.donGia = double.Parse(txtDonGia.Text);
                nguyenLieu.soLuong = int.Parse(txtSoLuong.Text);
                nguyenLieu.donViTinh = txtDonViTinh.Text;
                nguyenLieu.ngayHetHan = dateNgayHetHan.SelectedDate.Value.Date;
                nguyenLieu.ngayNhap = dateNgayNhap.SelectedDate.Value.Date;

                if (CNguyenLieu_BUS.edit(nguyenLieu))
                {
                    MessageBox.Show("Sửa thành công");
                    this.Close();
                }
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Lỗi! Để dữ liệu rỗng");
            }
            catch (FormatException)
            {
                MessageBox.Show("Lỗi! Không đúng kiểu dữ liệu, Đơn giá và số lượng phải là số");
            }
            catch (OverflowException)
            {
                MessageBox.Show("Lỗi! Đơn giá hoặc số lượng có độ dài quá giới hạn cho phép");
            }
        }
    }
}
