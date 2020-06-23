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
        private TaiKhoan tk;
        private QuanLyQuanCoffeeEntities1 dc = new QuanLyQuanCoffeeEntities1();

        public frmQuanLyTaiKhoan()
        {
            InitializeComponent();
            dc = new QuanLyQuanCoffeeEntities1();
            hienthiDStaikhoan();
        }
        public void hienthiDStaikhoan()
        {
            try
            {
                dgQltaikhoan.ItemsSource = CTaiKhoan_BUS.toList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi: " + ex.Message);
            }
        }


        private void cboManhanvien_Loaded(object sender, RoutedEventArgs e)
        {

            try
            {
                cboManhanvien.ItemsSource = CTaiKhoan_BUS.toListByMaLoaiNV();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi: " + ex.Message);
            }
        }

        private void cboLoaitaikhoan_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                cboLoaitaikhoan.ItemsSource = CTaiKhoan_BUS.toListByMaLoaiTK();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi: " + ex.Message);
            }
        }

        private void btnThemTK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TaiKhoan tk1 = new TaiKhoan();
                //CSanPham.saochep(sp1, sp);
                tk1.maNhanVien = cboManhanvien.SelectedItem.ToString();
                tk1.taiKhoan1 = txtTaikhoan.Text;
                tk1.matKhau = txtMatkhau.Text;
                tk1.maLoaiTaiKhoan = cboLoaitaikhoan.SelectedItem.ToString();
                tk1.trangThai = 0;
                string makt = cboManhanvien.SelectedItem.ToString();
                if (CServices.kiemTraThongTin(tk1))
                {
                    if (CTaiKhoan_BUS.find(makt) == null)
                    {
                        CTaiKhoan_BUS.add(tk1);
                    }
                    else
                    {
                        MessageBox.Show("Nhân viên này đã có tài khoản.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi: " + ex.Message);
            }

            hienthiDStaikhoan();
            load();
        }
        public void load()
        {
            txtTaikhoan.Text = "";
            txtMatkhau.Text = "";
            cboLoaitaikhoan.Text = "";
            cboManhanvien.Text = "";
            tk = null;
        }
        private void btnXoaTK_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (tk != null)
                {
                    if (CTaiKhoan_BUS.remove(tk))
                    {
                        MessageBox.Show("Xóa thành công");
                        hienthiDStaikhoan();
                    }
                    else
                    {
                        MessageBox.Show("Không thể xóa tài khoản");
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn tài khoản cần xóa");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi: " + ex.Message);
            }

            hienthiDStaikhoan();
            load();
        }

        private void btnSuaTK_Click(object sender, RoutedEventArgs e)
        {
            if (tk == null)
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần sửa");
            }
            else
            {
                TaiKhoan tkSua = new TaiKhoan();
                tkSua.maNhanVien = cboManhanvien.Text;
                tkSua.taiKhoan1 = txtTaikhoan.Text;
                tkSua.matKhau = txtMatkhau.Text;
                tkSua.maLoaiTaiKhoan = cboLoaitaikhoan.Text;
                tkSua.trangThai = tk.trangThai;
                if (CTaiKhoan_BUS.KTRong(tkSua))
                {
                    if (CTaiKhoan_BUS.edit(tkSua))
                    {
                        MessageBox.Show("Sửa thành công");
                        hienthiDStaikhoan();
                    }
                }
                else
                {
                    MessageBox.Show("Yêu cầu điền đầy đủ thông tin tài khoản");
                }
            }

            hienthiDStaikhoan();
            load();
        }

        private void dgQltaikhoan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgQltaikhoan.SelectedItem != null)
            {
                tk = CTaiKhoan_BUS.find(dgQltaikhoan.SelectedValue.ToString());
                txtTaikhoan.Text = tk.taiKhoan1;
                txtMatkhau.Text = tk.matKhau;
                cboLoaitaikhoan.Text = tk.maLoaiTaiKhoan;
                cboManhanvien.Text = tk.maNhanVien;
            }
            else
            {
                cboLoaitaikhoan.Text = "";
                cboManhanvien.Text = "";
                txtTaikhoan.Text = "";
                txtMatkhau.Text = "";
            }
        }
    }
}
