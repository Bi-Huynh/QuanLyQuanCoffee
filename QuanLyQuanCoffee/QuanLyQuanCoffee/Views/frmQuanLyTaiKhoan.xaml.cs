﻿using Prism.Services.Dialogs;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLyQuanCoffee.Views
{
    /// <summary>
    /// Interaction logic for frmQuanLyTaiKhoan.xaml
    /// </summary>
    public partial class frmQuanLyTaiKhoan : Page
    {
        private TaiKhoan taiKhoanSelect;

        public frmQuanLyTaiKhoan()
        {
            InitializeComponent();

            hienthiDStaikhoan();

        }

        public void hienthiDStaikhoan()
        {
            dgQltaikhoan.ItemsSource = CTaiKhoan_BUS.toListNotAdmin().Select(x => new
            {
                maTaiKhoan = x.maTaiKhoan,
                maNhanVien = x.maNhanVien,
                tenNhanVien = x.NhanVien.hoNhanVien + " " + x.NhanVien.tenNhanVien,
                taiKhoan = x.tenTaiKhoan,
                matKhau = x.matKhau,
                trangThai = x.trangThai == 0 ? "Mở khóa" : "Đã khóa"
            });
        }

        private void btnThemTK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtTaiKhoan.Text == "" || txtTaiKhoan.Text == null ||
                    txtMatKhau.Text == "" || txtMatKhau.Text == null)
                {
                    MessageBox.Show("Yêu cầu điền thông tin tài khoản");
                    return;
                }

                TaiKhoan taiKhoan = new TaiKhoan();
                string maNhanVien = CNhanVien_BUS.findTenbyMa(cmbTenNhanVien.SelectedItem.ToString());
                if (maNhanVien == "")
                {
                    MessageBox.Show("Vui lòng chọn nhân viên");
                    return;
                }
                taiKhoan.maNhanVien = maNhanVien;

                if (taiKhoan.maNhanVien == null || taiKhoan.maNhanVien == "")
                {
                    MessageBox.Show("Không lấy được mã nhân viên");
                    return;
                }
                if ((txtTaiKhoan.Text == null || txtTaiKhoan.Text == "") &&
                    (txtMatKhau.Text == null || txtMatKhau.Text == ""))
                {
                    MessageBox.Show("Điền đầy đủ thông tin tài khoản");
                    return;
                }
                if (CTaiKhoan_BUS.findTK(txtTaiKhoan.Text))
                {
                    MessageBox.Show("Tên tài khoản đã tồn tại");
                    return;
                }

                foreach (char item in txtTaiKhoan.Text)
                {
                    if ((item < 65 || item > 90) && (item < 97 || item > 122) && (item < 0 || item > 57))
                    {
                        MessageBox.Show("Tên tài khoản chỉ có các chữ cái in hoa hoặc thường và số");
                        return;
                    }
                }

                taiKhoan.tenTaiKhoan = txtTaiKhoan.Text;
                taiKhoan.matKhau = CTaiKhoan_BUS.Encrypt(txtMatKhau.Text);
                taiKhoan.maTaiKhoan = CServices.taoMa<TaiKhoan>(CTaiKhoan_BUS.toList());

                taiKhoan.trangThai = 0;

                if (CServices.kiemTraThongTin(taiKhoan))//Kiểm tra thông tin tài khoản hợp lệ
                {
                    CTaiKhoan_BUS.add(taiKhoan);//Thêm tài khoản
                    MessageBox.Show("Thêm thành công");
                }
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Bạn chưa chọn mã nhân viên cần cấp tài khoản");
            }

            hienthiDStaikhoan();
            load();
            cmbTenNhanVien_Loaded(sender, e);
        }

        public void load()
        {
            txtTaiKhoan.Text = "";
            txtMatKhau.Text = "1";
        }

        private void btnKhoaTaiKhoan_Click(object sender, RoutedEventArgs e)
        {
            if (dgQltaikhoan.SelectedItem != null)
            {
                if (CTaiKhoan_BUS.khoaTaiKhoan(dgQltaikhoan.SelectedValue.ToString()))
                {
                    hienthiDStaikhoan();
                }
            }
        }

        private void btnMoTaiKhoan_Click(object sender, RoutedEventArgs e)
        {
            if (dgQltaikhoan.SelectedItem != null)
            {
                if (CTaiKhoan_BUS.moKhoaTaiKhoan(dgQltaikhoan.SelectedValue.ToString()))
                {
                    hienthiDStaikhoan();
                }
            }
        }

        private void cmbTenNhanVien_Loaded(object sender, RoutedEventArgs e)
        {
            List<NhanVien> nhanViens = CNhanVien_BUS.toListNotAccount();
            cmbTenNhanVien.ItemsSource = nhanViens.Select(x => x.hoNhanVien + " " + x.tenNhanVien).ToList();
        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            if (taiKhoanSelect != null)
            {
                foreach (char item in txtTaiKhoan.Text)
                {
                    if ((item < 65 || item > 90) && (item < 97 || item > 122) && (item < 0 || item > 57))
                    {
                        MessageBox.Show("Tên tài khoản chỉ có các chữ cái in hoa hoặc thường và số");
                        return;
                    }
                }

                taiKhoanSelect.tenTaiKhoan = txtTaiKhoan.Text;
                if (CTaiKhoan_BUS.edit(taiKhoanSelect))
                {
                    MessageBox.Show("Sửa thành công");
                    txtMatKhau.IsEnabled = true;
                    load();
                    hienthiDStaikhoan();
                }
            }
        }

        private void dgQltaikhoan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgQltaikhoan.SelectedItem != null)
            {
                taiKhoanSelect = new TaiKhoan();
                taiKhoanSelect = CTaiKhoan_BUS.find(dgQltaikhoan.SelectedValue.ToString());

                if (taiKhoanSelect != null)
                {
                    txtTaiKhoan.Text = taiKhoanSelect.tenTaiKhoan;
                    txtMatKhau.Text = "";
                    txtMatKhau.IsEnabled = false;
                }
            }
        }

        private void btnBoChon_Click(object sender, RoutedEventArgs e)
        {
            if (taiKhoanSelect != null)
            {
                load();
                taiKhoanSelect = null;
                txtMatKhau.IsEnabled = true;
            }
        }
    }
}
