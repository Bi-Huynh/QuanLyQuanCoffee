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
    /// Interaction logic for frmQuanLyKetCa.xaml
    /// </summary>
    public partial class frmQuanLyKetCa : Page
    {
        private List<KetCa> listDSKetCa;
        public frmQuanLyKetCa()
        {
            InitializeComponent();
            listDSKetCa = new List<KetCa>();
            listDSKetCa = CCa_BUS.toList();
            hienthiDSKetCa(listDSKetCa);
        }
        public void hienthiDSKetCa(List<KetCa> listKetCa)
        {
            if (listKetCa.Count() > 0)
            {
                try
                {
                    dgDSKetCa.ItemsSource = listKetCa.Select(x => new
                    {
                        maKetCa = x.maKetCa,
                        maNhanVien = x.maNhanVien,
                        tenNhanVien = x.NhanVien.hoNhanVien + " " + x.NhanVien.tenNhanVien,
                        gioBatDau = x.gioBatDau.Value.ToString("hh:MM:ss"),
                        gioKetThuc = x.gioKetThuc.Value.ToString("hh:MM:ss"),
                        ngayLap = x.ngayLap.Value.ToString("dd/MM/yyyy"),
                        soLuong = x.soLuong,
                        tienDauCa = x.tienDauCa,
                        tongTienBan = x.tongTienBan,
                        tongDoanhThu = x.tongDoanhThu
                    });
                }
                catch (FormatException)
                {
                    MessageBox.Show("Sai định dạng giờ");
                }

            }
        }
    }
}
