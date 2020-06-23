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
    /// Interaction logic for frmQuanLyLoaiNguyenLieu.xaml
    /// </summary>
    public partial class frmQuanLyLoaiNguyenLieu : Window
    {
        private LoaiNguyenLieu loaiNguyenLieuSeclect;

        public frmQuanLyLoaiNguyenLieu()
        {
            InitializeComponent();
            txtMaLoaiNguyenLieu.Text = CServices.taoMa<LoaiNguyenLieu>(CLoaiNguyenLieu_BUS.toListAll());
            loaiNguyenLieuSeclect = new LoaiNguyenLieu();
            hienThiDSLoaiNhanVien(CLoaiNguyenLieu_BUS.toList());
            isEnabledThongTin(false);
        }

        private void hienThiDSLoaiNhanVien(List<LoaiNguyenLieu> list)
        {
            dgDSLoaiNguyenLieu.ItemsSource = list;
        }

        private void hienThiThongTin(LoaiNguyenLieu loaiNguyenLieu)
        {
            txtMaLoaiNguyenLieu.Text = loaiNguyenLieu.maLoaiNguyenLieu;
            txtTenLoai.Text = loaiNguyenLieu.tenLoaiNguyenLieu;
        }

        private void isEnabledThongTin(bool value)
        {
            btnBoChon.IsEnabled = value;
            btnSua.IsEnabled = value;
            btnXoa.IsEnabled = value;
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            LoaiNguyenLieu loaiNguyenLieu = new LoaiNguyenLieu();
            loaiNguyenLieu.maLoaiNguyenLieu = txtMaLoaiNguyenLieu.Text;
            loaiNguyenLieu.tenLoaiNguyenLieu = txtTenLoai.Text;
            loaiNguyenLieu.trangThai = 0;

            if (CLoaiNguyenLieu_BUS.add(loaiNguyenLieu))
            {
                MessageBox.Show("Thêm thành công");
                hienThiDSLoaiNhanVien(CLoaiNguyenLieu_BUS.toList());
            }
            else
            {
                MessageBox.Show("Thêm không thành công");
            }
        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            LoaiNguyenLieu loaiNguyenLieu = new LoaiNguyenLieu();
            loaiNguyenLieu.maLoaiNguyenLieu = txtMaLoaiNguyenLieu.Text;
            loaiNguyenLieu.tenLoaiNguyenLieu = txtTenLoai.Text;
            loaiNguyenLieu.trangThai = 0;

            if (CLoaiNguyenLieu_BUS.edit(loaiNguyenLieu))
            {
                MessageBox.Show("Sửa thành công");
                hienThiDSLoaiNhanVien(CLoaiNguyenLieu_BUS.toList());
            }
            else
            {
                MessageBox.Show("Sửa không thành công");
            }
        }

        private void btnBoChon_Click(object sender, RoutedEventArgs e)
        {
            hienThiThongTin(new LoaiNguyenLieu());
            isEnabledThongTin(false);
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            if (loaiNguyenLieuSeclect != null)
            {
                if (CLoaiNguyenLieu_BUS.remove(loaiNguyenLieuSeclect))
                {
                    MessageBox.Show("Xóa thành công");
                    hienThiDSLoaiNhanVien(CLoaiNguyenLieu_BUS.toList());
                }
                else
                {
                    MessageBox.Show("Xóa không thành công");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn nguyên liệu cần xóa");
            }
        }

        private void dgDSLoaiNguyenLieu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgDSLoaiNguyenLieu.SelectedValue == null || dgDSLoaiNguyenLieu.SelectedValue.ToString() == "")
            {
                return;
            }
            if (dgDSLoaiNguyenLieu.SelectedValue.ToString() == "          ")
            {
                MessageBox.Show("Không thể chọn loại nguyên liệu này");
                return;
            }
            string maLoaiNguyenLieu = dgDSLoaiNguyenLieu.SelectedValue.ToString();
            loaiNguyenLieuSeclect = CLoaiNguyenLieu_BUS.find(maLoaiNguyenLieu);
            hienThiThongTin(loaiNguyenLieuSeclect);
            isEnabledThongTin(true);
        }
    }
}
