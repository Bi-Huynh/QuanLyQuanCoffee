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
    /// Interaction logic for frmQuanLyLoaiTaiKhoan.xaml
    /// </summary>
    public partial class frmQuanLyLoaiTaiKhoan : Page
    {
        private QuanLyQuanCoffeeEntities1 dc = new QuanLyQuanCoffeeEntities1();
        LoaiTaiKhoan loaiTK;
        LoaiTaiKhoan a;
        public frmQuanLyLoaiTaiKhoan()
        {
            InitializeComponent();
            HienThiDSLoaitaikhoan();
        }
        public void HienThiDSLoaitaikhoan()
        {
            dgLoaitaikhoan.ItemsSource = CLoaiTaiKhoan_BUS.toList();
        }
        private void btnThemLoaiTK_Click(object sender, RoutedEventArgs e)
        {
            LoaiTaiKhoan ltk = new LoaiTaiKhoan();
            ltk.maLoaiTaiKhoan = txtmaLoaitaikhoan.Text;
            ltk.tenLoaiTaiKhoan = txttenLoaitaikhoan.Text;
            ltk.trangThai = 0;
            string makt = txtmaLoaitaikhoan.Text;
            if (CLoaiTaiKhoan_BUS.KTRong(ltk))
            {
                if (CLoaiTaiKhoan_BUS.find(makt) == null)
                {

                    CLoaiTaiKhoan_BUS.add(ltk);
                    MessageBox.Show("Thêm thành công");
                    txtmaLoaitaikhoan.Text = "";
                }
                else
                {
                    MessageBox.Show("Mã loại bì trùng");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập tên loại");
            }
            HienThiDSLoaitaikhoan();
            load();

        }

        public void load()
        {
            txtmaLoaitaikhoan.Text = "";
            txttenLoaitaikhoan.Text = "";
            loaiTK = null;
        }
        private void btnXoaLoaiTK_Click(object sender, RoutedEventArgs e)
        {
            if (loaiTK != null)
            {
                try
                {
                    string maloai = loaiTK.maLoaiTaiKhoan.ToString();
                    if (maloai == null)
                    {
                        MessageBox.Show("Không tìm thấy");
                    }
                    else
                    {
                        loaiTK = CLoaiTaiKhoan_BUS.find(maloai);
                        if (CLoaiTaiKhoan_BUS.remove(loaiTK))
                        {
                            MessageBox.Show("Xóa thành công " + maloai + " khỏi danh sách");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi: " + ex.Message);
                }

            }
            else
            {
                MessageBox.Show("Vui lòng chọn loại tài khoản cần xóa");
            }
            HienThiDSLoaitaikhoan();
            load();
        }

        private void btnSuaLoaiTK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (loaiTK == null)
                {
                    MessageBox.Show("Vui lòng chọn loại tài khoản cần sửa");
                }
                else
                {
                    LoaiTaiKhoan a = new LoaiTaiKhoan();
                    a.maLoaiTaiKhoan = txtmaLoaitaikhoan.Text;
                    a.tenLoaiTaiKhoan = txttenLoaitaikhoan.Text;
                    a.trangThai = 0;
                    if (CLoaiTaiKhoan_BUS.KTRong(a))
                    {
                        if (CLoaiTaiKhoan_BUS.edit(a))
                        {
                            MessageBox.Show("Sửa thành công");
                            HienThiDSLoaitaikhoan();
                            load();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng nhập tên loại tài khoản");
                    }    
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
                    loaiTK = CLoaiTaiKhoan_BUS.find(dgLoaitaikhoan.SelectedValue.ToString());
                    txtmaLoaitaikhoan.Text = loaiTK.maLoaiTaiKhoan;
                    txttenLoaitaikhoan.Text = loaiTK.tenLoaiTaiKhoan;
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

        private void btnBochon_Click(object sender, RoutedEventArgs e)
        {
            txtmaLoaitaikhoan.Text = "";
            txttenLoaitaikhoan.Text = "";
            loaiTK = null;
        }
    }
}
