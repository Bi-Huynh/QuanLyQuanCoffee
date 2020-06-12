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

        public void hienThiDS(List<NguyenLieu> list)
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

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            frmThongTinNguyenLieu frm = new frmThongTinNguyenLieu();
            frm.Show();
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
            new frmThongTinNguyenLieu(nguyenLieuSelect, 0).Show();
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
                new frmThongTinNguyenLieu(nguyenLieuSelect, 2).Show();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn nguyên liệu");
            }

        }

        private void btnQuanLyLoaiNguyenLieu_Click(object sender, RoutedEventArgs e)
        {
            new frmQuanLyLoaiNguyenLieu().Show();
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            if (nguyenLieuSelect != null)
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
            else
            {
                MessageBox.Show("vui lòng chọn nguyên liệu");
            }
        }

        private void txtTimKiem_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtTimKiem.Text == "")
            {
                hienThiDS(CNguyenLieu_BUS.toList());
                return;
            }

            // nếu combox tìm kiếm là 0 tức là tìm theo tên nguyên liệu
            if (cmbTimKiem.SelectedIndex == 0)
            {
                hienThiDS(CNguyenLieu_BUS.findTen(txtTimKiem.Text));
            }
            //nếu combox tìm kiếm là 1 tức là tìm theo mã nguyên liệu
            else if (cmbTimKiem.SelectedIndex == 1)
            {
                List<NguyenLieu> list = CNguyenLieu_BUS.findMa(txtTimKiem.Text);
                hienThiDS(list);
            }
            else
            {
                hienThiDS(CNguyenLieu_BUS.findTenLoai(txtTimKiem.Text));
            }
        }
    }
}
