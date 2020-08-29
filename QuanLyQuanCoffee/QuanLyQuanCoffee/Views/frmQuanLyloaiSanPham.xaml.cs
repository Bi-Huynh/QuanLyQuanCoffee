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
        private QuanLyQuanCoffeeEntities1 dc = new QuanLyQuanCoffeeEntities1();


        LoaiSanPham a;
        public frmQuanLyloaiSanPham()
        {
            InitializeComponent();
            HienThiDSLoaiSanPham();
        }
        public void HienThiDSLoaiSanPham()
        {
            List<LoaiSanPham> list = CLoaiSanPham_BUS.toList();
            if (list.Count > 0)
            {
                dgLoaisanpham.ItemsSource = list.Select(x => new
                {
                    maLoaiSanPham = x.maLoaiSanPham,
                    tenLoai = x.tenLoai,
                    trangThai = x.trangThai == 0 ? "Mở" : "Khóa"
                });
            }
            else
            {
                MessageBox.Show("Danh sách loại sản phẩm rỗng, chưa có loại sản phẩm nào");
            }
            txtmaLoai.Text = CServices.taoMa<LoaiSanPham>(CLoaiSanPham_BUS.toList());
        }
        private void btnThemLoaiSP_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LoaiSanPham a = new LoaiSanPham();
                a.maLoaiSanPham = txtmaLoai.Text;
                a.tenLoai = txttenLoai.Text;
                a.trangThai = 0;
                string makt = txtmaLoai.Text;
                if (CLoaiSanPham_BUS.KTRong(a))
                {
                    if (CLoaiSanPham_BUS.find(makt) == null)
                    {

                        CLoaiSanPham_BUS.add(a);
                        MessageBox.Show("Thêm thành công");
                        txtmaLoai.Text = CServices.taoMa<LoaiSanPham>(CLoaiSanPham_BUS.DSLoaiSP());
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
            load();
            txttenLoai.Text = "";
            //btnBoChon_Click(sender, e);
        }

        private void btnXoaLoaiSP_Click(object sender, RoutedEventArgs e)
        {
            if (a != null)
            {
                try
                {
                    //string maloai = dgLoaisanpham.SelectedValue.ToString();
                    string maloai = a.maLoaiSanPham.ToString();
                    if (maloai == null)
                    {
                        MessageBox.Show("Không tìm thấy");
                    }
                    else
                    {
                        a = CLoaiSanPham_BUS.find(maloai);
                        if (CLoaiSanPham_BUS.remove(a))
                        {
                            if (CSanPham_BUS.thaydoiLoai(a))
                            {
                                MessageBox.Show("Đã thay đổi trạng thái " + maloai + " thành công.");
                                HienThiDSLoaiSanPham();
                                load();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi: " + ex.Message);
                }
                HienThiDSLoaiSanPham();
                load();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần thay đổi trạng thái");
            }
            HienThiDSLoaiSanPham();
            load();
        }

        private void btnSuaLoaiSP_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (a == null)
                {
                    MessageBox.Show("Vui lòng chọn Loại sản phẩm cần sửa");
                }
                else
                {
                    LoaiSanPham b = new LoaiSanPham();
                    b.maLoaiSanPham = txtmaLoai.Text;
                    b.tenLoai = txttenLoai.Text;
                    b.trangThai = 0;
                    if (CLoaiSanPham_BUS.KTRong(b))
                    {
                        if (CLoaiSanPham_BUS.edit(b))
                        {
                            MessageBox.Show("Sửa thành công");
                            HienThiDSLoaiSanPham();
                            load();
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
                    txtmaLoai.Text = CServices.taoMa<LoaiSanPham>(CLoaiSanPham_BUS.DSLoaiSP());
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
            txtmaLoai.Text = CServices.taoMa<LoaiSanPham>(CLoaiSanPham_BUS.DSLoaiSP());
            txttenLoai.Text = "";
            a = null;
        }
        public void load()
        {
            txtmaLoai.Text = CServices.taoMa<LoaiSanPham>(CLoaiSanPham_BUS.DSLoaiSP());
            a = null;
        }

    }
}
