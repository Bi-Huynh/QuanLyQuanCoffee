﻿using QuanLyQuanCoffee.BUS;
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
            dgQlhoadon.ItemsSource = CHoaDon_BUS.toList();
        }
        private void gdQuanLyChitietHoaDon_Click(object sender, RoutedEventArgs e)
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

        private void dgQlhoadon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            hoaDonSelected = CHoaDon_BUS.find(dgQlhoadon.SelectedItem.ToString());
        }
    }
}
