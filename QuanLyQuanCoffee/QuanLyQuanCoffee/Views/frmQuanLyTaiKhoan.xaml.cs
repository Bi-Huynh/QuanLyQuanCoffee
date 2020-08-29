using Prism.Services.Dialogs;
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
        public frmQuanLyTaiKhoan()
        {
            InitializeComponent();
            hienthiDStaikhoan();
        }

        public void hienthiDStaikhoan()
        {
            dgQltaikhoan.ItemsSource = CTaiKhoan_BUS.toListNotAdmin().Select(x => new
            {
                maTaiKhoan = x.maTaiKhoan,
                maNhanVien = x.maNhanVien,
                tenNhanVien = x.NhanVien.hoNhanVien + " " + x.NhanVien.tenNhanVien,
                taiKhoan = x.tenTaiKhoan,
                matKhau = x.matKhau,
                trangThai = x.trangThai == 0 ? "Mở khóa" : "Đã khóa"
            });
        }

        private void btnThemTK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtTaiKhoan.Text == "" || txtTaiKhoan.Text == null||
                    txtMatKhau.Text == "" || txtMatKhau.Text == null)
                {
                    MessageBox.Show("Yêu cầu điền thông tin tài khoản");
                    return;
                }

                TaiKhoan taiKhoan = new TaiKhoan();
                string maNhanVien = CNhanVien_BUS.findTenbyMa(cmbTenNhanVien.SelectedItem.ToString());
                taiKhoan.maNhanVien = maNhanVien;

                if (taiKhoan.maNhanVien == null || taiKhoan.maNhanVien == "")
                {
                    MessageBox.Show("Không lấy được mã nhân viên");
                    return;
                }
                if ((txtTaiKhoan.Text == null || txtTaiKhoan.Text == "") && 
                    (txtMatKhau.Text == null || txtMatKhau.Text == ""))
                {
                    MessageBox.Show("Điền đầy đủ thông tin tài khoản");
                    return;
                }
                if (CTaiKhoan_BUS.findTK(txtTaiKhoan.Text))
                {
                    MessageBox.Show("Tên tài khoản đã tồn tại");
                    return;
                }

                taiKhoan.tenTaiKhoan = txtTaiKhoan.Text;
                taiKhoan.matKhau = CTaiKhoan_BUS.Encrypt(txtMatKhau.Text);
                taiKhoan.maTaiKhoan = CServices.taoMa<TaiKhoan>(CTaiKhoan_BUS.toList());

                taiKhoan.trangThai = 0;

                if (CServices.kiemTraThongTin(taiKhoan))//Kiểm tra thông tin tài khoản hợp lệ
                {
                    CTaiKhoan_BUS.add(taiKhoan);//Thêm tài khoản
                    MessageBox.Show("Thêm thành công");
                }
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Bạn chưa chọn mã nhân viên cần cấp tài khoản");
            }

            hienthiDStaikhoan();
            load();
        }

        public void load()
        {
            txtTaiKhoan.Text = "";
            txtMatKhau.Text = "1";
        }

        private void btnKhoaTaiKhoan_Click(object sender, RoutedEventArgs e)
        {
            if (dgQltaikhoan.SelectedItem != null)
            {
                if (CTaiKhoan_BUS.khoaTaiKhoan(dgQltaikhoan.SelectedValue.ToString()))
                {
                    hienthiDStaikhoan();
                }
            }
        }

        private void btnMoTaiKhoan_Click(object sender, RoutedEventArgs e)
        {
            if (dgQltaikhoan.SelectedItem != null)
            {
                if (CTaiKhoan_BUS.moKhoaTaiKhoan(dgQltaikhoan.SelectedValue.ToString()))
                {
                    hienthiDStaikhoan();
                }
            }
        }

        private void cmbTenNhanVien_Loaded(object sender, RoutedEventArgs e)
        {
            List<NhanVien> nhanViens = CNhanVien_BUS.toListNotAccount();
            cmbTenNhanVien.ItemsSource = nhanViens.Select(x => x.hoNhanVien + " " + x.tenNhanVien).ToList();
        }
    }
}
