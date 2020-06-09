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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLyQuanCoffee.Views
{
    /// <summary>
    /// Interaction logic for frmQuanLyNguyenLieu.xaml
    /// </summary>
    public partial class frmQuanLyNguyenLieu : Page
    {
        private NguyenLieu nguyenLieuSelect;

        public frmQuanLyNguyenLieu()
        {
            InitializeComponent();
            hienThiDS(CNguyenLieu_BUS.toList());
        }

        private void hienThiDS(List<NguyenLieu> list)
        {
            dgDSNguyenLieu.ItemsSource = list.Select(x => new
            {
                maNguyenLieu = x.maNguyenLieu,
                tenNguyenLieu = x.tenNguyenLieu,
                donGia = x.donGia,
                soLuong = x.soLuong,
                ngayHetHan = x.ngayHetHan.ToString("dd/MM/yyyy"),
                ngayNhap = x.ngayNhap.ToString("dd/MM/yyyy")
            });
        }

        private void CommandBinding_Executed_XoaNguyenLieu(object sender, ExecutedRoutedEventArgs e)
        {
            if (CNguyenLieu_BUS.remove(nguyenLieuSelect))
            {
                MessageBox.Show("Xóa thành công");
                hienThiDS(CNguyenLieu_BUS.toList());
            }
            else
            {
                MessageBox.Show("Xóa không thành công");
            }
        }

        private void CommandBinding_CanExecute_XoaNguyenLieu(object sender, CanExecuteRoutedEventArgs e)
        {
            if (nguyenLieuSelect != null)
            {
                e.CanExecute = true;
            }
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            new frmThongTinNguyenLieu().Show();
        }

        private void btnRefesh_Click(object sender, RoutedEventArgs e)
        {
            hienThiDS(CNguyenLieu_BUS.toList());
        }

        private void btnXemThongTinChiTiet_Click(object sender, RoutedEventArgs e)
        {
            if (nguyenLieuSelect == null)
            {
                MessageBox.Show("Vui lòng chọn nguyên liệu");
                return;
            }
            new frmThongTinNguyenLieu(nguyenLieuSelect).Show();
        }

        private void dgDSNguyenLieu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgDSNguyenLieu.SelectedItem != null)
            {
                nguyenLieuSelect = CNguyenLieu_BUS.find(dgDSNguyenLieu.SelectedValue.ToString());
            }
        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            if (nguyenLieuSelect != null)
            {
                new frmThongTinNguyenLieu(nguyenLieuSelect).Show();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn nguyên liệu");
            }

        }
    }
}
