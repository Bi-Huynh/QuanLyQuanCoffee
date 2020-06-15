﻿using QuanLyQuanCoffee.BUS;
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
    /// Interaction logic for frmThongTinNhanVien.xaml
    /// </summary>
    public partial class frmThongTinNhanVien : Window
    {
        private NhanVien nhanVienSelect = null;

        public frmThongTinNhanVien(NhanVien nhanVien = null, int flag = 1)
        {
            InitializeComponent();

            // khi người dùng nhấn thêm thì ấn nút sửa đi
            if (flag == 1)
            {
                btnSua.IsEnabled = false;
                btnLuu.IsEnabled = false;
            }
            // khi người dùng nhấn nút sửa
            else if (flag == 2)
            {
                btnThem.IsEnabled = false;
                btnSua.IsEnabled = false;
            }
            // là khi người dùng bấm nút xem chi tiết
            else
            {
                btnThem.IsEnabled = false;
                btnLuu.IsEnabled = false;
                btnResest.IsEnabled = false;
                isEnabledThongTin(false);
            }
            if (nhanVien != null)
            {
                nhanVienSelect = nhanVien;
                hienThiThongTin(nhanVienSelect);
            }
            cmbLoaiNhanVien.ItemsSource = CLoaiNhanVien_BUS.toListTenLoai();
            dateNgayVaoLam.SelectedDate = DateTime.Now;
        }

        private void hienThiThongTin(NhanVien nhanVien)
        {
            txtMaNhanVien.Text = nhanVien.maNhanVien;
            txtHoNhanVien.Text = nhanVien.hoNhanVien;
            txtTenNhanVien.Text = nhanVien.tenNhanVien;
            dateNgayVaoLam.SelectedDate = nhanVien.ngayVaoLam;
            cmbLoaiNhanVien.SelectedItem = nhanVien.LoaiNhanVien.tenLoai;
            cmbPhai.SelectedIndex = nhanVien.phai == true ? 0 : 1;
            txtSoDienThoai.Text = nhanVien.soDienThoai;
            dateNgaySinh.SelectedDate = nhanVien.ngaySinh;
            txtThuongTru.Text = nhanVien.thuongTru;
            txtTamTru.Text = nhanVien.tamTru;
            txtCMND.Text = nhanVien.cMND;
            txtTuoi.Text = CNhanVien_BUS.tinhTuoi(nhanVien).ToString();
        }

        private void isEnabledThongTin(bool value)
        {
            txtHoNhanVien.IsEnabled = value;
            txtTenNhanVien.IsEnabled = value;
            cmbLoaiNhanVien.IsEnabled = value;
            dateNgayVaoLam.IsEnabled = value;
            cmbPhai.IsEnabled = value;
            txtSoDienThoai.IsEnabled = value;
            dateNgaySinh.IsEnabled = value;
            txtCMND.IsEnabled = value;
            txtThuongTru.IsEnabled = value;
            txtTamTru.IsEnabled = value;
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            string maNhanVien = "";
            string phai = cmbPhai.SelectedValue.ToString();

            // chưa xét trường hợp nếu các mã tạo ra đã trùng hết thì sẽ làm như nào
            do
            {
                maNhanVien = CServices.randomMa();
            } while (CNhanVien_BUS.find(maNhanVien) != null);

            NhanVien nhanVien = new NhanVien();
            nhanVien.maNhanVien = maNhanVien;
            nhanVien.hoNhanVien = txtHoNhanVien.Text;
            nhanVien.tenNhanVien = txtTenNhanVien.Text;
            nhanVien.soDienThoai = txtSoDienThoai.Text;
            nhanVien.ngaySinh = dateNgaySinh.SelectedDate.Value.Date;
            nhanVien.phai = cmbPhai.SelectedValue.ToString() == "Nam" ? true : false;
            nhanVien.cMND = txtCMND.Text;
            nhanVien.thuongTru = txtThuongTru.Text;
            nhanVien.tamTru = txtTamTru.Text;
            nhanVien.ngayVaoLam = dateNgayVaoLam.SelectedDate.Value.Date;
            nhanVien.maLoaiNhanVien = CLoaiNhanVien_BUS.findMaLoaiByTenLoai(cmbLoaiNhanVien.SelectedItem.ToString());

            if (CNhanVien_BUS.add(nhanVien))
            {
                MessageBox.Show("Thêm thành công!");
                this.Close();
            }
            else
            {
                MessageBox.Show("Thông tin không hợp lệ, Vui lòng kiểm tra lại!");
            }
        }

        private void btnResest_Click(object sender, RoutedEventArgs e)
        {
            // thêm cái group box vào để biding dữ liệu lên
            if (nhanVienSelect == null)
            {
                txtMaNhanVien.Text = "";
                txtHoNhanVien.Text = "";
                txtTenNhanVien.Text = "";
                dateNgayVaoLam.SelectedDate = DateTime.Now;
                cmbLoaiNhanVien.SelectedIndex = 0;
                cmbPhai.SelectedIndex = 0;
                txtSoDienThoai.Text = "";
                dateNgaySinh.SelectedDate = DateTime.Now;
                txtThuongTru.Text = "";
                txtTamTru.Text = "";
                txtCMND.Text = "";
            }
            else
            {
                hienThiThongTin(nhanVienSelect);
            }
        }

        // sự kiện tính tuổi sau khi người dùng chọn ngày sinh của minh
        private void dateNgaySinh_CalendarClosed(object sender, RoutedEventArgs e)
        {
            txtTuoi.Text = CNhanVien_BUS.tinhTuoi(dateNgaySinh.SelectedDate.Value).ToString();
        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            btnLuu.IsEnabled = true;
            isEnabledThongTin(true);
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            string phai = cmbPhai.SelectedValue.ToString();
            NhanVien nhanVien = new NhanVien();
            nhanVien.maNhanVien = txtMaNhanVien.Text;
            nhanVien.hoNhanVien = txtHoNhanVien.Text;
            nhanVien.tenNhanVien = txtTenNhanVien.Text;
            nhanVien.soDienThoai = txtSoDienThoai.Text;
            nhanVien.ngaySinh = dateNgaySinh.SelectedDate.Value.Date;
            nhanVien.phai = cmbPhai.SelectedValue.ToString() == "Nam" ? true : false;
            nhanVien.cMND = txtCMND.Text;
            nhanVien.thuongTru = txtThuongTru.Text;
            nhanVien.tamTru = txtTamTru.Text;
            nhanVien.ngayVaoLam = dateNgayVaoLam.SelectedDate.Value.Date;
            nhanVien.maLoaiNhanVien = CLoaiNhanVien_BUS.findMaLoaiByTenLoai(cmbLoaiNhanVien.SelectedItem.ToString());

            if (CNhanVien_BUS.edit(nhanVien))
            {
                MessageBox.Show("Sửa thành công");
                this.Close();
            }
            else
            {
                MessageBox.Show("Sửa không thành công");
            }
        }
    }
}
