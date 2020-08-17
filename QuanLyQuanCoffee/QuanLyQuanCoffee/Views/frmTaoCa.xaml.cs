using QuanLyQuanCoffee.BUS;
using QuanLyQuanCoffee.DTO;
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
    /// Interaction logic for frmTaoCa.xaml
    /// </summary>
    public partial class frmTaoCa : Window
    {
        private NhanVien nhanVienSelect;

        public frmTaoCa(NhanVien nhanVien)
        {
            InitializeComponent();

            nhanVienSelect = nhanVien;
            if (nhanVienSelect == null)
            {
                nhanVienSelect = new NhanVien();
            }
        }

        private void btnTaoCa_Click(object sender, RoutedEventArgs e)
        {
            if (cmbCaLam.SelectedIndex == 0)
            {
                //if (DateTime.Now >= new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 6, 0, 0) &&
                //    DateTime.Now <= new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 10, 0, 0))
                //{
                //    DateTime gioBatDau = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0);
                //    DateTime gioKetThuc = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 15, 0, 0);
                //    CCa_BUS.CaLamViec = new DTO.CCa_DTO(nhanVienSelect.maNhanVien, gioBatDau, gioKetThuc);
                //    this.Close();
                //}
                //else
                //{
                //    MessageBox.Show("Không thể tạo ca 1, ca 1 chỉ có thể tạo từ 6am tới 10am");
                //}

                //DateTime gioBatDau = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0);
                //DateTime gioKetThuc = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 15, 0, 0);
                //CCa_BUS.CaLamViec = new DTO.CCa_DTO(nhanVienSelect.maNhanVien, gioBatDau, gioKetThuc);

                DateTime now = DateTime.Now;
                //DateTime gioBatDau = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
                DateTime gioBatDau = DateTime.Now;
                //DateTime gioKetThuc = new DateTime(now.Year, now.Month, now.Day, 15, 0, 0);
                CCa_BUS.CaLamViec = new DTO.CCa_DTO(nhanVienSelect.maNhanVien, gioBatDau);

                //CCa_BUS.isDaKetCa = true;
                this.Close();
            }
            else if (cmbCaLam.SelectedIndex == 1)
            {
                //if (DateTime.Now >= new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 13, 0, 0) &&
                //    DateTime.Now <= new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 0, 0))
                //{
                //    DateTime gioBatDau = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 15, 0, 0);
                //    DateTime gioKetThuc = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 21, 0, 0);
                //    CCa_BUS.CaLamViec = new DTO.CCa_DTO(nhanVienSelect.maNhanVien, gioBatDau, gioKetThuc);
                //    this.Close();
                //}
                //else
                //{
                //    MessageBox.Show("Không thể tạo ca 2, ca 2 chỉ có thể tạo từ 13am tới 17am");
                //}
                DateTime gioBatDau = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 15, 0, 0);
                DateTime gioKetThuc = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 21, 0, 0);
                CCa_BUS.CaLamViec = new DTO.CCa_DTO(nhanVienSelect.maNhanVien, gioBatDau, gioKetThuc);
                //CCa_BUS.isDaKetCa = true;
                this.Close();
            }
        }

        private void cmbCaLam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbCaLam.SelectedIndex == 0)
            {
                txtGioBatDau.Text = "08:00:00";
            }
            else if (cmbCaLam.SelectedIndex == 1)
            {
                txtGioBatDau.Text = "15:00:00";
            }
        }
    }
}
