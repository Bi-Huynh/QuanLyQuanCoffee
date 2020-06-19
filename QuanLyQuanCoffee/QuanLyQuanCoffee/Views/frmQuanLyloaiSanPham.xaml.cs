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
        private QuanLyQuanCoffeeEntities dc = new QuanLyQuanCoffeeEntities();
        LoaiSanPham a;
        public frmQuanLyloaiSanPham()
        {
            InitializeComponent();
            HienThiDSLoaiSanPham();
        }
        public void HienThiDSLoaiSanPham()
        {
            dgLoaisanpham.ItemsSource = dc.LoaiSanPhams.ToList();
            txtmaLoai.Text = CServices.taoMa<LoaiSanPham>(dc.LoaiSanPhams.ToList());
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
                if (dc.LoaiSanPhams.Find(makt) == null)
                {
                    dc.LoaiSanPhams.Add(a);
                    dc.SaveChanges();
                    txtmaLoai.Text = CServices.taoMa<LoaiSanPham>(dc.LoaiSanPhams.ToList());
                }
                else
                {
                    MessageBox.Show("Mã Loại bị trùng!");
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
                    LoaiSanPham a = dc.LoaiSanPhams.Find(maloai);
                    if (maloai == null)
                    {
                        MessageBox.Show("Khong tim thay");
                    }
                    else
                    {
                        if ((dc.SanPhams.Where(x => x.maLoaiSanPham == maloai).ToList().Count > 0) == false)
                        {
                            //MessageBox.Show("Bạn có chắc muốn xóa mã loại " + maloai + " khỏi danh sách sản phẩm Không?");
                            dc.LoaiSanPhams.Remove(a);
                            dc.SaveChanges();
                            MessageBox.Show("Xóa thành công " + maloai + " khỏi danh sách");
                        }
                        else
                        {
                            MessageBox.Show("Mã loại sản phẩm này tồn tại trong Danh sách sản phẩm");
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
                if ((dc.LoaiSanPhams.Find(txtmaLoai.Text) == null))
                {
                    MessageBox.Show("Bạn không thể sửa loại sản phẩm");
                }
                else
                {
                    LoaiSanPham a = dc.LoaiSanPhams.Find(txtmaLoai.Text);
                    a.tenLoai = txttenLoai.Text;
                    dc.SaveChanges();
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
                    a = dc.LoaiSanPhams.Find(dgLoaisanpham.SelectedValue.ToString());
                    txtmaLoai.Text = a.maLoaiSanPham;
                    txttenLoai.Text = a.tenLoai;
                }
                else
                {
                    txtmaLoai.Text = "";
                    txttenLoai.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi: " + ex.Message);
            }
        }
    }
}
