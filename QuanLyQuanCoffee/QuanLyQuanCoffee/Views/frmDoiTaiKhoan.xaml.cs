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
    /// Interaction logic for frmDoiTaiKhoan.xaml
    /// </summary>
    public partial class frmDoiTaiKhoan : Window
    {
        private TaiKhoan taiKhoanSelect;

        public frmDoiTaiKhoan(TaiKhoan taiKhoan = null)
        {
            InitializeComponent();
            taiKhoanSelect = taiKhoan;
            if (taiKhoan == null)
            {
                taiKhoanSelect = new TaiKhoan();
            }
        }

        private void btnXacNhan_Click(object sender, RoutedEventArgs e)
        {
            if (taiKhoanSelect.matKhau == "1" && taiKhoanSelect.trangThai == 3)
            // Lần đầu đổi mật khẩu
            {
                if (CTaiKhoan_BUS.doiMatKhau(taiKhoanSelect, txtMatKhauMoi.Password))
                {
                    MessageBox.Show("Thay đổi mật khẩu thành công");
                    this.Close();
                }
            }
            else
            // Lần thứ 2 trở lên đổi mật khẩu
            {
                string matKhau = CTaiKhoan_BUS.Encrypt(txtMatKhauCu.Password);
                if (taiKhoanSelect.matKhau == matKhau)
                {
                    if (CTaiKhoan_BUS.doiMatKhau(taiKhoanSelect, txtMatKhauMoi.Password))
                    {
                        MessageBox.Show("Thay đổi mật khẩu thành công");
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Mật khẩu cũ không đúng. Vui lòng nhập lại");
                }
            }
        }
    }
}
