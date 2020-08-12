using QuanLyQuanCoffee.BUS;
using QuanLyQuanCoffee.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
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
    /// Interaction logic for frmXuatNguyenLieu.xaml
    /// </summary>
    public partial class frmXuatNguyenLieu : Window
    {
        NhanVien nhanVienSelected;
        private List<ChiTietPhieuXuat> chiTietPhieuXuats;
        private List<ChiTietPhieuNhap> chiTietPhieuNhaps;
        private QuanLyQuanCoffeeEntities1 dc = new QuanLyQuanCoffeeEntities1();
        int i = 0;
        ChiTietPhieuNhap chiTietPhieuNhapselect;

        public frmXuatNguyenLieu(NhanVien nhanVien)
        {
            InitializeComponent();
            nhanVienSelected = nhanVien;
            if (nhanVienSelected == null)
            {
                nhanVienSelected = new NhanVien();
            }
            chiTietPhieuXuats = new List<ChiTietPhieuXuat>();
            chiTietPhieuNhaps = CChiTietPhieuNhapNguyenLieu_BUS.toList();
            HienthiNguyenLieu(chiTietPhieuNhaps);
            taoMaPhieuXuat();
        }
        public void taoMaPhieuXuat()
        {
            txtMaPhieuXuat.Text = CServices.taoMa<PhieuXuatNguyenLieu>(CPhieuXuatNguyenLieu_BUS.toList());
        }
        public string taoMaChiTietPX()
        {
            string a = CServices.taoMa<ChiTietPhieuXuat>(CChiTietPhieuXuat_BUS.toList());
            return a;
        }
        private void LstBoxNguyenLieu_Loaded(object sender, RoutedEventArgs e)
        {
            LstBoxNguyenLieu.ItemsSource = CNguyenLieu_BUS.DSNguyenLieuTheoTen();
        }
        public void HienthiNguyenLieu(List<ChiTietPhieuNhap> list)
        {
            if (list.Count() > 0)
            {
                foreach (var item in list)
                {
                    if (item.ChiTietNguyenLieu == null)
                    {
                        item.ChiTietNguyenLieu = CChiTietNguyenLieu_BUS.findCT(item.maChitietNguyenLieu);
                    }
                    if (item.PhieuNhapNguyenLieu == null)
                    {
                        item.PhieuNhapNguyenLieu = CPhieuNhapNguyenLieu_BUS.find(item.maPhieuNhap);
                    }
                }
                dgChiTietNguyenLieu.ItemsSource = list.Select(x => new
                {
                    
                    maChiTietNguyenLieu = x.maChitietNguyenLieu.Substring(10),
                    tenNguyenLieu = CNguyenLieu_BUS.findTenByMaChiTietNguyenLieu(x.maChitietNguyenLieu),
                    donGia = x.donGia,
                    soLuong = x.soLuong,
                    ngayNhap = x.PhieuNhapNguyenLieu.ngayNhap.Value.ToString("dd/MM/yyyy"),
                    ngayHetHan = x.ChiTietNguyenLieu.ngayHetHan.Value.ToString("dd/MM/yyyy")    
                });
            }
        }

        private void txtBoxNv_Loaded(object sender, RoutedEventArgs e)
        {
            txtBoxNv.Text = nhanVienSelected.hoNhanVien + " " + nhanVienSelected.tenNhanVien;
        }
        public void hienthitheoListBOX(string maloai)
        {
            List<ChiTietNguyenLieu> listNguyenLieu = CNguyenLieu_BUS.hienthiTheoNL(maloai);
            if (listNguyenLieu.Count() > 0)
            {
                dgChiTietNguyenLieu.ItemsSource = listNguyenLieu;
            }
            else
            {
                MessageBox.Show("Không có nguyên liệu phẩm nào");
            }

        }

        private void LstBoxNguyenLieu_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //int dongthu = LstBoxNguyenLieu.SelectedIndex;
            //if (dongthu < 0)
            //{
            //    MessageBox.Show("Chưa có loại Nguyên liệu nào được cập nhập");
            //    return;
            //}
            ////string maLoai = dc.LoaiSanPhams.ToList()[dongthu].maLoaiSanPham;
            ////string maLoai = CLoaiSanPham_BUS.DSLoaiSPtheoTen()[dongthu];
            //string maLoai = CNguyenLieu_BUS.layMaloaitheoSo(dongthu);
            //hienthitheoListBOX(maLoai);

            string tenNguyenLieu = LstBoxNguyenLieu.SelectedItem.ToString();
            chiTietPhieuNhaps = new List<ChiTietPhieuNhap>();
            chiTietPhieuNhaps = CChiTietPhieuNhapNguyenLieu_BUS.toListTenNguyenLieu(tenNguyenLieu);
            HienthiNguyenLieu(chiTietPhieuNhaps);
        }

        private void btnChonNL_Click(object sender, RoutedEventArgs e)
        {
           

            if (dgChiTietNguyenLieu.SelectedItem == null)
            {
                MessageBox.Show("Bạn chưa chọn chi tiết Nguyên Liệu cần xuất");
                return;
            }
          
            int index = dgChiTietNguyenLieu.SelectedIndex;
            chiTietPhieuNhapselect = new ChiTietPhieuNhap();
             chiTietPhieuNhapselect = chiTietPhieuNhaps[index] as ChiTietPhieuNhap;
            if (chiTietPhieuNhapselect == null)
            {
                MessageBox.Show("Không có chi tiết phiếu nhập");
            }
            else
            {

                ChiTietPhieuXuat chitietPhieuXuat = new ChiTietPhieuXuat();
                if(i==0)
                {
                    chitietPhieuXuat.maChiTietPhieuXuat = taoMaChiTietPX();
                    i = 1;
                }
                else
                {
                    chitietPhieuXuat.maChiTietPhieuXuat = CServices.taoMa<ChiTietPhieuXuat>(chiTietPhieuXuats);
                }
                chitietPhieuXuat.maChitietNguyenLieu = chiTietPhieuNhapselect.maChitietNguyenLieu;
                chitietPhieuXuat.soLuong = chiTietPhieuNhapselect.soLuong;
                chitietPhieuXuat.donGia = chiTietPhieuNhapselect.donGia;
                chitietPhieuXuat.thanhTien = chiTietPhieuNhapselect.thanhTien;
                chitietPhieuXuat.maPhieuXuat = txtMaPhieuXuat.Text;
                chitietPhieuXuat.ChiTietNguyenLieu = chiTietPhieuNhapselect.ChiTietNguyenLieu;
                chiTietPhieuXuats.Add(chitietPhieuXuat);
                dgChitietPhieuXuat.ItemsSource = chiTietPhieuXuats.Select(x => new
                {
                    maChiTietNguyenLieu = x.maChitietNguyenLieu.Substring(0, 13),
                    tenNguyenLieu = x.ChiTietNguyenLieu.NguyenLieu.tenNguyenLieu.Trim(),
                    soLuong = x.soLuong,
                    donGia = x.donGia,
                    thanhTien = x.thanhTien
                });
                txtBoxTongtien.Text = tinhTongThanhTien(chiTietPhieuXuats).ToString();
            }
            

        }

        private double tinhTongThanhTien(List<ChiTietPhieuXuat> list)
        {
            double tongThanhTien = 0;
            list.ForEach(x => tongThanhTien += x.thanhTien.Value);
            return tongThanhTien;
        }

        private void hienThiDSChiTietPX(List<ChiTietPhieuXuat> chiTietPhieuXuats)
        {
            if (chiTietPhieuXuats != null)
            {
                dgChitietPhieuXuat.ItemsSource = chiTietPhieuXuats.Select(x => new
                {

                    maChiTietPhieuXuat = x.maChiTietPhieuXuat,
                    maChitietNguyenLieu = x.maChitietNguyenLieu,
                    soLuong = x.soLuong,
                    donGia = x.donGia,
                    thanhTien = x.thanhTien
                }).ToList();
            }
        }
        private void btnXuatNguyenLieu_Click(object sender, RoutedEventArgs e)
        {
            if (chiTietPhieuXuats.Count() == 0)
            {
                MessageBox.Show("Hóa Đơn chưa có chi tiết Phiếu Xuất");
                return;
            }
            if (CPhieuXuatNguyenLieu_BUS.find(txtMaPhieuXuat.Text) == null)
            {
                try
                {
                    PhieuXuatNguyenLieu phieuXuat = new PhieuXuatNguyenLieu();
                    phieuXuat.maPhieuXuat = txtMaPhieuXuat.Text;
                    phieuXuat.ngayXuat = DateTime.Now;
                    phieuXuat.tongThanhTien = double.Parse(txtBoxTongtien.Text);
                    phieuXuat.maNhanVien = nhanVienSelected.maNhanVien;
                    //phieuXuat.NhanVien = nhanVienSelected;
                    phieuXuat.trangThai = 0;

                    foreach (var item in chiTietPhieuXuats)
                    {
                        //item.HoaDon = hoaDon;
                        ChiTietPhieuXuat ctPX = new ChiTietPhieuXuat();
                        ctPX.maChiTietPhieuXuat = item.maChiTietPhieuXuat;
                        ctPX.maChitietNguyenLieu = item.maChitietNguyenLieu;
                        ctPX.soLuong = item.soLuong;
                        ctPX.donGia = item.donGia;
                        ctPX.thanhTien = item.thanhTien;
                        ctPX.maPhieuXuat = phieuXuat.maPhieuXuat;
                        //ctPX.ChiTietNguyenLieu = new ChiTietNguyenLieu();
                        //ctPX.ChiTietNguyenLieu = item.ChiTietNguyenLieu;
                        //ctPX.PhieuXuatNguyenLieu = item.PhieuXuatNguyenLieu;
                        phieuXuat.ChiTietPhieuXuats.Add(ctPX);
                        //CChiTietPhieuXuat_BUS.CapNhapSoLuong_PhieuNhap(ctPX);
                    }
                    //phieuXuat.NhanVien = nhanVienSelected;
                    dc.PhieuXuatNguyenLieux.Add(phieuXuat);
                    dc.SaveChanges();
                    MessageBox.Show("Xuất Nguyên Liệu Thành Công");
                    txtMaPhieuXuat.Text = CServices.taoMa<PhieuXuatNguyenLieu>(CPhieuXuatNguyenLieu_BUS.toList());
                    CChiTietPhieuXuat_BUS.CapNhapSoLuong_CTNguyenLieu(chiTietPhieuXuats);
                    CChiTietPhieuNhapNguyenLieu_BUS.CapNhapSoLuong_CTPhieuNhap(chiTietPhieuXuats);
                    //dc.SaveChanges();
                    chiTietPhieuXuats.Clear();
                    hienThiDSChiTietPX(chiTietPhieuXuats);
                    chiTietPhieuNhapselect = null;
                    i = 0;
                }
                catch (DbEntityValidationException)
                {
                    MessageBox.Show("Lỗi kiểu dữ liệu");
                }
                catch (DbUpdateException)
                {
                    MessageBox.Show("Không cập được dữ liệu");
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

            }
            else
            {
                MessageBox.Show("Mã Phiếu Xuất đã tồn tại");
            }
           
            
        }

        private void dgChiTietNguyenLieu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
