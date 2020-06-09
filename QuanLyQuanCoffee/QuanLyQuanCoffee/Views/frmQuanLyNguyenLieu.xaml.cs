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
    /// Interaction logic for frmQuanLyNguyenLieu.xaml
    /// </summary>
    public partial class frmQuanLyNguyenLieu : Page
    {
        public frmQuanLyNguyenLieu()
        {
            InitializeComponent();
            hienThiDS(CNguyenLieu_BUS.toList());
        }

        private void hienThiDS(List<NguyenLieu> list)
        {
            dgDSNguyenLieu.ItemsSource = list.Select(x => new
            {
                maNguyenLieu = x.maNguyenLieu,
                tenNguyenLieu = x.tenNguyenLieu,
                donGia = x.donGia,
                soLuong = x.soLuong,
                ngayHetHan = x.ngayHetHan.ToString("dd/MM/yyyy"),
                ngayNhap = x.ngayNhap.ToString("dd/MM/yyyy")
            });
        }

        private void CommandBinding_Executed_XoaNguyenLieu(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Đã xóa");
        }

        private void CommandBinding_CanExecute_XoaNguyenLieu(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            new frmThongTinNguyenLieu().Show();
        }

        private void btnRefesh_Click(object sender, RoutedEventArgs e)
        {
            hienThiDS(CNguyenLieu_BUS.toList());
        }
    }
}
