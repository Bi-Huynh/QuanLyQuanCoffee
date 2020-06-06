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
    /// Interaction logic for frmQuanLyNhanVien.xaml
    /// </summary>
    public partial class frmQuanLyNhanVien : Page
    {
        private QuanLyQuanCoffeeEntities quanLyQuanCoffee = new QuanLyQuanCoffeeEntities();
        private NhanVien nhanVienSelect;

        private void hienThiDSNhanVien()
        {
            dgDSNhanVien.ItemsSource = quanLyQuanCoffee.NhanViens.Select(x => new
            {
                maNhanVien = x.maNhanVien,
                hoNhanVien = x.hoNhanVien,
                tenNhanVien = x.tenNhanVien,
                tenLoai = x.LoaiNhanVien.tenLoai,
                phai = x.phai == true ? "Nam" : "Nữ",
                soDienThoai = x.soDienThoai,
                ngayVaoLam = x.ngayVaoLam
            }).ToList();
        }

        public frmQuanLyNhanVien()
        {
            InitializeComponent();
            nhanVienSelect = new NhanVien();

            hienThiDSNhanVien();
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            new frmThongTinNhanVien().Show();

        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            new frmThongTinNhanVien().Show();
        }

        private void btnXemThongTinChiTiet_Click(object sender, RoutedEventArgs e)
        {
            new frmThongTinNhanVien(nhanVienSelect).Show();
        }

        private void dgDSNhanVien_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgDSNhanVien.SelectedItem == null)
            {
                return;
            }
            var maNhanVien = dgDSNhanVien.SelectedItem.ToString().Substring(15, 10);
            nhanVienSelect = quanLyQuanCoffee.NhanViens.Find(maNhanVien);
        }

        private void btnRefesh_Click(object sender, RoutedEventArgs e)
        {
            hienThiDSNhanVien();
        }
    }
}
