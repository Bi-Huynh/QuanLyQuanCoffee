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

        public frmQuanLyNhanVien()
        {
            InitializeComponent();

            hienThiDSNhanVien(CNhanVien_BUS.toList());
        }

        private void hienThiDSNhanVien(List<NhanVien> list)
        {
            dgDSNhanVien.ItemsSource = list.Select(x => new
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
                    hienThiDSNhanVien(CNhanVien_BUS.toList());
                }
                else
                {
                    MessageBox.Show("Xóa không thành công");
                }
            }
        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            if (nhanVienSelect != null)
            {
                new frmThongTinNhanVien(nhanVienSelect, 2).Show();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn nhân viên");
            }

        }

        private void btnXemThongTinChiTiet_Click(object sender, RoutedEventArgs e)
        {
            if (nhanVienSelect != null)
            {
                new frmThongTinNhanVien(nhanVienSelect).Show();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn nhân viên");
            }

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
            hienThiDSNhanVien(CNhanVien_BUS.toList());
        }

        private void txtTimKiem_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtTimKiem.Text == "")
            {
                hienThiDSNhanVien(CNhanVien_BUS.toList());
                return;
            }

            // nếu combox tìm kiếm là 0 tức là tìm theo tên nhân viên
            if (cmbTimKiem.SelectedIndex == 0)
            {
                hienThiDSNhanVien(CNhanVien_BUS.findTen(txtTimKiem.Text));
            }
            //nếu combox tìm kiếm là 1 tức là tìm theo mã nhân viên
            else if (cmbTimKiem.SelectedIndex == 1)
            {
                List<NhanVien> list = new List<NhanVien>();
                list.Add(CNhanVien_BUS.find(txtTimKiem.Text));
                hienThiDSNhanVien(list);
            }
            else
            {
                hienThiDSNhanVien(CNhanVien_BUS.findTenLoai(txtTimKiem.Text)); ;
            }
        }
    }
}
