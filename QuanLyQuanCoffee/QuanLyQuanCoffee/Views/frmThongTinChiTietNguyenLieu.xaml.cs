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
        public frmThongTinChiTietNguyenLieu(NguyenLieu nguyenLieu)
        {
            InitializeComponent();
            List<ChiTietNguyenLieu> list = CChiTietNguyenLieu_BUS.find(nguyenLieu.maNguyenLieu);
            txtMaNguyenLieu.Text = nguyenLieu.maNguyenLieu;
            txtTenLoai.Text = nguyenLieu.LoaiNguyenLieu.tenLoaiNguyenLieu;
            if (list.Count() > 0)
            {
                hienThi(list);
            }
        }

        public void hienThi(List<ChiTietNguyenLieu> list)
        {
            dgDSChiTietNguyenLieu.ItemsSource = list.Select(x => new
            {
                soLuong = x.soLuong,
                ngayHetHan = x.ngayHetHan,
                soNgayConLai = CChiTietNguyenLieu_BUS.soNgayConLai(x.ngayHetHan.Value),
                donViTinh = x.donViTinh
            });
        }
    }
}
