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
using System.Windows.Shapes;

namespace QuanLyQuanCoffee.Views
{
    /// <summary>
    /// Interaction logic for frmDangNhap.xaml
    /// </summary>
    public partial class frmDangNhap : Window
    {
        private TaiKhoan taiKhoan;
        private QuanLyQuanCoffeeEntities1 dc = new QuanLyQuanCoffeeEntities1();

        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void kiemTraTaiKhoan()
        {
            string matKhau = CTaiKhoan_BUS.Encrypt(txtMatkhau.Password);

            taiKhoan = dc.TaiKhoans.Where(x => x.taiKhoan1 == txtTaikhoan.Text &&
                                                x.matKhau == txtMatkhau.Password).FirstOrDefault();

            if (taiKhoan != null)
            {
                if (taiKhoan.maLoaiTaiKhoan == "00001")
                {
                    new frmAdmin(taiKhoan).Show();
                    this.Close();
                }
                else
                {
                    if (taiKhoan.trangThai == 0 || taiKhoan.trangThai == 3)
                    {
                        NhanVien nhanVien = CNhanVien_BUS.find(taiKhoan.maNhanVien);
                        if (nhanVien != null)
                        {
                            new frmNhanVien(nhanVien, taiKhoan).Show();
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
    }
}
