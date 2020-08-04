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
    /// Interaction logic for frmQuanLyPhieuXuatNguyenLieu.xaml
    /// </summary>
    public partial class frmQuanLyPhieuXuatNguyenLieu : Page
    {
        NhanVien nhanVienSelected;
        private PhieuXuatNguyenLieu phieuXuatnguyenlieuSelect= new PhieuXuatNguyenLieu();

        public frmQuanLyPhieuXuatNguyenLieu(NhanVien nhanVien)
        {
            InitializeComponent();
            nhanVienSelected = nhanVien;
            hienThiPhieuXuat(CPhieuXuatNguyenLieu_BUS.toList());
        }
        //public void hienThiPhieuXuat()
        //{
        //    List<PhieuXuatNguyenLieu> list= CPhieuXuatNguyenLieu_BUS.toList();
        //    dgDSPhieuXuat.ItemsSource = list;
        //    //dgDSPhieuXuat.ItemsSource = list.Select(x => new
        //    //     {
        //    //         maPhieuXuat = x.maPhieuXuat,
        //    //         ngayXuat = x.ngayXuat.Value.ToString("dd/MM/yyyy"),
        //    //         tongThanhTien = x.tongThanhTien
        //    //     });
        //}
        public void hienThiPhieuXuat(List<PhieuXuatNguyenLieu> list)
        {
           
            //dgDSPhieuXuat.ItemsSource = list;
            dgDSPhieuXuat.ItemsSource = list.Select(x => new
                 {
                     maPhieuXuat = x.maPhieuXuat,
                     ngayXuat = x.ngayXuat.Value.ToString("dd/MM/yyyy"),
                     tongThanhTien = x.tongThanhTien
                 });
        }

        //private void hienThiDSPhieuNhap(List<PhieuNhapNguyenLieu> list)
        //{
        //    dgDSPhieuNhap.ItemsSource = list.Select(x => new
        //    {
        //        maPhieuNhap = x.maPhieuNhap,
        //        ngayNhap = x.ngayNhap.Value.ToString("dd/MM/yyyy"),
        //        tongThanhTien = String.Format("{0:#,###,0 VND;(#,###,0 VND);0 VND}", x.tongThanhTien)
        //    });
        //}
        private void txtTimKiem_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtTimKiem.Text == "")
            {
                hienThiPhieuXuat(CPhieuXuatNguyenLieu_BUS.toList());
                return;
            }

            // nếu combox tìm kiếm là 0 tức là tìm theo mã phiếu nhập
            if (cmbTimKiem.SelectedIndex == 0)
            {
                hienThiPhieuXuat(CPhieuXuatNguyenLieu_BUS.toListMa(txtTimKiem.Text));
            }
            else
            {
                try
                {
                    double tongThanhTien = double.Parse(txtTimKiem.Text);
                    hienThiPhieuXuat(CPhieuXuatNguyenLieu_BUS.toListTongThanhTien(tongThanhTien));
                }
                catch (ArgumentNullException)
                {
                    MessageBox.Show("Dữ liệu không được để rỗng");
                }
                catch (FormatException)
                {
                    MessageBox.Show("Dữ liệu phải là số");
                }
                catch (OverflowException)
                {
                    MessageBox.Show("Dữ liệu có độ lớn vượt quá giới hạn cho phép");
                }
            }
        }

        private void btnXemThongTinChiTiet_Click(object sender, RoutedEventArgs e)
        {
            frmThongTinChiTietPhieuXuatNL f = new frmThongTinChiTietPhieuXuatNL(phieuXuatnguyenlieuSelect);
            f.Show();

        }

        private void dgDSPhieuXuat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            phieuXuatnguyenlieuSelect = CPhieuXuatNguyenLieu_BUS.find(dgDSPhieuXuat.SelectedItem.ToString());
        }

        private void btnRefesh_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            if (phieuXuatnguyenlieuSelect != null)
            {
                var result = MessageBox.Show("Do you want to delete changes?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    if (CPhieuXuatNguyenLieu_BUS.remove(phieuXuatnguyenlieuSelect))
                    {
                        hienThiPhieuXuat(CPhieuXuatNguyenLieu_BUS.toList());
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn phiếu xuất");
            }
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            frmXuatNguyenLieu f = new frmXuatNguyenLieu(nhanVienSelected);
            f.Show();
        }

        private void cmbTimKiem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (cmbTimKiem.SelectedIndex == 1)
            //{
            //    txtTimKiem.Visibility = Visibility.Hidden;
               
            //}
            //else
            //{
            //    txtTimKiem.Visibility = Visibility.Visible;
               
            //}
        }
    }
}
