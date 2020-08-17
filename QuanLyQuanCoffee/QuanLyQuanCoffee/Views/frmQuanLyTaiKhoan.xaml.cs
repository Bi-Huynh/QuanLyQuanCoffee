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
        private QuanLyQuanCoffeeEntities1 dc = new QuanLyQuanCoffeeEntities1();

        public frmQuanLyTaiKhoan()
        {
            InitializeComponent();
            dc = new QuanLyQuanCoffeeEntities1();
            hienthiDStaikhoan();
        }

        public void hienthiDStaikhoan()
        {
            List<TaiKhoan> taiKhoans = new List<TaiKhoan>();
            foreach (TaiKhoan tk in CTaiKhoan_BUS.toList())
            {
                TaiKhoan taiKhoan = new TaiKhoan();
                taiKhoan.maNhanVien = tk.maNhanVien;
                taiKhoan.NhanVien = CNhanVien_BUS.find(tk.maNhanVien);
                taiKhoan.taiKhoan1 = tk.taiKhoan1;
                taiKhoan.matKhau = tk.matKhau;
                taiKhoan.maLoaiTaiKhoan = tk.maLoaiTaiKhoan;
                taiKhoan.LoaiTaiKhoan = CLoaiTaiKhoan_BUS.find(tk.maLoaiTaiKhoan);
                taiKhoan.trangThai = tk.trangThai;

                taiKhoans.Add(taiKhoan);
            }

            dgQltaikhoan.ItemsSource = taiKhoans.Select(x => new
            {
                maNhanVien = x.maNhanVien,
                tenNhanVien = x.NhanVien.hoNhanVien + " " + x.NhanVien.tenNhanVien,
                taiKhoan = x.taiKhoan1,
                matKhau = x.matKhau,
                tenLoaiTaiKhoan = x.LoaiTaiKhoan.tenLoaiTaiKhoan,
                trangThai = x.trangThai == 0 ? "Mở khóa" : "Đã khóa"
            });
        }

        private void btnThemTK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TaiKhoan taiKhoan = new TaiKhoan();
                string maNhanVien = CNhanVien_BUS.findTenbyMa(cmbTenNhanVien.SelectedItem.ToString());
                taiKhoan.maNhanVien = maNhanVien;
                //taiKhoan.NhanVien = CNhanVien_BUS.find(maNhanVien);
                if (taiKhoan.maNhanVien == null || taiKhoan.maNhanVien == "")
                {
                    MessageBox.Show("Không lấy được mã nhân viên");
                    return;
                }
                if ((txtTaiKhoan.Text == null || txtTaiKhoan.Text == "") && 
                    (txtMatKhau.Text == null || txtMatKhau.Text == "") && 
                    (cmbLoaiTaiKhoan.SelectedItem == null) && 
                    (cmbTenNhanVien.SelectedItem == null))
                {
                    MessageBox.Show("Điền đầy đủ thông tin tài khoản");
                    return;
                }
                taiKhoan.taiKhoan1 = txtTaiKhoan.Text;
                taiKhoan.matKhau = txtMatKhau.Text;
                taiKhoan.maLoaiTaiKhoan = CLoaiTaiKhoan_BUS.findTen(cmbLoaiTaiKhoan.SelectedItem.ToString()).maLoaiTaiKhoan;
                if (taiKhoan.maLoaiTaiKhoan == null || taiKhoan.maLoaiTaiKhoan == "")
                {
                    MessageBox.Show("Không lấy được mã loại tài khoản");
                    return;
                }
                taiKhoan.trangThai = 3;

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

        private void cmbLoaiTaiKhoan_Loaded(object sender, RoutedEventArgs e)
        {
            List<LoaiTaiKhoan> loaiNhanViens = CLoaiTaiKhoan_BUS.toList();
            cmbLoaiTaiKhoan.ItemsSource = loaiNhanViens.Select(x => x.tenLoaiTaiKhoan).ToList();
        }

        private void cmbTenNhanVien_Loaded(object sender, RoutedEventArgs e)
        {
            List<NhanVien> nhanViens = CNhanVien_BUS.toListNotAccount();
            cmbTenNhanVien.ItemsSource = nhanViens.Select(x => x.hoNhanVien + " " + x.tenNhanVien).ToList();
        }
    }
}
