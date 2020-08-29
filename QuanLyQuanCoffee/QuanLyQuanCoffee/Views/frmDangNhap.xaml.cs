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
using System.Windows.Shapes;

namespace QuanLyQuanCoffee.Views
{
    /// <summary>
    /// Interaction logic for frmDangNhap.xaml
    /// </summary>
    public partial class frmDangNhap : Window
    {
        private TaiKhoan taiKhoanSelect;
        private QuanLyQuanCoffeeEntities1 dc = LoadDatabase.Instance();

        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void kiemTraTaiKhoan()
        {
            string matKhau = CTaiKhoan_BUS.Encrypt(txtMatkhau.Password);

            foreach (TaiKhoan taiKhoan in dc.TaiKhoans.ToList())
            {
                if (taiKhoan.tenTaiKhoan.Trim() == txtTaikhoan.Text.Trim() &&
                    taiKhoan.matKhau.Trim() == matKhau.Trim())
                {
                    taiKhoanSelect = taiKhoan;
                    break;
                }
            }

            if (taiKhoanSelect != null)
            {
                if (taiKhoanSelect.maTaiKhoan == "0000000001")
                {
                    new frmAdmin(taiKhoanSelect).Show();
                    this.Close();
                }
                else
                {
                    if (taiKhoanSelect.matKhau == "IZC83pakndc=" && taiKhoanSelect.trangThai == 0)   // mật khẩu mặc định là 1
                    {
                        NhanVien nhanVien = CNhanVien_BUS.find(taiKhoanSelect.maNhanVien);
                        if (nhanVien != null)
                        {
                            new frmNhanVien(nhanVien, taiKhoanSelect).Show();

                            MessageBox.Show("Vui lòng đổi mật khẩu");
                            frmDoiTaiKhoan frmDoiTaiKhoan = new frmDoiTaiKhoan(taiKhoanSelect);
                            frmDoiTaiKhoan.Show();

                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy nhân viên sở hữu tài khoản này");
                        }
                    }
                    else if (taiKhoanSelect.trangThai == 0)
                    {
                        NhanVien nhanVien = CNhanVien_BUS.find(taiKhoanSelect.maNhanVien);
                        if (nhanVien != null)
                        {
                            new frmNhanVien(nhanVien, taiKhoanSelect).Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy nhân viên sở hữu tài khoản này");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Tài khoản này đã bị khóa");
                    }
                }
            }
            else
            {
                MessageBox.Show("Sai tài khoản hoặc mật khẩu");
            }
        }

        private void btnDangNhap_Click(object sender, RoutedEventArgs e)
        {
            kiemTraTaiKhoan();
        }

        private void txtMatkhau_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnDangNhap_Click(sender, e);
            }
        }
    }
}
