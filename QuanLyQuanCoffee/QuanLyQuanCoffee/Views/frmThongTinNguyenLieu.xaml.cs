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
                btnResest.IsEnabled = false;
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

        //private void CommandBinding_Executed_ThemNguyenLieu(object sender, ExecutedRoutedEventArgs e)
        //{
        //    string maNguyenLieu = "";
        //    do
        //    {
        //        maNguyenLieu = CNhanVien_BUS.randomMaNhanVien();
        //    } while (CNguyenLieu_BUS.find(maNguyenLieu) != null);

        //    NguyenLieu nguyenLieu = new NguyenLieu(
        //        maNguyenLieu,
        //        txtTenNguyenLieu.Text,
        //        double.Parse(txtDonGia.Text),
        //        int.Parse(txtSoLuong.Text),
        //        txtDonViTinh.Text,
        //        dateNgayHetHan.SelectedDate.Value,
        //        dateNgayNhap.SelectedDate.Value,
        //        CLoaiNguyenLieu_BUS.findMaLoaibyTenLoai(cmbLoaiNguyenLieu.SelectedItem.ToString())
        //        );
        //    CNguyenLieu_BUS.add(nguyenLieu);
        //    MessageBox.Show("Thêm thành công");
        //    this.Close();
        //}

        //private void CommandBinding_CanExecute_ThemNguyenLieu(object sender, CanExecuteRoutedEventArgs e)
        //{
        //    e.CanExecute = true;
        //}

        //private void CommandBinding_Executed_SuaNguyenLieu(object sender, ExecutedRoutedEventArgs e)
        //{
        //    NguyenLieu nguyenLieu = new NguyenLieu(
        //        txtMaNguyenLieu.Text,
        //        txtTenNguyenLieu.Text,
        //        double.Parse(txtDonGia.Text),
        //        int.Parse(txtSoLuong.Text),
        //        txtDonViTinh.Text,
        //        dateNgayHetHan.SelectedDate.Value,
        //        dateNgayNhap.SelectedDate.Value,
        //        CLoaiNguyenLieu_BUS.findMaLoaibyTenLoai(cmbLoaiNguyenLieu.SelectedItem.ToString())
        //        );
        //    if (CNguyenLieu_BUS.edit(nguyenLieu))
        //    {
        //        MessageBox.Show("Sửa thành công");
        //        this.Close();
        //    }
        //    else
        //    {
        //        MessageBox.Show("Sửa không thành công");
        //    }
        //}

        //private void CommandBinding_CanExecute_SuaNguyenLieu(object sender, CanExecuteRoutedEventArgs e)
        //{
        //    e.CanExecute = true;
        //}

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            btnLuu.IsEnabled = true;
            isEnabledThongTin(true);
        }

        private void btnResest_Click(object sender, RoutedEventArgs e)
        {
            if (NguyenLieuSelect != null)
            {
                hienThiThongTin(NguyenLieuSelect);
            }
            else
            {
                hienThiThongTin(new NguyenLieu());
            }
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            string maNguyenLieu = "";

            // chưa xét trường hợp nếu các mã tạo ra đã trùng hết thì sẽ làm như nào
            do
            {
                maNguyenLieu = CServices.randomMa();
            } while (CNguyenLieu_BUS.find(maNguyenLieu) != null);

            NguyenLieu nguyenLieu = new NguyenLieu(
                            maNguyenLieu,
                            txtTenNguyenLieu.Text,
                            double.Parse(txtDonGia.Text),
                            int.Parse(txtSoLuong.Text),
                            txtDonViTinh.Text,
                            dateNgayHetHan.SelectedDate.Value,
                            dateNgayNhap.SelectedDate.Value,
                            CLoaiNguyenLieu_BUS.findMaLoaibyTenLoai(cmbLoaiNguyenLieu.SelectedItem.ToString())
                            );
            if (CNguyenLieu_BUS.add(nguyenLieu))
            {
                MessageBox.Show("Thêm thành công!");
                this.Close();
            }
            else
            {
                MessageBox.Show("Thông tin không hợp lệ, Vui lòng kiểm tra lại!");
            }
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            NguyenLieu nguyenLieu = new NguyenLieu(
                            txtMaNguyenLieu.Text,
                            txtTenNguyenLieu.Text,
                            double.Parse(txtDonGia.Text),
                            int.Parse(txtSoLuong.Text),
                            txtDonViTinh.Text,
                            dateNgayHetHan.SelectedDate.Value.Date,
                            dateNgayNhap.SelectedDate.Value.Date,
                            CLoaiNguyenLieu_BUS.findMaLoaibyTenLoai(cmbLoaiNguyenLieu.SelectedItem.ToString())
                            );

            if (CNguyenLieu_BUS.edit(nguyenLieu))
            {
                MessageBox.Show("Sửa thành công");
                this.Close();
            }
            else
            {
                MessageBox.Show("Sửa không thành công");
            }
        }
    }
}
