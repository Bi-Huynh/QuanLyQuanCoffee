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
    /// Interaction logic for frmKetCa.xaml
    /// </summary>
    public partial class frmKetCa : Page
    {
        private CCa_BUS ca;
        private NhanVien nhanVien;
        private double tienBanDuoc = 0;
        public frmKetCa(CCa_BUS caLam = null, NhanVien nv = null)
        {
            InitializeComponent();

            ca = caLam;
            nhanVien = nv;
            if (ca == null)
            {
                ca = new CCa_BUS();
            }
            if (nv == null)
            {
                nhanVien = new NhanVien();
            }
            hienThiThongTin(ca);
        }

        private void hienThiThongTin(CCa_BUS ca)
        {
            DateTime gioKetThuc = DateTime.Now;
            txtMaNhanVien.Text = ca.MaNhanVien;
            txtTenNhanVien.Text = nhanVien.hoNhanVien + " " + nhanVien.tenNhanVien;
            txtGioBatDau.Text = String.Format("{0:hh:mm:ss tt}", ca.GioBatDau);
            txtGioKetThuc.Text = String.Format("{0:hh:mm:ss tt}", gioKetThuc);
            int soLuong = CHoaDon_BUS.demSoLuongBanDuoc(ca.GioBatDau, gioKetThuc);
            txtSoLuongBan.Text = soLuong.ToString();
            tienBanDuoc = CHoaDon_BUS.tongTienBan(ca.GioBatDau, gioKetThuc);
            txtTienBanDuoc.Text = String.Format("{0:#,###,0 VND;(#,###,0 VND);0 VND}", tienBanDuoc);
            txtSoTienBanDau.Text = "";
        }

        private void txtSoTienBanDau_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (txtSoTienBanDau.Text == null || txtSoTienBanDau.Text == "")
                {
                    txtTongDoanhThu.Text = String.Format("{0:#,###,0 VND;(#,###,0 VND);0 VND}", 0);
                }
                else
                {
                    double tienDauCa = double.Parse(txtSoTienBanDau.Text);
                    double tongDoanhThu = tienBanDuoc + tienDauCa;
                    txtTongDoanhThu.Text = String.Format("{0:#,###,0VND;(#,###,0 VND);0 VND}", tongDoanhThu);
                }
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Tiền đầu ca rỗng, Vui lòng nhập tiền đầu ca");
                return;
            }
            catch (FormatException)
            {
                MessageBox.Show("Tiền đầu ca phải là số nguyên dương");
                txtSoTienBanDau.Text = "";
                return;
            }
            catch (OverflowException)
            {
                MessageBox.Show("Tiền đầu ca quá lớn, không thể tính");
                return;
            }
        }
    }
}
