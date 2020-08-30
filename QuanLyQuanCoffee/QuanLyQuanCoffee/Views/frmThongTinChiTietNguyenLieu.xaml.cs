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
using System.Windows.Shapes;

namespace QuanLyQuanCoffee.Views
{
    /// <summary>
    /// Interaction logic for frmThongTinChiTietNguyenLieu.xaml
    /// </summary>
    public partial class frmThongTinChiTietNguyenLieu : Window
    {
        public frmThongTinChiTietNguyenLieu(NguyenLieu nguyenLieu = null)
        {
            InitializeComponent();
            //List<ChiTietPhieuNhap> list = CChiTietPhieuNhapNguyenLieu_BUS.findList(nguyenLieu.maNguyenLieu);

            List<ChiTietNguyenLieu> chiTietNguyenLieus = new List<ChiTietNguyenLieu>();
            if (nguyenLieu != null)
            {
                chiTietNguyenLieus = nguyenLieu.ChiTietNguyenLieux.ToList();
            }

            //foreach (ChiTietNguyenLieu chiTietNguyenLieu in chiTietNguyenLieus)
            //{
            //    chiTietNguyenLieu.ChiTietPhieuNhaps.ToList();
            //}

            txtMaNguyenLieu.Text = nguyenLieu.maNguyenLieu;
            txtTenNguyenLieu.Text = nguyenLieu.tenNguyenLieu;
            txtTenLoai.Text = nguyenLieu.LoaiNguyenLieu.tenLoaiNguyenLieu;

            if (chiTietNguyenLieus.Count() > 0)
            {
                hienThi(chiTietNguyenLieus);
            }
        }

        public void hienThi(List<ChiTietNguyenLieu> list)
        {
            dgDSChiTietNguyenLieu.ItemsSource = list.Select(x => new
            {
                soLuong = x.soLuong,
                ngayHetHan = x.ngayHetHan.Value.ToString("dd/MM/yyyy"),
                soNgayConLai = CChiTietNguyenLieu_BUS.soNgayConLai(x.ngayHetHan.Value),
                donGia = String.Format("{0:#,###,0 VND;(#,###,0 VND);0 VND}", x.ChiTietPhieuNhaps.FirstOrDefault().donGia),
                donViTinh = x.donViTinh,
                ngayNhap = x.ChiTietPhieuNhaps.FirstOrDefault().PhieuNhapNguyenLieu.ngayNhap.Value.ToString("dd/MM/yyyy"),
                ngayXuat = CChiTietPhieuXuat_BUS.findNgayXuat(x.maChiTietNguyenLieu)
            });
        }
    }
}
