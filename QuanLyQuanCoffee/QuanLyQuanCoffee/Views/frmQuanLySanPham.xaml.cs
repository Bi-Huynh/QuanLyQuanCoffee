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
    /// Interaction logic for frmQuanLySanPham.xaml
    /// </summary>
    public partial class frmQuanLySanPham : Page
    {
        private SanPham sanPhamSelect;

        public frmQuanLySanPham()
        {
            InitializeComponent();
            hienthiSP();
            txtMasanpham.Text = CServices.taoMa<SanPham>(CSanPham_BUS.DsSanPham());
        }
        public void hienthiSP()
        {
            try
            {
                dgQlsanpham.ItemsSource = CSanPham_BUS.toList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi: " + ex.Message);
            }

        }
        public void HienTHiTK(string maSP)
        {
            try
            {
                dgQlsanpham.ItemsSource = CSanPham_BUS.toListTK(maSP);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi: " + ex.Message);
            }
        }
        private void cboLoaisanpham_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CSanPham_BUS.toListByMaSP().Count() <= 0)
                {
                    MessageBox.Show("Bạn chưa có loại sản phẩm nào, cần thêm loại sản phẩm trước");
                }
                else
                {
                    cboLoaisanpham.ItemsSource = CSanPham_BUS.toListByMaSP();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi: " + ex.Message);
            }
        }

        private void dgQlsanpham_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dgQlsanpham.SelectedItem != null)
                {
                    sanPhamSelect = CSanPham_BUS.find(dgQlsanpham.SelectedValue.ToString());
                    if (sanPhamSelect != null)
                    {
                        txtTensanpham.Text = sanPhamSelect.tenSanPham;
                        txtMasanpham.Text = sanPhamSelect.maSanPham;
                        txtDonvitinh.Text = sanPhamSelect.donViTinh;
                        txtDongia.Text = sanPhamSelect.donGia.ToString();
                        cboLoaisanpham.Text = sanPhamSelect.maLoaiSanPham;
                    }
                }
                else
                {
                    txtMasanpham.Text = "";
                    txtTensanpham.Text = "";
                    txtDonvitinh.Text = "";
                    txtDongia.Text = "";
                    cboLoaisanpham.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi: " + ex.Message);
            }
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SanPham sp1 = new SanPham();
                sp1.maSanPham = txtMasanpham.Text;
                sp1.tenSanPham = txtTensanpham.Text;
                sp1.donViTinh = txtDonvitinh.Text;
                //sp1.maLoaiSanPham = cboLoaisanpham.SelectedItem.ToString();
                if ((cboLoaisanpham.SelectedItem != null))
                {
                    sp1.maLoaiSanPham = cboLoaisanpham.SelectedItem.ToString();
                }
                else
                {
                    MessageBox.Show("Mã Loại sản phẩm không được để trống");
                }
                sp1.donGia = int.Parse(txtDongia.Text);
                sp1.trangThai = 0;
                if (CSanPham_BUS.KTRong(sp1))
                {
                    if (CServices.kiemTraThongTin(sp1))
                    {
                        if (CSanPham_BUS.find(txtMasanpham.Text) == null)
                        {
                            if (CSanPham_BUS.add(sp1))
                            {
                                MessageBox.Show("Thêm thành công");
                                txtMasanpham.Text = CServices.taoMa<SanPham>(CSanPham_BUS.DsSanPham());
                                hienthiSP();
                                load();
                            }

                        }
                        else
                        {
                            MessageBox.Show("Mã sản phẩm bị trùng");
                        }
                    }
                    else
                    {

                        MessageBox.Show("Xem lại đơn giá");

                    }
                }
                else
                {
                    MessageBox.Show("Yêu cầu nhập đầy đủ thông tin sản phẩm");
                }
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Không được để rỗng đơn giá");
            }
            catch (FormatException)
            {
                MessageBox.Show("Đơn giá phải là số");
            }
            catch (OverflowException)
            {
                MessageBox.Show("Đơn giá vượt quá giới hạn lưu trữ");
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Dữ liệu không được bỏ trống");
            }
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            if (sanPhamSelect != null)
            {
                if (CSanPham_BUS.remove(sanPhamSelect))
                {
                    MessageBox.Show("Xóa thành công");
                    hienthiSP();
                    load();
                }
                else
                {
                    MessageBox.Show("Sản phẩm đã tồn tại trong chi tiết hóa đơn");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sản phẩm");
            }
        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sanPhamSelect == null)
                {
                    MessageBox.Show("Vui lòng chọn sản phẩm");
                }
                else
                {
                    SanPham a = new SanPham();
                    a.maSanPham = txtMasanpham.Text;
                    a.tenSanPham = txtTensanpham.Text;
                    a.donViTinh = txtDonvitinh.Text;
                    a.maLoaiSanPham = cboLoaisanpham.Text;
                    a.donGia = int.Parse(txtDongia.Text);
                    a.trangThai = 0;
                    if (CSanPham_BUS.KTRong(a))
                    {
                        if (CSanPham_BUS.edit(a))
                        {
                            MessageBox.Show("Sửa thành công");
                            hienthiSP();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Yêu cầu nhập đầy đủ thông tin sản phẩm");
                    }

                }
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Không được để rỗng đơn giá");
            }
            catch (FormatException)
            {
                MessageBox.Show("Đơn giá phải là số");
            }
            catch (OverflowException)
            {
                MessageBox.Show("Đơn giá vượt quá giới hạn lưu trữ");
            }

            hienthiSP();
            load();
        }

        private void btnBochon_Click(object sender, RoutedEventArgs e)
        {
            txtMasanpham.Text = CServices.taoMa<SanPham>(CSanPham_BUS.DsSanPham());
            txtDonvitinh.Text = null;
            cboLoaisanpham.Text = null;
            txtTensanpham.Text = "";
            txtDongia.Text = "";
            sanPhamSelect = null;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }
        public void load()
        {
            txtMasanpham.Text = CServices.taoMa<SanPham>(CSanPham_BUS.DsSanPham());
            txtDonvitinh.Text = null;
            cboLoaisanpham.Text = null;
            txtTensanpham.Text = "";
            txtDongia.Text = "";
            sanPhamSelect = null;
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtTK.Text == "")
            {
                hienthiSP();
                return;
            }

            // nếu combox tìm kiếm là 0 tức là tìm theo mã phiếu nhập
           
            HienTHiTK(txtTK.Text);
           
            
        }
    }
}
