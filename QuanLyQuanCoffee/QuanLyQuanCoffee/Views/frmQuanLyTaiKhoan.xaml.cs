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
        private TaiKhoan tk;
        private QuanLyQuanCoffeeEntities1 dc = new QuanLyQuanCoffeeEntities1();

        public frmQuanLyTaiKhoan()
        {
            InitializeComponent();
            dc = new QuanLyQuanCoffeeEntities1();
            hienthiDStaikhoan();
        }
        public void hienthiDStaikhoan()
        {
            try
            {
                dgQltaikhoan.ItemsSource = CTaiKhoan_BUS.toList().Select(x => new
                {
                    maNhanVien = x.maNhanVien,
                    taiKhoan = x.taiKhoan1,
                    matKhau = x.matKhau,
                    tenLoaiTaiKhoan = x.LoaiTaiKhoan.tenLoaiTaiKhoan,
                    trangThai = x.trangThai == 0 ? "Mở khóa" : "Đã khóa"
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi: " + ex.Message);
            }
        }


        private void cboManhanvien_Loaded(object sender, RoutedEventArgs e)
        {

            try
            {
                cboManhanvien.ItemsSource = CLoaiTaiKhoan_BUS.toListTenLoai();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi: " + ex.Message);
            }
        }

        private void cboLoaitaikhoan_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                cboLoaitaikhoan.ItemsSource = CLoaiTaiKhoan_BUS.toListTenLoai();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi: " + ex.Message);
            }
        }

        private void btnThemTK_Click(object sender, RoutedEventArgs e)
        {
            if (cboManhanvien.SelectedItem == null)
            {
                MessageBox.Show("Bạn chưa chọn mã nhân viên cần cấp tài khoản");
                return;
            }
            if (cboLoaitaikhoan.SelectedItem == null)
            {
                MessageBox.Show("Bạn chưa chọn mã loại tài khoản");
                return;
            }
            else
            {
                try
                {
                    TaiKhoan tk1 = new TaiKhoan();
                    //CSanPham.saochep(sp1, sp)
                    tk1.maNhanVien = cboManhanvien.SelectedItem.ToString();
                    tk1.taiKhoan1 = txtTaikhoan.Text;
                    tk1.matKhau = txtMatkhau.Text;
                    tk1.maLoaiTaiKhoan = CLoaiTaiKhoan_BUS.findTen(cboLoaitaikhoan.SelectedItem.ToString()).maLoaiTaiKhoan;
                    tk1.trangThai = 0;
                    string makt = cboManhanvien.SelectedItem.ToString();
                    if (CServices.kiemTraThongTin(tk1))///Kiểm tra thông tin tài khoản hợp lệ
                    {
                        if (CTaiKhoan_BUS.find(makt) == null)//Kiểm tra tài khoản đã tồn tại chưa
                        {
                            CTaiKhoan_BUS.add(tk1);//Thêm tài khoản
                            MessageBox.Show("Thêm thành công");
                        }
                        else
                        {
                            if (CTaiKhoan_BUS.findTrangThai(tk1.maNhanVien))//kiểm tra  trạng thái=1(đã được cấp và đã bị xóa)
                            {
                                //MessageBox.Show("Nhân viên này đã có tài khoản nhưng đã được xóa, bạn có muốn phục hồi không","Thông báo",MessageBoxButton.YesNo);
                                if (CTaiKhoan_BUS.KTtaiKhoanDaXoa(tk1) == "Yes")// tài khoản đã được xóa và hỏi người dùng có muốn phục hồi tài khoản cho nhân viên Không//Người dùng chọn "Yes"
                                {

                                    TaiKhoan tkphuchoi = CTaiKhoan_BUS.find(makt);
                                    if (CTaiKhoan_BUS.KTRong(tkphuchoi))
                                    {
                                        if (CTaiKhoan_BUS.hoiPhucTK(tkphuchoi))
                                        {
                                            MessageBox.Show("Nhân viên có mã: " + tk1.maNhanVien + " đã được khôi phục tài khoản");
                                            load();
                                            hienthiDStaikhoan();
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Lỗi không thể thay đổi thông tin tài khoản");
                                    }
                                    hienthiDStaikhoan();
                                    load();
                                }
                                else
                                {
                                    MessageBox.Show("Nhân viên có mã: " + tk1.maNhanVien + " không được hồi phục tài khoản");
                                }
                            }
                            else//trang thai tai khoản =0(tài khoản đang được sử dụng)
                            {
                                MessageBox.Show("Nhân viên này đã có tài khoản.");
                            }

                        }
                    }
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("Bạn chưa chọn mã nhân viên cần cấp tài khoản");
                }
            }

            hienthiDStaikhoan();
            load();
        }

        public void load()
        {
            txtTaikhoan.Text = "";
            txtMatkhau.Text = "";
            cboLoaitaikhoan.Text = "";
            cboManhanvien.Text = "";
            tk = null;
        }
        private void btnXoaTK_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (tk != null)
                {
                    if (CTaiKhoan_BUS.remove(tk))
                    {
                        MessageBox.Show("Xóa thành công");
                        hienthiDStaikhoan();
                    }
                    else
                    {
                        MessageBox.Show("Không thể xóa tài khoản");
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn tài khoản cần xóa");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi: " + ex.Message);
            }

            hienthiDStaikhoan();
            load();
        }

        private void btnSuaTK_Click(object sender, RoutedEventArgs e)
        {
            if (tk == null)
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần sửa");
            }
            else
            {
                TaiKhoan tkSua = new TaiKhoan();
                tkSua.maNhanVien = cboManhanvien.Text;
                tkSua.taiKhoan1 = txtTaikhoan.Text;
                tkSua.matKhau = txtMatkhau.Text;
                tkSua.maLoaiTaiKhoan = cboLoaitaikhoan.Text;
                tkSua.trangThai = 0;
                if (CTaiKhoan_BUS.KTRong(tkSua))
                {
                    if (CTaiKhoan_BUS.edit(tkSua))
                    {
                        MessageBox.Show("Sửa thành công");
                        hienthiDStaikhoan();
                    }
                }
                else
                {
                    MessageBox.Show("Yêu cầu điền đầy đủ thông tin tài khoản");
                }
            }

            hienthiDStaikhoan();
            load();
        }

        private void dgQltaikhoan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgQltaikhoan.SelectedItem != null)
            {
                tk = CTaiKhoan_BUS.find(dgQltaikhoan.SelectedValue.ToString());
                txtTaikhoan.Text = tk.taiKhoan1;
                txtMatkhau.Text = tk.matKhau;
                cboLoaitaikhoan.Text = tk.LoaiTaiKhoan.tenLoaiTaiKhoan;
                cboManhanvien.Text = tk.maNhanVien;
            }
            else
            {
                cboLoaitaikhoan.Text = "";
                cboManhanvien.Text = "";
                txtTaikhoan.Text = "";
                txtMatkhau.Text = "";
            }
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
    }
}
