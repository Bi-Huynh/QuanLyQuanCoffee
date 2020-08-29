using Microsoft.Win32;
using Prism.Services.Dialogs;
using QuanLyQuanCoffee.BUS;
using QuanLyQuanCoffee.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
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
using System.Xml;

namespace QuanLyQuanCoffee.Views
{
    /// <summary>
    /// Interaction logic for frmThongTinNhanVien.xaml
    /// </summary>
    public partial class frmThongTinNhanVien : Window
    {
        private NhanVien nhanVienSelect = null;
        private string urlAnh = "";

        public frmThongTinNhanVien(NhanVien nhanVien = null, int flag = 1)
        {
            InitializeComponent();
            cmbLoaiNhanVien.ItemsSource = CLoaiNhanVien_BUS.toListTenLoai();
            dateNgayVaoLam.SelectedDate = DateTime.Now;
            // khi người dùng nhấn thêm thì ấn nút sửa
            if (flag == 1)
            {
                txtMaNhanVien.Text = CServices.taoMa<NhanVien>(CNhanVien_BUS.toListAll());
                btnSua.IsEnabled = false;
                btnLuu.IsEnabled = false;
            }
            // khi người dùng nhấn nút sửa
            else if (flag == 2)
            {
                btnThem.IsEnabled = false;
                btnSua.IsEnabled = false;
            }
            // là khi người dùng bấm nút xem chi tiết
            else
            {
                btnThem.IsEnabled = false;
                btnLuu.IsEnabled = false;
                btnChosse.IsEnabled = false;
                isEnabledThongTin(false);
            }
            if (nhanVien != null)
            {
                nhanVienSelect = nhanVien;
                hienThiThongTin(nhanVienSelect);
            }
        }

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
            cmbTrangThai.SelectedIndex = nhanVien.trangThai.Value;
            urlAnh = nhanVien.urlAnh;
            hienThiHinh(nhanVien.urlAnh);
        }

        private void isEnabledThongTin(bool value)
        {
            txtHoNhanVien.IsEnabled = value;
            txtTenNhanVien.IsEnabled = value;
            cmbLoaiNhanVien.IsEnabled = value;
            dateNgayVaoLam.IsEnabled = value;
            cmbPhai.IsEnabled = value;
            txtSoDienThoai.IsEnabled = value;
            dateNgaySinh.IsEnabled = value;
            txtCMND.IsEnabled = value;
            txtThuongTru.IsEnabled = value;
            txtTamTru.IsEnabled = value;
        }

        private void hienThiHinh(string url)
        {
            if (url != "" && url != null)
            {
                try
                {
                    Uri uri = new Uri(url);
                    imgAnh.Source = new BitmapImage(uri);
                }
                catch (ArgumentNullException)
                {
                    MessageBox.Show("Đường dẫn ảnh rỗng");
                }
                catch (FileNotFoundException)
                {
                    MessageBox.Show("Lỗi! đường dẫn, không thể mở ảnh");
                }
            }
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dateNgaySinh.SelectedDate.Value != null &&
                dateNgayVaoLam.SelectedDate.Value != null &&
                cmbLoaiNhanVien.SelectedItem.ToString() != null)
                {
                    NhanVien nhanVien = new NhanVien();
                    nhanVien.maNhanVien = txtMaNhanVien.Text;
                    nhanVien.hoNhanVien = txtHoNhanVien.Text;
                    nhanVien.tenNhanVien = txtTenNhanVien.Text;
                    nhanVien.soDienThoai = txtSoDienThoai.Text;
                    nhanVien.ngaySinh = dateNgaySinh.SelectedDate.Value.Date;
                    nhanVien.phai = cmbPhai.SelectedIndex == 0 ? true : false;
                    nhanVien.cMND = txtCMND.Text;
                    nhanVien.thuongTru = txtThuongTru.Text;
                    nhanVien.tamTru = txtTamTru.Text;
                    nhanVien.ngayVaoLam = dateNgayVaoLam.SelectedDate.Value.Date;
                    nhanVien.maLoaiNhanVien = CLoaiNhanVien_BUS.findMaLoaiByTenLoai(cmbLoaiNhanVien.SelectedItem.ToString());
                    nhanVien.urlAnh = urlAnh;
                    nhanVien.trangThai = cmbTrangThai.SelectedIndex;

                    if (CNhanVien_BUS.add(nhanVien))
                    {
                        MessageBox.Show("Thêm thành công!");
                        this.Close();
                    }
                }
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Lỗi! Không được để dữ liệu nhập vào là rỗng");
            }
        }

        // sự kiện tính tuổi sau khi người dùng chọn ngày sinh của minh
        private void dateNgaySinh_CalendarClosed(object sender, RoutedEventArgs e)
        {
            int tuoi = CNhanVien_BUS.tinhTuoi(dateNgaySinh.SelectedDate.Value);
            if (tuoi == -1)
            {
                MessageBox.Show("Tuổi được đi làm là từ 18 đến 65 tuổi");
            }
            else
            {
                txtTuoi.Text = tuoi.ToString();
            }
        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            btnLuu.IsEnabled = true;
            btnChosse.IsEnabled = true;
            isEnabledThongTin(true);
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Do you want to save changes?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                NhanVien nhanVien = new NhanVien();
                nhanVien.maNhanVien = txtMaNhanVien.Text;
                nhanVien.hoNhanVien = txtHoNhanVien.Text;
                nhanVien.tenNhanVien = txtTenNhanVien.Text;
                nhanVien.soDienThoai = txtSoDienThoai.Text;
                nhanVien.ngaySinh = dateNgaySinh.SelectedDate.Value.Date;
                nhanVien.phai = cmbPhai.SelectedIndex == 0 ? true : false;
                nhanVien.cMND = txtCMND.Text;
                nhanVien.thuongTru = txtThuongTru.Text;
                nhanVien.tamTru = txtTamTru.Text;
                nhanVien.ngayVaoLam = dateNgayVaoLam.SelectedDate.Value.Date;
                nhanVien.maLoaiNhanVien = CLoaiNhanVien_BUS.findMaLoaiByTenLoai(cmbLoaiNhanVien.SelectedItem.ToString());
                nhanVien.urlAnh = urlAnh;
                nhanVien.trangThai = cmbTrangThai.SelectedIndex;
                if (nhanVien.trangThai == 2)
                {
                    if (CTaiKhoan_BUS.khoaTaiKhoanNV(nhanVien.maNhanVien) == false)
                    {
                        return;
                    }
                }

                if (CNhanVien_BUS.edit(nhanVien))
                {
                    this.Close();
                }
            }
        }

        private void btnChosse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Chọn ảnh";
            openFileDialog.Filter = "Image files (*.png;*.jpg)|*.png;*.jpg";
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == true)
            {
                urlAnh = openFileDialog.FileName;

                hienThiHinh(urlAnh);
            }
        }

        private void cmbLoaiNhanVien_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var index = cmbLoaiNhanVien.SelectedIndex;
            //txtLuong.Text = CLoaiNhanVien_BUS.toList()[index].luongCoBan.ToString();
        }

        private void dateNgaySinh_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.ToString() == "Tab" || e.Key == Key.Enter)
            {
                try
                {
                    int tuoi = CNhanVien_BUS.tinhTuoi(dateNgaySinh.SelectedDate.Value);
                    if (tuoi == -1)
                    {
                        MessageBox.Show("Tuổi được đi làm là từ 18 đến 65 tuổi");
                    }
                    else
                    {
                        txtTuoi.Text = tuoi.ToString();
                        string ngaySinh = dateNgaySinh.Text;
                        dateNgaySinh.SelectedDate = DateTime.Parse(ngaySinh);
                    }
                }
                catch (ArgumentNullException)
                {
                    MessageBox.Show("Ngày sinh rỗng, không thể tính được tuổi nhân viên");
                }
                catch (FormatException)
                {
                    MessageBox.Show("Lỗi Định dạng! Ngày sinh phải là: MM/dd/yyyy");
                }
            }
        }
    }
}
