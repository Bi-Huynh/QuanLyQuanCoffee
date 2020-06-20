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
    /// Interaction logic for frmQuanLyLoaiTaiKhoan.xaml
    /// </summary>
    public partial class frmQuanLyLoaiTaiKhoan : Page
    {
        private QuanLyQuanCoffeeEntities1 dc = new QuanLyQuanCoffeeEntities1();
        LoaiTaiKhoan a;
        public frmQuanLyLoaiTaiKhoan()
        {
            InitializeComponent();
            HienThiDSLoaitaikhoan();
        }
        public void HienThiDSLoaitaikhoan()
        {
            dgLoaitaikhoan.ItemsSource = dc.LoaiTaiKhoans.ToList();
        }
        private void btnThemLoaiTK_Click(object sender, RoutedEventArgs e)
        {
            LoaiTaiKhoan a = new LoaiTaiKhoan();
            a.maLoaiTaiKhoan = txtmaLoaitaikhoan.Text;
            a.tenLoaiTaiKhoan = txttenLoaitaikhoan.Text;
            a.trangThai = 0;
            string makt = txtmaLoaitaikhoan.Text;
            if (dc.LoaiTaiKhoans.Find(makt) == null)
            {
                dc.LoaiTaiKhoans.Add(a);
                dc.SaveChanges();
            }
            else
            {
                MessageBox.Show("Mã Loại bị trùng!");
            }
            HienThiDSLoaitaikhoan();
        }

        private void btnXoaLoaiTK_Click(object sender, RoutedEventArgs e)
        {
            string maloai = dgLoaitaikhoan.SelectedValue.ToString();
            LoaiTaiKhoan a = dc.LoaiTaiKhoans.Find(maloai);
            if (maloai == null)
            {
                MessageBox.Show("Khong tim thay");
            }
            else
            {
                if ((dc.TaiKhoans.Where(x => x.maLoaiTaiKhoan == maloai).ToList().Count > 0) == false)
                {
                    //MessageBox.Show("Bạn có chắc muốn xóa mã loại " + maloai + " khỏi danh sách sản phẩm Không?");
                    dc.LoaiTaiKhoans.Remove(a);
                    dc.SaveChanges();
                    MessageBox.Show("Xóa thành công " + maloai + " khỏi danh sách");
                }
                else
                {
                    MessageBox.Show("Mã loại tài khoản này tồn tại trong Danh sách tài khoản");
                }


            }

            HienThiDSLoaitaikhoan();
        }

        private void btnSuaLoaiTK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((dc.LoaiTaiKhoans.Find(txtmaLoaitaikhoan.Text) == null))
                {
                    MessageBox.Show("Bạn không thể sửa loại tài khoản");
                }
                else
                {
                    LoaiTaiKhoan a = dc.LoaiTaiKhoans.Find(txtmaLoaitaikhoan.Text);
                    a.maLoaiTaiKhoan = txtmaLoaitaikhoan.Text;
                    a.tenLoaiTaiKhoan = txttenLoaitaikhoan.Text;
                    dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi: " + ex.Message);
            }
            HienThiDSLoaitaikhoan();
        }

        private void dgLoaitaikhoan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dgLoaitaikhoan.SelectedItem != null)
                {
                    a = dc.LoaiTaiKhoans.Find(dgLoaitaikhoan.SelectedValue.ToString());
                    txtmaLoaitaikhoan.Text = a.maLoaiTaiKhoan;
                    txttenLoaitaikhoan.Text = a.tenLoaiTaiKhoan;
                }
                else
                {
                    txtmaLoaitaikhoan.Text = "";
                    txttenLoaitaikhoan.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi: " + ex.Message);
            }
        }
    }
}
