using QuanLyQuanCoffee.BUS;
using QuanLyQuanCoffee.DTO;
using QuanLyQuanCoffee.Properties;
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
    /// Interaction logic for frmKetCa.xaml
    /// </summary>
    public partial class frmKetCa : Window
    {
        private CCa_DTO ca;
        private NhanVien nhanVien;
        private KetCa ketCa;
        public frmKetCa(NhanVien nv = null)
        {
            InitializeComponent();

            ca = CCa_BUS.CaLamViec;
            nhanVien = nv;
            if (ca == null)
            {
                ca = new CCa_DTO();
            }
            if (nv == null)
            {
                nhanVien = new NhanVien();
            }

            ketCa = new KetCa();
            DateTime gioKetThuc = DateTime.Now;
            ketCa.maKetCa = CServices.taoMa<KetCa>(CCa_BUS.toList());
            ketCa.maNhanVien = nhanVien.maNhanVien;
            ketCa.gioBatDau = ca.GioBatDau;
            ketCa.gioKetThuc = gioKetThuc;
            ketCa.ngayLap = gioKetThuc;
            ketCa.soLuong = CHoaDon_BUS.DsHoaDon(ca.GioBatDau, gioKetThuc).Count();
            double tongTienBan = CHoaDon_BUS.tongTienBan(ca.GioBatDau, gioKetThuc);
            ketCa.tongTienBan = tongTienBan;
            ketCa.tienDauCa = 0;
            ketCa.tongDoanhThu = tongTienBan;

            hienThiThongTin(ketCa);
        }

        private void hienThiThongTin(KetCa ca)
        {
            txtMaNhanVien.Text = nhanVien.maNhanVien;
            txtTenNhanVien.Text = nhanVien.hoNhanVien + " " + nhanVien.tenNhanVien;
            txtGioBatDau.Text = String.Format("{0:hh:mm:ss tt}", ca.gioBatDau);
            txtGioKetThuc.Text = String.Format("{0:hh:mm:ss tt}", ca.gioKetThuc);
            txtTongSoHoaDon.Text = ca.soLuong.ToString();
            txtTienBanDuoc.Text = String.Format("{0:#,###,0 VND;(#,###,0 VND);0 VND}", ca.tongTienBan);
            txtTongDoanhThu.Text = String.Format("{0:#,###,0 VND;(#,###,0 VND);0 VND}", ca.tongDoanhThu);
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
                    double tongDoanhThu = ketCa.tongTienBan.Value + tienDauCa;
                    txtTongDoanhThu.Text = String.Format("{0:#,###,0VND;(#,###,0 VND);0 VND}", tongDoanhThu);
                    ketCa.tienDauCa = tienDauCa;
                    ketCa.tongDoanhThu = tongDoanhThu;
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

        private void btnKetCa_Click(object sender, RoutedEventArgs e)
        {
            if (ketCa != null)
            {
                if (CCa_BUS.add(ketCa))
                {
                    MessageBox.Show("Kết ca thành công");
                    this.Close();
                }
            }
        }

        private void btnKetCa_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnKetCa_Click(sender, e);
            }
        }
    }
}
