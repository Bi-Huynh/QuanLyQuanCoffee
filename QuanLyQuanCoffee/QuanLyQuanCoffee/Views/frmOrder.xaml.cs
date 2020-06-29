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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLyQuanCoffee.Views
{
    /// <summary>
    /// Interaction logic for frmOrder.xaml
    /// </summary>
    public partial class frmOrder : Page
    {
        NhanVien nhanVienSelect;
        private QuanLyQuanCoffeeEntities1 dc = new QuanLyQuanCoffeeEntities1();
        private List<ChiTietHoaDon> chiTietHoaDons;

        public frmOrder(NhanVien nhanVien)
        {
            InitializeComponent();
            nhanVienSelect = nhanVien;
            HienthiSP();
            chiTietHoaDons = new List<ChiTietHoaDon>();
            taoma();
        }

        public void taoma()
        {
            txtMahoadon.Text = CServices.taoMa<HoaDon>(CHoaDon_BUS.toList());
        }
        //public string randomMa()
        //{
        //    Random random = new Random();
        //    string ma = "";
        //    for (int i = 0; i < 10; i++)
        //    {
        //        ma += Convert.ToString((char)random.Next(65, 90));
        //    }
        //    return ma;
        //}

        public void HienthiSP()
        {
            dgDanhsachsanpham.ItemsSource = CSanPham_BUS.toList();
        }

        public void hienthitheoListBOX(string maloai)
        {
            List<SanPham> listLoaiSP = CSanPham_BUS.hienthiTheoMa(maloai);
            if (listLoaiSP.Count() > 0)
            {
                dgDanhsachsanpham.ItemsSource = listLoaiSP;
            }
            else
            {
                MessageBox.Show("Không có loại sản phẩm nào");
            }

        }
        private void LstBoxLoaisanpham_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int dongthu = LstBoxLoaisanpham.SelectedIndex;
            if (dongthu < 0)
            {
                MessageBox.Show("Chưa có loại sản phẩm nào được cập nhập");
                return;
            }
            //string maLoai = dc.LoaiSanPhams.ToList()[dongthu].maLoaiSanPham;
            //string maLoai = CLoaiSanPham_BUS.DSLoaiSPtheoTen()[dongthu];
            string maLoai = CLoaiSanPham_BUS.layMaloaitheoSo(dongthu);
            hienthitheoListBOX(maLoai);
        }
        private void hienThiDSChiTietHD(List<ChiTietHoaDon> chiTietHoaDons)
        {
            if (chiTietHoaDons != null)
            {
                dgChitiethoadon.ItemsSource = chiTietHoaDons.Select(x => new
                {
                    maHoaDon = x.maHoaDon,
                    maSanPham = x.maSanPham,
                    tenSanPham = x.SanPham.tenSanPham,
                    soLuong = x.soLuong,
                    donGia = x.SanPham.donGia,
                    thanhTien = CChiTietHoaDon_BUS.tinhThanhTien(x)
                }).ToList();
                txtBoxTongtien.Text = tinhTongThanhTien(chiTietHoaDons).ToString();
            }
        }

        public double tinhTongThanhTien(List<ChiTietHoaDon> chiTietHoaDons)
        {
            double tongThanhTien = 0;
            chiTietHoaDons.ForEach(item => tongThanhTien += CChiTietHoaDon_BUS.tinhThanhTien(item));
            return tongThanhTien;
        }

        //public string taoMaTangDan()
        //{
        //    string ma = dc.ChiTietHoaDons.LastOrDefault().maChiTietHoaDon;
        //    if (ma == null)
        //    {
        //        ma = "Ct"+;
        //    }
        //    return ma;
        //}

        private void txtBoxNv_Loaded(object sender, RoutedEventArgs e)
        {
            txtBoxNv.Text = nhanVienSelect.hoNhanVien + " " + nhanVienSelect.tenNhanVien;
        }

        private void LstBoxLoaisanpham_Loaded(object sender, RoutedEventArgs e)
        {
            //LstBoxLoaisanpham.ItemsSource = dc.LoaiSanPhams.Select(x => x.tenLoai).ToList();//hien thi ra list ten do uong
            LstBoxLoaisanpham.ItemsSource = CLoaiSanPham_BUS.DSLoaiSPtheoTen();//hien thi ra list ten do uong
        }



        private void btnChonsanpham_Click(object sender, RoutedEventArgs e)
        {
            if (dgDanhsachsanpham.SelectedItem == null)
            {
                MessageBox.Show("Bạn chưa chọn sản phẩm");
                return;
            }
            SanPham sanPham = CSanPham_BUS.find(dgDanhsachsanpham.SelectedValue.ToString());
            if (sanPham == null)
            {
                MessageBox.Show("khong co san pham");
            }
            else
            {
                //string maChiTietHoaDon;
                //do
                //{
                //    maChiTietHoaDon = randomMa();
                //} while (dc.ChiTietHoaDons.Find(maChiTietHoaDon) != null);

                ChiTietHoaDon chiTietHoaDon = new ChiTietHoaDon();
                //chiTietHoaDon.maChiTietHoaDon = randomMa();
                //chiTietHoaDon.maHoaDon = txtMahoadon.Text;
                chiTietHoaDon.maSanPham = sanPham.maSanPham;
                chiTietHoaDon.soLuong = 1;
                chiTietHoaDon.SanPham = sanPham;
                if (chiTietHoaDons.Where(x => x.maSanPham == chiTietHoaDon.maSanPham).Count() == 0)
                {
                    chiTietHoaDons.Add(chiTietHoaDon);
                    hienThiDSChiTietHD(chiTietHoaDons);
                }
                else
                {
                    ChiTietHoaDon a = new ChiTietHoaDon();
                    a = chiTietHoaDons.Where(x => x.maSanPham == chiTietHoaDon.maSanPham).FirstOrDefault();
                    a.soLuong += 1;
                    hienThiDSChiTietHD(chiTietHoaDons);
                }
                //chiTietHoaDon.tinhThanhTien();

                HienthiSP();
            }

        }

        private void btnBochon_Click(object sender, RoutedEventArgs e)
        {
            if (dgChitiethoadon.SelectedItem == null)
            {
                return;
            }
            string maSanPham = dgChitiethoadon.SelectedValue.ToString();
            //MessageBox.Show(maSanPham);
            ChiTietHoaDon chiTietHoaDon = chiTietHoaDons.Where(x => x.maSanPham == maSanPham).FirstOrDefault();
            if (chiTietHoaDon == null)
            {
                MessageBox.Show("chua chon san pham trong chi tiet hoa don");
            }
            else
            {
                if (chiTietHoaDon.soLuong > 1)
                {
                    ChiTietHoaDon a = new ChiTietHoaDon();
                    a = chiTietHoaDons.Where(x => x.maSanPham == chiTietHoaDon.maSanPham).FirstOrDefault();
                    a.soLuong -= 1;
                    hienThiDSChiTietHD(chiTietHoaDons);
                }
                else
                {
                    chiTietHoaDons.Remove(chiTietHoaDon);
                    hienThiDSChiTietHD(chiTietHoaDons);
                }
            }
        }

        private void btnTinhtien_Click(object sender, RoutedEventArgs e)
        {
            if (chiTietHoaDons.Count() == 0)
            {
                MessageBox.Show("Hóa Đơn chưa có chi tiết hóa đơn");
                return;
            }
            if (CHoaDon_BUS.find(txtMahoadon.Text) == null)
            {
                try
                {
                    HoaDon hoaDon = new HoaDon();
                    hoaDon.maHoaDon = txtMahoadon.Text;
                    hoaDon.maNhanVien = nhanVienSelect.maNhanVien;
                    hoaDon.ngayLap = DateTime.Now;
                    hoaDon.tongThanhTien = double.Parse(txtBoxTongtien.Text);

                    hoaDon.trangThai = 0;

                    foreach (var item in chiTietHoaDons)
                    {
                        //item.HoaDon = hoaDon;
                        ChiTietHoaDon a = new ChiTietHoaDon();
                        a.maHoaDon = hoaDon.maHoaDon;
                        a.maSanPham = item.maSanPham;
                        a.soLuong = item.soLuong;
                        a.thanhTien = CChiTietHoaDon_BUS.tinhThanhTien(item);
                        hoaDon.ChiTietHoaDons.Add(a);
                    }

                    dc.HoaDons.Add(hoaDon);

                    //themChiTietHoaDon();
                    dc.SaveChanges();
                    taoma();

                    chiTietHoaDons.Clear();
                    hienThiDSChiTietHD(chiTietHoaDons);
                }
                catch (DbEntityValidationException)
                {
                    MessageBox.Show("loi kieu du lieu");
                }
                catch (DbUpdateException)
                {
                    MessageBox.Show("data k update");
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
                MessageBox.Show("Ma hoa don da ton tai");
            }

        }

        public void themChiTietHoaDon()
        {
            foreach (ChiTietHoaDon item in chiTietHoaDons)
            {
                dc.ChiTietHoaDons.Add(item);
                //dc.SaveChanges();
            }

        }
    }
}
