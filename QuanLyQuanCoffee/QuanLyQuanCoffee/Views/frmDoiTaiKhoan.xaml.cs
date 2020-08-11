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
            if (taiKhoan != null)
            {
                taiKhoanSelect = taiKhoan;
            }
        }

        private void btnXacNhan_Click(object sender, RoutedEventArgs e)
        {
            //if (taiKhoanSelect.matKhau == "1")
            //{
            //    if (CTaiKhoan_BUS.doiMatKhau(taiKhoanSelect, txtMatKhauMoi.Password))
            //    {
            //        MessageBox.Show("Thay đổi mật khẩu thành công");
            //        this.Close();
            //    };
            //}
            //else
            //{
            //    string matKhauCu = CTaiKhoan_BUS.maHoaMatKhau(txtMatKhauCu.Password);
            //    if (taiKhoanSelect.matKhau == matKhauCu)
            //    {
            //        if (CTaiKhoan_BUS.doiMatKhau(taiKhoanSelect, txtMatKhauMoi.Password))
            //        {
            //            MessageBox.Show("Thay đổi mật khẩu thành công");
            //            this.Close();
            //        };
            //    }
            //}
            MessageBox.Show("Đang fix");
        }
    }
}
