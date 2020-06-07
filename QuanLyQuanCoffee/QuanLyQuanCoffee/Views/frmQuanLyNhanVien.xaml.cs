﻿using QuanLyQuanCoffee.BUS;
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
    /// Interaction logic for frmQuanLyNhanVien.xaml
    /// </summary>
    public partial class frmQuanLyNhanVien : Page
    {
        private NhanVien nhanVienSelect;

        private void hienThiDSNhanVien()
        {
            dgDSNhanVien.ItemsSource = CNhanVien_BUS.toList().Select(x => new
            {
                maNhanVien = x.maNhanVien,
                hoNhanVien = x.hoNhanVien,
                tenNhanVien = x.tenNhanVien,
                tenLoai = x.LoaiNhanVien.tenLoai,
                phai = x.phai == true ? "Nam" : "Nữ",
                soDienThoai = x.soDienThoai,
                ngayVaoLam = x.ngayVaoLam
            });
        }

        public frmQuanLyNhanVien()
        {
            InitializeComponent();
            nhanVienSelect = new NhanVien();

            hienThiDSNhanVien();
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            new frmThongTinNhanVien(null, 1).Show();

        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            if (nhanVienSelect != null)
            {
                if (CNhanVien_BUS.remove(nhanVienSelect))
                {
                    MessageBox.Show("Xóa thành công");
                }
                else
                {
                    MessageBox.Show("Xóa không thành công");
                }
            }
        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            new frmThongTinNhanVien(nhanVienSelect, 2).Show();
        }

        private void btnXemThongTinChiTiet_Click(object sender, RoutedEventArgs e)
        {
            new frmThongTinNhanVien(nhanVienSelect).Show();
        }

        private void dgDSNhanVien_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgDSNhanVien.SelectedItem == null)
            {
                return;
            }
            var maNhanVien = dgDSNhanVien.SelectedValue.ToString();
            nhanVienSelect = CNhanVien_BUS.find(maNhanVien);
        }

        private void btnRefesh_Click(object sender, RoutedEventArgs e)
        {
            hienThiDSNhanVien();
        }
    }
}
