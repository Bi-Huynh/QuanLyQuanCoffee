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
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class frmAdmin : Window
    {
        public frmAdmin()
        {
            InitializeComponent();
        }

        private void gd_QuanLyNhanVien_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new frmQuanLyNhanVien();
            
        }

        private void Main_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {

        }
    }
}
