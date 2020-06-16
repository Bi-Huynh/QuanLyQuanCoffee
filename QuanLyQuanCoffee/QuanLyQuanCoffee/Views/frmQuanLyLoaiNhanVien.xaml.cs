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
    /// Interaction logic for frmQuanLyLoaiNhanVien.xaml
    /// </summary>
    public partial class frmQuanLyLoaiNhanVien : Window
    {
        private LoaiNhanVien loaiNhanVienSelect;

        public frmQuanLyLoaiNhanVien()
        {
            InitializeComponent();
            loaiNhanVienSelect = new LoaiNhanVien();
            hienThiDSLoaiNhanVien(CLoaiNhanVien_BUS.toList());
            isEnabledThongTin(false);
            txtMaLoaiNhanVien.Text = CServices.taoMa<LoaiNhanVien>(CLoaiNhanVien_BUS.toList());
        }

        private void hienThiDSLoaiNhanVien(List<LoaiNhanVien> list)
        {
            dgDSLoaiNhanVien.ItemsSource = list;
        }

        private void hienThiThongTin(LoaiNhanVien loaiNhanVien)
        {
            txtMaLoaiNhanVien.Text = loaiNhanVien.maLoaiNhanvien;
            txtTenLoai.Text = loaiNhanVien.tenLoai;
            txtLuong.Text = loaiNhanVien.luongCoBan.ToString();
        }

        private void isEnabledThongTin(bool value)
        {
            btnBoChon.IsEnabled = value;
            btnSua.IsEnabled = value;
            btnXoa.IsEnabled = value;
        }

        private void dgDSLoaiNhanVien_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgDSLoaiNhanVien.SelectedValue == null)
            {
                return;
            }
            string maLoaiNhanVien = dgDSLoaiNhanVien.SelectedValue.ToString();
            loaiNhanVienSelect = CLoaiNhanVien_BUS.find(maLoaiNhanVien);
            hienThiThongTin(loaiNhanVienSelect);
            isEnabledThongTin(true);
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            LoaiNhanVien loaiNhanVien = new LoaiNhanVien();
            loaiNhanVien.maLoaiNhanvien = txtMaLoaiNhanVien.Text;
            loaiNhanVien.tenLoai = txtTenLoai.Text;
            loaiNhanVien.luongCoBan = double.Parse(txtLuong.Text);

            if (CLoaiNhanVien_BUS.add(loaiNhanVien))
            {
                MessageBox.Show("Thêm thành công");
                hienThiDSLoaiNhanVien(CLoaiNhanVien_BUS.toList());
            }
            else
            {
                MessageBox.Show("Thêm không thành công");
            }
            
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            if (loaiNhanVienSelect != null)
            {
                if (CLoaiNhanVien_BUS.remove(loaiNhanVienSelect))
                {
                    MessageBox.Show("Xóa thành công");
                    hienThiDSLoaiNhanVien(CLoaiNhanVien_BUS.toList());
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xóa");
            }
        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            LoaiNhanVien loaiNhanVien = new LoaiNhanVien();
            loaiNhanVien.maLoaiNhanvien = txtMaLoaiNhanVien.Text;
            loaiNhanVien.tenLoai = txtTenLoai.Text;
            loaiNhanVien.luongCoBan = double.Parse(txtLuong.Text);

            if (CLoaiNhanVien_BUS.edit(loaiNhanVien))
            {
                MessageBox.Show("Sửa thành công");
                hienThiDSLoaiNhanVien(CLoaiNhanVien_BUS.toList());
            }
            else
            {
                MessageBox.Show("Sửa không thành công");
            }
        }

        private void btnBoChon_Click(object sender, RoutedEventArgs e)
        {
            hienThiThongTin(new LoaiNhanVien());
            isEnabledThongTin(false);
        }
    }
}
