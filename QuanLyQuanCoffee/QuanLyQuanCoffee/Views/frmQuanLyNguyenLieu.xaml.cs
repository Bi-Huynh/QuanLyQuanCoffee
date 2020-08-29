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
            hienThiDS(CNguyenLieu_BUS.to_List());
        }

        public void hienThiDS(List<NguyenLieu> list)
        {
            if (list.Count() > 0)
            {
                dgDSNguyenLieu.ItemsSource = list.Select(x => new
                {
                    maNguyenLieu = x.maNguyenLieu,
                    tenNguyenLieu = x.tenNguyenLieu,
                    tongSoLuong = CChiTietPhieuNhapNguyenLieu_BUS.tongSoLuong(x.maNguyenLieu),
                    tongThanhTien = String.Format("{0:#,###,0 VND;(#,###,0 VND);0 VND}", CChiTietPhieuNhapNguyenLieu_BUS.tongThanhTien(x.maNguyenLieu)),
                    tenLoaiNguyenLieu = x.LoaiNguyenLieu.tenLoaiNguyenLieu
                });
            }
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            frmThongTinNguyenLieu frm = new frmThongTinNguyenLieu();
            frm.Show();
        }

        private void btnRefesh_Click(object sender, RoutedEventArgs e)
        {
            hienThiDS(CNguyenLieu_BUS.to_List());
        }

        // chưa sửa hiển thị thông tin chi tiết của nguyên liệu, chưa tạo giao diện cho phần này
        private void btnXemThongTinChiTiet_Click(object sender, RoutedEventArgs e)
        {
            if (nguyenLieuSelect == null)
            {
                MessageBox.Show("Vui lòng chọn nguyên liệu");
                return;
            }
            new frmThongTinChiTietNguyenLieu(nguyenLieuSelect).Show();
        }

        private void dgDSNguyenLieu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgDSNguyenLieu.SelectedItem != null)
            {
                nguyenLieuSelect = new NguyenLieu();
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
                var result = MessageBox.Show("Do you want to delete changes?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    if (CNguyenLieu_BUS.remove(nguyenLieuSelect))
                    {
                        hienThiDS(CNguyenLieu_BUS.to_List());
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn nguyên liệu");
            }
        }

        private void txtTimKiem_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtTimKiem.Text == "")
            {
                hienThiDS(CNguyenLieu_BUS.to_List());
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
