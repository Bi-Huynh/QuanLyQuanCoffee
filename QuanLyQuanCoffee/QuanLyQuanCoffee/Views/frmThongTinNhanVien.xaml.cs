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
    /// Interaction logic for frmThongTinNhanVien.xaml
    /// </summary>
    public partial class frmThongTinNhanVien : Window
    {
        private QuanLyQuanCoffeeEntities quanLyQuanCoffee = new QuanLyQuanCoffeeEntities();
        private NhanVien nhanVienSelect = null;

        private void hienThiThongTin(NhanVien nhanVien)
        {
            txtMaNhanVien.Text = nhanVien.maNhanVien;
            txtHoNhanVien.Text = nhanVien.hoNhanVien;
            txtTenNhanVien.Text = nhanVien.tenNhanVien;
            dateNgayVaoLam.SelectedDate = nhanVien.ngayVaoLam;
            cmbLoaiNhanVien.SelectedItem = nhanVien.LoaiNhanVien.tenLoai;
            cmbPhai.SelectedIndex = nhanVien.phai == true ? 0 : 1;
            txtSoDienThoai.Text = nhanVien.soDienThoai;
            dateNgaySinh.SelectedDate = nhanVien.ngaySinh;
            txtThuongTru.Text = nhanVien.thuongTru;
            txtTamTru.Text = nhanVien.tamTru;
            txtCMND.Text = nhanVien.cMND;
            txtTuoi.Text = CNhanVien_BUS.tinhTuoi(nhanVien).ToString();
        }

        public frmThongTinNhanVien(NhanVien nhanVien = null)
        {
            InitializeComponent();
            if (nhanVien != null)
            {
                nhanVienSelect = nhanVien;  // không chắc chỗ này có copy được hk hay là bị trỏ chung địa chỉ
                hienThiThongTin(nhanVien);
            }
            cmbLoaiNhanVien.ItemsSource = quanLyQuanCoffee.LoaiNhanViens.Select(x => x.tenLoai ).ToList();
            dateNgayVaoLam.SelectedDate = DateTime.Now;
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            string maNhanVien = CNhanVien_BUS.randomMaNhanVien();
            string phai = cmbPhai.SelectedItem.ToString();

            if (quanLyQuanCoffee.NhanViens.Find(maNhanVien) == null)
            {
                NhanVien nhanVien = new NhanVien(
                                maNhanVien,
                                txtHoNhanVien.Text,
                                txtTenNhanVien.Text,
                                txtSoDienThoai.Text,
                                dateNgaySinh.SelectedDate.Value.Date,
                                phai.Substring(phai.Length - 3) == "Nam" ? true : false,
                                txtCMND.Text,
                                txtThuongTru.Text,
                                txtTamTru.Text,
                                dateNgayVaoLam.SelectedDate.Value.Date,
                                // so sánh cái tên của loại nhân viên và lấy ra cái mã
                                quanLyQuanCoffee.LoaiNhanViens.Where(x => cmbLoaiNhanVien.SelectedItem.ToString() == x.tenLoai).FirstOrDefault().maLoaiNhanvien
                                );
                if (CNhanVien_BUS.kiemTraThongTin(nhanVien))
                {
                    quanLyQuanCoffee.NhanViens.Add(nhanVien);
                    quanLyQuanCoffee.SaveChanges();
                    MessageBox.Show("Thêm thành công!");
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Thông tin không hợp lệ!");

            }
        }

        private void btnResest_Click(object sender, RoutedEventArgs e)
        {
            // thêm cái group box vào để biding dữ liệu lên
            if (nhanVienSelect == null)
            {
                txtMaNhanVien.Text = "";
                txtHoNhanVien.Text = "";
                txtTenNhanVien.Text = "";
                dateNgayVaoLam.SelectedDate = DateTime.Now;
                cmbLoaiNhanVien.SelectedIndex = 0;
                cmbPhai.SelectedIndex = 0;
                txtSoDienThoai.Text = "";
                dateNgaySinh.SelectedDate = DateTime.Now;
                txtThuongTru.Text = "";
                txtTamTru.Text = "";
                txtCMND.Text = "";
            } 
            else
            {
                hienThiThongTin(nhanVienSelect);
            }
        }

        private void dateNgaySinh_CalendarClosed(object sender, RoutedEventArgs e)
        {
            txtTuoi.Text = CNhanVien_BUS.tinhTuoi(dateNgaySinh.SelectedDate.Value).ToString();
        }
    }
}
