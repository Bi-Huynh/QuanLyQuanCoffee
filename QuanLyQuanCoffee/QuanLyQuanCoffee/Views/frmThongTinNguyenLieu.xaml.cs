using QuanLyQuanCoffee.BUS;
using QuanLyQuanCoffee.Services;
using System;
using System.CodeDom.Compiler;
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
            cmbLoaiNguyenLieu.ItemsSource = CLoaiNguyenLieu_BUS.toListTenLoai();
        }

        private void hienThiThongTin(NguyenLieu nguyenLieu)
        {
            txtMaNguyenLieu.Text = nguyenLieu.maNguyenLieu;
            txtTenNguyenLieu.Text = nguyenLieu.tenNguyenLieu;
            cmbLoaiNguyenLieu.SelectedItem = nguyenLieu.LoaiNguyenLieu.tenLoaiNguyenLieu;
            txtMaLoai.Text = nguyenLieu.LoaiNguyenLieu.maLoaiNguyenLieu;
        }

        private void isEnabledThongTin(bool value)
        {
            txtTenNguyenLieu.IsEnabled = value;
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
                //string maLoaiNguyenLieu = CLoaiNguyenLieu_BUS.findMaLoaibyTenLoai(cmbLoaiNguyenLieu.SelectedItem.ToString());
                NguyenLieu nguyenLieu = new NguyenLieu();
                nguyenLieu.maNguyenLieu = txtMaNguyenLieu.Text;
                if (txtTenNguyenLieu.Text == null || txtTenNguyenLieu.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập tên nguyên liệu");
                    return;
                }
                nguyenLieu.tenNguyenLieu = txtTenNguyenLieu.Text;
                if (txtMaLoai.Text == null || txtMaLoai.Text == "")
                {
                    MessageBox.Show("Vui lòng chọn loại nguyên liệu");
                    return;
                }
                nguyenLieu.maLoaiNguyenLieu = txtMaLoai.Text;
                nguyenLieu.trangThai = 0;

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
                //string maLoaiNguyenLieu = CLoaiNguyenLieu_BUS.findMaLoaibyTenLoai(cmbLoaiNguyenLieu.SelectedItem.ToString());
                NguyenLieu nguyenLieu = new NguyenLieu();
                nguyenLieu.maNguyenLieu = txtMaNguyenLieu.Text;
                nguyenLieu.tenNguyenLieu = txtTenNguyenLieu.Text;
                nguyenLieu.maLoaiNguyenLieu = txtMaLoai.Text;
                nguyenLieu.trangThai = 0;

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

        private void cmbLoaiNguyenLieu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbLoaiNguyenLieu.SelectedIndex < 0)
            {
                txtMaLoai.Text = "";
            }
            else
            {
                LoaiNguyenLieu loaiNguyenLieu = CLoaiNguyenLieu_BUS.findMaLoaibyTenLoai(cmbLoaiNguyenLieu.SelectedItem.ToString());
                if (loaiNguyenLieu != null)
                {
                    txtMaLoai.Text = loaiNguyenLieu.maLoaiNguyenLieu;
                }
            }
        }
    }
}
