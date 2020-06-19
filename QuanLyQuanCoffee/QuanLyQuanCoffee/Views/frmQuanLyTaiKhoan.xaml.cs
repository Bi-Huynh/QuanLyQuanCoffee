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
        private QuanLyQuanCoffeeEntities dc = new QuanLyQuanCoffeeEntities();
        TaiKhoan tk;
        public frmQuanLyTaiKhoan()
        {
            InitializeComponent();
            dc = new QuanLyQuanCoffeeEntities();
            hienthiDStaikhoan();
        }
        public void hienthiDStaikhoan()
        {
            dgQltaikhoan.ItemsSource = dc.TaiKhoans.ToList();
        }


        private void cboManhanvien_Loaded(object sender, RoutedEventArgs e)
        {
            cboManhanvien.ItemsSource = dc.NhanViens.Select(x => x.maNhanVien).ToList();
        }

        private void cboLoaitaikhoan_Loaded(object sender, RoutedEventArgs e)
        {
            cboLoaitaikhoan.ItemsSource = dc.LoaiTaiKhoans.Select(x => x.maLoaiTaiKhoan).ToList();
        }

        private void btnThemTK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                tk = new TaiKhoan();
                //CSanPham.saochep(sp1, sp);
                tk.maNhanVien = cboManhanvien.SelectedItem.ToString();
                tk.taiKhoan1 = txtTaikhoan.Text;
                tk.matKhau = txtMatkhau.Text;
                tk.maLoaiTaiKhoan = cboLoaitaikhoan.SelectedItem.ToString();
                tk.trangThai = 0;
                string makt = cboManhanvien.SelectedItem.ToString();
                if (dc.TaiKhoans.Find(makt) == null)
                {
                    dc.TaiKhoans.Add(tk);
                    dc.SaveChanges();
                }
                else
                {
                    MessageBox.Show("Nhân viên này đã có tài khoản.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi: " + ex.Message);
            }
            hienthiDStaikhoan();
        }

        private void btnXoaTK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string manv = dgQltaikhoan.SelectedValue.ToString();
                TaiKhoan tkxoa = dc.TaiKhoans.Find(manv);
                if (tkxoa == null)
                {
                    MessageBox.Show("Không tìm thấy tài khoản cần xóa");
                }
                else
                {
                    dc.TaiKhoans.Remove(tkxoa);
                    dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi: " + ex.Message);
            }

            hienthiDStaikhoan();
        }

        private void btnSuaTK_Click(object sender, RoutedEventArgs e)
        {
            TaiKhoan tk = dc.TaiKhoans.Find(dgQltaikhoan.SelectedValue.ToString());
            tk.taiKhoan1 = txtTaikhoan.Text;
            tk.matKhau = txtMatkhau.Text;
            tk.maLoaiTaiKhoan = cboLoaitaikhoan.SelectedItem.ToString();
            dc.SaveChanges();
            hienthiDStaikhoan();
        }

        private void dgQltaikhoan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgQltaikhoan.SelectedItem != null)
            {
                TaiKhoan tk = dc.TaiKhoans.Find(dgQltaikhoan.SelectedValue.ToString());
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
