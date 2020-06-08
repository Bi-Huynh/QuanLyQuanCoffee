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
        }

        private void hienThiDSLoaiNhanVien(List<LoaiNhanVien> list)
        {
            dgDSLoaiNhanVien.ItemsSource = list;
        }

        private void dgDSLoaiNhanVien_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgDSLoaiNhanVien.SelectedItem == null)
            {
                return;
            }
            loaiNhanVienSelect = dgDSLoaiNhanVien.SelectedValue as LoaiNhanVien;
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            string maLoaiNhanVien = "";
            do
            {
                maLoaiNhanVien = CNhanVien_BUS.randomMaNhanVien();
            } while (CLoaiNhanVien_BUS.find(maLoaiNhanVien) == null);

            LoaiNhanVien loaiNhanVien = new LoaiNhanVien(
                maLoaiNhanVien,
                txtTenLoai.Text,
                double.Parse(txtLuong.Text)
                );

            if (CLoaiNhanVien_BUS.add(loaiNhanVien))
            {
                MessageBox.Show("Thêm thành công");
            }
            else
            {
                MessageBox.Show("Thêm không thành công");
            }
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            if (loaiNhanVienSelect == null)
            {
                return;
            }
            if (CLoaiNhanVien_BUS.remove(loaiNhanVienSelect))
            {
                MessageBox.Show("Xóa thành công");
            }
            else
            {
                MessageBox.Show("Xóa không thành công");
            }
        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            LoaiNhanVien loaiNhanVien = new LoaiNhanVien(
                txtMaLoaiNhanVien.Text,
                txtTenLoai.Text,
                double.Parse(txtLuong.Text)
                );
            if (CLoaiNhanVien_BUS.remove(loaiNhanVien))
            {
                MessageBox.Show("Xóa thành công");
            }
            else
            {
                MessageBox.Show("Xóa không thành công");
            }
        }
    }
}
