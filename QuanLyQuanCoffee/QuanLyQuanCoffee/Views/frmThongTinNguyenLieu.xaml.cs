using QuanLyQuanCoffee.BUS;
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

        public frmThongTinNguyenLieu(NguyenLieu nguyenLieu = null)
        {
            InitializeComponent();
            hienThi();
            NguyenLieuSelect = nguyenLieu;
            if (NguyenLieuSelect != null)
            {
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

        private void CommandBinding_Executed_ThemNguyenLieu(object sender, ExecutedRoutedEventArgs e)
        {
            string maNguyenLieu = "";
            do
            {
                maNguyenLieu = CNhanVien_BUS.randomMaNhanVien();
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
            CNguyenLieu_BUS.add(nguyenLieu);
            MessageBox.Show("Thêm thành công");
            this.Close();
        }

        private void CommandBinding_CanExecute_ThemNguyenLieu(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBinding_Executed_SuaNguyenLieu(object sender, ExecutedRoutedEventArgs e)
        {
            NguyenLieu nguyenLieu = new NguyenLieu(
                txtMaNguyenLieu.Text,
                txtTenNguyenLieu.Text,
                double.Parse(txtDonGia.Text),
                int.Parse(txtSoLuong.Text),
                txtDonViTinh.Text,
                dateNgayHetHan.SelectedDate.Value,
                dateNgayNhap.SelectedDate.Value,
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

        private void CommandBinding_CanExecute_SuaNguyenLieu(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {

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
    }
}
