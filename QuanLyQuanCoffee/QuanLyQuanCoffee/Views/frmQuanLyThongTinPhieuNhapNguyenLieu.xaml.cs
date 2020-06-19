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
    /// Interaction logic for frmQuanLyThongTinPhieuNhapNguyenLieu.xaml
    /// </summary>
    public partial class frmQuanLyThongTinPhieuNhapNguyenLieu : Window
    {
        private PhieuNhapNguyenLieu phieuNhapNguyenLieuSelect;

        public frmQuanLyThongTinPhieuNhapNguyenLieu(PhieuNhapNguyenLieu phieuNhapNguyenLieu, int flag = 1)
        {
            InitializeComponent();
            hienThi();
            // khi người dùng nhấn thêm thì ấn nút sửa đi
            if (flag == 1)
            {
                //txtMaNguyenLieu.Text = CServices.taoMa<NguyenLieu>(CNguyenLieu_BUS.toList());
                //btnSua.IsEnabled = false;
                //btnLuu.IsEnabled = false;
            }
            // khi người dùng nhấn nút sửa
            else if (flag == 2)
            {
                //btnThem.IsEnabled = false;
                //btnSua.IsEnabled = false;
            }
            // là khi người dùng bấm nút xem chi tiết
            else
            {
                //btnThem.IsEnabled = false;
                //btnLuu.IsEnabled = false;
                isEnabledThongTin(false);
            }
            if (phieuNhapNguyenLieu != null)
            {
                phieuNhapNguyenLieuSelect = phieuNhapNguyenLieu;
                hienThiThongTin(phieuNhapNguyenLieuSelect);
            }
        }

        private void hienThi()
        {

        }

        private void hienThiThongTin(PhieuNhapNguyenLieu phieuNhapNguyenLieu)
        {
            
        }

        private void isEnabledThongTin(bool value)
        {

        }
    }
}
