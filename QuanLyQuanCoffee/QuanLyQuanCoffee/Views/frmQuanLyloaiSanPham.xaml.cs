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
    /// Interaction logic for frmQuanLyloaiSanPham.xaml
    /// </summary>
    public partial class frmQuanLyloaiSanPham : Page
    {
        //private CLoaiSanPham lsp = new CLoaiSanPham();
       
        LoaiSanPham a;
        public frmQuanLyloaiSanPham()
        {
            InitializeComponent();
            HienThiDSLoaiSanPham();
        }
        public void HienThiDSLoaiSanPham()
        {
            dgLoaisanpham.ItemsSource = CLoaiSanPham_BUS.toList();
            txtmaLoai.Text = CServices.taoMa<LoaiSanPham>(CLoaiSanPham_BUS.toList());
        }
        private void btnThemLoaiSP_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LoaiSanPham a = new LoaiSanPham();
                a.maLoaiSanPham = txtmaLoai.Text;
                a.tenLoai = txttenLoai.Text;
                string makt = txtmaLoai.Text;
                a.trangThai = 0;
                if(CLoaiSanPham_BUS.KTRong(a))
                {
                    if (CLoaiSanPham_BUS.find(makt) == null)
                    {
                        CLoaiSanPham_BUS.add(a);
                        txtmaLoai.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Mã Loại bị trùng!");
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập tên loại");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi: " + ex.Message);
            }
            HienThiDSLoaiSanPham();
        }

        private void btnXoaLoaiSP_Click(object sender, RoutedEventArgs e)
        {
            if (a != null)
            {


                try
                {
                    string maloai = dgLoaisanpham.SelectedValue.ToString();
                    if (maloai == null)
                    {
                        MessageBox.Show("Khong tim thay");
                    }
                    else
                    {
                        a = CLoaiSanPham_BUS.find(maloai);
                        if (CLoaiSanPham_BUS.remove(a))
                        {
                            MessageBox.Show("Xóa thành công " + maloai + " khỏi danh sách");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi: " + ex.Message);
                }
                HienThiDSLoaiSanPham();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa");
            }
        }

        private void btnSuaLoaiSP_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                if(a==null)
                {
                    MessageBox.Show("Vui lòng chọn Loại sản phẩm cần xóa");
                }
                else
                {
                    LoaiSanPham b = new LoaiSanPham();
                    b.maLoaiSanPham = txtmaLoai.Text;
                    b.tenLoai = txttenLoai.Text;
                    b.trangThai = 0;
                    if(CLoaiSanPham_BUS.KTRong(b))
                    {
                        if(CLoaiSanPham_BUS.edit(b))
                        {
                            MessageBox.Show("Sửa thành công");
                            HienThiDSLoaiSanPham();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Yêu cầu nhập đầy đủ thông tin sản phẩm");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi: " + ex.Message);
            }
            HienThiDSLoaiSanPham();
        }

        private void dgLoaisanpham_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dgLoaisanpham.SelectedItem != null)
                {
                    a = CLoaiSanPham_BUS.find(dgLoaisanpham.SelectedValue.ToString());
                    txtmaLoai.Text = a.maLoaiSanPham;
                    txttenLoai.Text = a.tenLoai;
                }
                else
                {
                    txtmaLoai.Text = CServices.taoMa<LoaiSanPham>(CLoaiSanPham_BUS.toList());
                    txttenLoai.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi: " + ex.Message);
            }
        }

        private void btnBoChon_Click(object sender, RoutedEventArgs e)
        {
            txtmaLoai.Text = CServices.taoMa<LoaiSanPham>(CLoaiSanPham_BUS.toList());
            txttenLoai.Text = "";
            a = null;
        }
    }
}
