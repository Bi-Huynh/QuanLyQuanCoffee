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
        private TaiKhoan tk;
        private QuanLyQuanCoffeeEntities1 dc = new QuanLyQuanCoffeeEntities1();

        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, RoutedEventArgs e)
        {
            tk = dc.TaiKhoans.Where(x => x.taiKhoan1 == txtTaikhoan.Text && x.matKhau == txtMatkhau.Password).FirstOrDefault();
            if (tk != null)
            {
                frmAdmin f = new frmAdmin(tk);
                f.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Sai tài khoản hoặc mật khẩu");
            }
        }
    }
}
