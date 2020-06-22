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
            string ma = "";
            do
            {
                ma = randomMa();
            } while (dc.HoaDons.Find(ma) != null);
            txtMahoadon.Text = ma;
        }
        public string randomMa()
        {
            Random random = new Random();
            string ma = "";
            for (int i = 0; i < 10; i++)
            {
                ma += Convert.ToString((char)random.Next(65, 90));
            }
            return ma;
        }

        public void HienthiSP()
        {
            dgDanhsachsanpham.ItemsSource = dc.SanPhams.ToList();
        }

        public void hienthitheoListBOX(string maloai)
        {
            dgDanhsachsanpham.ItemsSource = dc.SanPhams.Where(x => x.maLoaiSanPham == maloai).ToList();
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
                    thanhTien = x.thanhTien
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
            LstBoxLoaisanpham.ItemsSource = dc.LoaiSanPhams.Select(x => x.tenLoai).ToList();//hien thi ra list ten do uong
        }

        private void LstBoxLoaisanpham_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int dongthu = LstBoxLoaisanpham.SelectedIndex;
            string maLoai = dc.LoaiSanPhams.ToList()[dongthu].maLoaiSanPham;
            hienthitheoListBOX(maLoai);
        }

        private void btnChonsanpham_Click(object sender, RoutedEventArgs e)
        {
            if (dgDanhsachsanpham.SelectedItem == null)
            {
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
                //chiTietHoaDon.tinhThanhTien();
                chiTietHoaDons.Add(chiTietHoaDon);
                hienThiDSChiTietHD(chiTietHoaDons);
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
            ChiTietHoaDon chiTietHoaDon = chiTietHoaDons.Where(x => x.maHoaDon == txtMahoadon.Text && x.maSanPham == maSanPham).FirstOrDefault();
            if (chiTietHoaDon == null)
            {
                MessageBox.Show("chua chon san pham trong chi tiet hoa don");
            }
            else
            {
                chiTietHoaDons.Remove(chiTietHoaDon);
                hienThiDSChiTietHD(chiTietHoaDons);
            }
        }

        private void btnTinhtien_Click(object sender, RoutedEventArgs e)
        {
            if (CHoaDon_BUS.find(txtMahoadon.Text) == null)
            {
                HoaDon hoaDon = new HoaDon();
                hoaDon.maHoaDon = txtMahoadon.Text;
                hoaDon.maNhanVien = nhanVienSelect.maNhanVien;
                hoaDon.ngayLap = DateTime.Now;
                hoaDon.tongThanhTien = double.Parse(txtBoxTongtien.Text);

                foreach (var item in chiTietHoaDons)
                {
                    //item.HoaDon = hoaDon;
                    ChiTietHoaDon a = new ChiTietHoaDon();
                    a.maHoaDon = hoaDon.maHoaDon;
                    a.maSanPham = item.maSanPham;
                    a.soLuong = item.soLuong;
                    a.thanhTien = 0;
                    hoaDon.ChiTietHoaDons.Add(a);
                }

                dc.HoaDons.Add(hoaDon);

                //themChiTietHoaDon();
                dc.SaveChanges();
                taoma();

                chiTietHoaDons.Clear();
                hienThiDSChiTietHD(chiTietHoaDons);
            }
            else
            {
                MessageBox.Show("Ma hoa don da ton tai");
            }
            //HoaDon hoaDon = new HoaDon(
            //    txtMahoadon.Text,
            //    nhanVienSelect.maNhanVien,
            //    DateTime.Now,
            //    double.Parse(txtBoxTongtien.Text),
            //    chiTietHoaDons
            //    );
            //dc.HoaDons.Add(hoaDon);
            //dc.SaveChanges();
            //taoma();

            //chiTietHoaDons.Clear();
            //hienThiDSChiTietHD(chiTietHoaDons);
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
