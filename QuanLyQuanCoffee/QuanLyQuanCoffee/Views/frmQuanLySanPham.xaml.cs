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
        private LoaiSanPham loaisanpham;
        private List<NguyenLieu> nguyenLieuThanhPhans;
        private int chosseTimKiem = 0;

        public frmQuanLySanPham()
        {
            InitializeComponent();
            hienthiSP();
            hienThiDSNguyenLieu();
            txtMasanpham.Text = CServices.taoMa<SanPham>(CSanPham_BUS.DsSanPham());
            nguyenLieuThanhPhans = new List<NguyenLieu>();
        }

        public void hienThiDSNguyenLieu()
        {
            lstThanhPhan.ItemsSource = CNguyenLieu_BUS.to_List().Select(x => new
            {
                tenNguyenLieu = x.tenNguyenLieu
            });
        }

        public void hienThiDSNguyenLieuDuocChon()
        {
            lstThanhPhanDuocChon.ItemsSource = nguyenLieuThanhPhans.Select(x => new
            {
                tenNguyenLieu = x.tenNguyenLieu
            });
        }

        public void hienthiSP()
        {
            try
            {
                List<SanPham> list = CSanPham_BUS.toList();
                if (list.Count > 0)
                {
                    dgQlsanpham.ItemsSource = list.Select(x => new
                    {
                        maSanPham = x.maSanPham,
                        tenSanPham = x.tenSanPham,
                        donViTinh = x.donViTinh,
                        donGia = String.Format("{0:#,###,0 VND;(#,###,0 VND);0 VND}", x.donGia),
                        tenLoaiSanPham = x.LoaiSanPham.tenLoai,
                        trangThai = x.trangThai == 0 ? "Mở" : "Khóa"
                    });
                }
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
                    cboLoaisanpham.ItemsSource = CSanPham_BUS.toListByMaTenLoaiSP();
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
                    nguyenLieuThanhPhans = new List<NguyenLieu>();
                    sanPhamSelect = CSanPham_BUS.find(dgQlsanpham.SelectedValue.ToString());
                    if (sanPhamSelect != null)
                    {
                        txtTensanpham.Text = sanPhamSelect.tenSanPham;
                        txtMasanpham.Text = sanPhamSelect.maSanPham;
                        txtDonvitinh.Text = sanPhamSelect.donViTinh;
                        txtDongia.Text = sanPhamSelect.donGia.ToString();
                        cboLoaisanpham.Text = sanPhamSelect.LoaiSanPham.tenLoai;

                        foreach (ThanhPhan thanhPhan in sanPhamSelect.ThanhPhans)
                        {
                            NguyenLieu nguyenLieu = new NguyenLieu();
                            nguyenLieu = CNguyenLieu_BUS.find(thanhPhan.maNguyenLieu);
                            nguyenLieuThanhPhans.Add(nguyenLieu);
                        }

                        hienThiDSNguyenLieuDuocChon();
                    }
                }
                else
                {
                    txtMasanpham.Text = "";
                    txtTensanpham.Text = "";
                    txtDonvitinh.Text = "";
                    txtDongia.Text = "";
                    cboLoaisanpham.Text = "";
                    nguyenLieuThanhPhans = new List<NguyenLieu>();
                    hienThiDSNguyenLieuDuocChon();
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
                if (cboLoaisanpham.SelectedItem != null)
                {
                    loaisanpham = CSanPham_BUS.findTen(cboLoaisanpham.SelectedItem.ToString());
                    sp1.maLoaiSanPham = loaisanpham.maLoaiSanPham;
                }
                else
                {
                    MessageBox.Show("Loại sản phẩm không được để trống");
                    return;
                }

                sp1.donGia = int.Parse(txtDongia.Text);
                sp1.trangThai = 0;

                if (nguyenLieuThanhPhans.Count() > 0)
                {
                    List<ThanhPhan> thanhPhans = new List<ThanhPhan>();
                    foreach (NguyenLieu nguyenLieu in nguyenLieuThanhPhans)
                    {
                        ThanhPhan thanhPhan = new ThanhPhan();
                        thanhPhan.maNguyenLieu = nguyenLieu.maNguyenLieu;
                        thanhPhan.maSanPham = sp1.maSanPham;
                        thanhPhan.trangThai = 0;
                    }
                    sp1.ThanhPhans = thanhPhans;
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn thành phần nguyên liệu có trong sản phẩm");
                    return;
                }

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
                                nguyenLieuThanhPhans = new List<NguyenLieu>();
                                hienThiDSNguyenLieuDuocChon();
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
                    MessageBox.Show("Thay đổi trạng thái thành công");
                    hienthiSP();
                    load();
                    nguyenLieuThanhPhans = new List<NguyenLieu>();
                    hienThiDSNguyenLieuDuocChon();
                }
                else
                {
                    MessageBox.Show("Không thể thay đổi, có lỗi");
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
                    loaisanpham = CSanPham_BUS.findTen(cboLoaisanpham.Text);
                    SanPham a = new SanPham();
                    a.maSanPham = txtMasanpham.Text;
                    a.tenSanPham = txtTensanpham.Text;
                    a.donViTinh = txtDonvitinh.Text;
                    a.maLoaiSanPham = loaisanpham.maLoaiSanPham;
                    a.donGia = int.Parse(txtDongia.Text);
                    a.trangThai = 0;

                    if (nguyenLieuThanhPhans.Count() > 0)
                    {
                        List<ThanhPhan> thanhPhans = new List<ThanhPhan>();
                        foreach (NguyenLieu nguyenLieu in nguyenLieuThanhPhans)
                        {
                            ThanhPhan thanhPhan = new ThanhPhan();
                            thanhPhan.maNguyenLieu = nguyenLieu.maNguyenLieu;
                            thanhPhan.maSanPham = a.maSanPham;
                            thanhPhan.trangThai = 0;
                            thanhPhans.Add(thanhPhan);
                        }
                        a.ThanhPhans = thanhPhans;
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng chọn thành phần nguyên liệu có trong sản phẩm");
                        return;
                    }

                    if (CSanPham_BUS.KTRong(a))
                    {
                        if (CSanPham_BUS.edit(a))
                        {
                            MessageBox.Show("Sửa thành công");
                            hienthiSP();
                            load();
                            nguyenLieuThanhPhans = new List<NguyenLieu>();
                            hienThiDSNguyenLieuDuocChon();
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
            nguyenLieuThanhPhans = new List<NguyenLieu>();
            hienThiDSNguyenLieuDuocChon();
        }

        public void load()
        {
            txtMasanpham.Text = CServices.taoMa<SanPham>(CSanPham_BUS.DsSanPham());
            txtDonvitinh.Text = null;
            cboLoaisanpham.Text = null;
            txtTensanpham.Text = "";
            txtDongia.Text = "";
            sanPhamSelect = null;
            nguyenLieuThanhPhans = new List<NguyenLieu>();
            hienThiDSNguyenLieuDuocChon();
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtTK.Text == "" || txtTK.Text == null)
            {
                hienthiSP();
                return;
            }

            switch (chosseTimKiem)
            {
                case 0:
                    HienTHiTK(txtTK.Text);
                    break;
                case 1:
                    dgQlsanpham.ItemsSource = CSanPham_BUS.toListTenSanPham(txtTK.Text).Select(x => new
                    {
                        maSanPham = x.maSanPham,
                        tenSanPham = x.tenSanPham,
                        donViTinh = x.donViTinh,
                        donGia = String.Format("{0:#,###,0 VND;(#,###,0 VND);0 VND}", x.donGia),
                        tenLoaiSanPham = x.LoaiSanPham.tenLoai,
                        trangThai = x.trangThai == 0 ? "Mở" : "Khóa"
                    });
                    break;
                case 2:
                    try
                    {
                        int donGia = int.Parse(txtTK.Text);
                        dgQlsanpham.ItemsSource = CSanPham_BUS.toListDonGia(donGia).Select(x => new
                        {
                            maSanPham = x.maSanPham,
                            tenSanPham = x.tenSanPham,
                            donViTinh = x.donViTinh,
                            donGia = String.Format("{0:#,###,0 VND;(#,###,0 VND);0 VND}", x.donGia),
                            tenLoaiSanPham = x.LoaiSanPham.tenLoai,
                            trangThai = x.trangThai == 0 ? "Mở" : "Khóa"
                        }); 
                    }
                    catch (ArgumentNullException)
                    {
                        MessageBox.Show("Đơn giá không được để null");
                    }
                    catch (FormatException)
                    {
                        MessageBox.Show("Đơn giá chỉ được nhập số");
                        txtTK.Text = "";
                    }
                    catch (OverflowException)
                    {
                        MessageBox.Show("Đơn giá có độ dài vượt giá mức cho phép");
                    }

                    break;
                default:
                    break;
            }
        }

        private void lstThanhPhan_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lstThanhPhan.SelectedItem != null)
            {
                NguyenLieu nguyenLieu = CNguyenLieu_BUS.findNguyenLieuByTen(lstThanhPhan.SelectedValue.ToString());
                nguyenLieuThanhPhans.Add(nguyenLieu);
                hienThiDSNguyenLieuDuocChon();
            }
        }

        private void lstThanhPhanDuocChon_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lstThanhPhanDuocChon.SelectedItem != null && nguyenLieuThanhPhans.Count() > 0)
            {
                NguyenLieu nguyenLieu = CNguyenLieu_BUS.findNguyenLieuByTen(lstThanhPhanDuocChon.SelectedValue.ToString());
                nguyenLieuThanhPhans.Remove(nguyenLieu);
                hienThiDSNguyenLieuDuocChon();
            }
        }

        private void cmbTimKiem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbTimKiem.SelectedItem != null)
            {
                chosseTimKiem = cmbTimKiem.SelectedIndex;
            }
        }
    }
}
