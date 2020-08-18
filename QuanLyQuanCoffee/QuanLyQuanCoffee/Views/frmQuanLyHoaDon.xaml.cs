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
    /// Interaction logic for frmQuanLyHoaDon.xaml
    /// </summary>
    public partial class frmQuanLyHoaDon : Page
    {
        HoaDon hoaDonSelected;
        public frmQuanLyHoaDon()
        {
            InitializeComponent();
            hienthiHoaDon();
        }
        public void hienthiHoaDon()
        {
            if (CHoaDon_BUS.toList().Count()>=0)
            {
                dgQlhoadon.ItemsSource = CHoaDon_BUS.toList();
            }
            else
            {

                MessageBox.Show("Hiện tại chưa có hóa đơn nào");
            }

        }
        private void gdQuanLyChitietHoaDon_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (hoaDonSelected != null)
                {
                    frmQuanLyChiTietHoaDon f = new frmQuanLyChiTietHoaDon(hoaDonSelected);
                    f.Show();
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn hóa đơn");
                }
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Bạn Chưa chọn hóa đơn");
            }
        }
        private void dgQlhoadon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgQlhoadon.SelectedItem == null)
            {
                MessageBox.Show("Hiện tại chưa có hóa đơn nào");
            }
            else
            {
                hoaDonSelected = CHoaDon_BUS.find(dgQlhoadon.SelectedItem.ToString());

            }
        }

        private void txtTK_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtTK.Text == "")
            {
                hienthiHoaDon();
                return;
            }

            // nếu combox tìm kiếm là 0 tức là tìm theo mã phiếu nhập

            HienThiTK(txtTK.Text);
        }
        public void HienThiTK(string maHoadon)
        {
            try
            {
                dgQlhoadon.ItemsSource = CHoaDon_BUS.toListTK(maHoadon);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi: " + ex.Message);
            }
        }
    }
}
