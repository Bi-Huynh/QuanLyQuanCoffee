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
    /// Interaction logic for frmXemHoaDonKetCa.xaml
    /// </summary>
    public partial class frmXemHoaDonKetCa : Window
    {
        private KetCa ketCaSelect;

        public frmXemHoaDonKetCa(KetCa ketCa = null)
        {
            InitializeComponent();
            if (ketCa == null)
            {
                ketCaSelect = new KetCa();
            }
            else
            {
                ketCaSelect = ketCa;
            }
        }

        private void dgHoaDonTrongNgay_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void dgHoaDonTrongNgay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
