﻿using QuanLyQuanCoffee.BUS;
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
                DateTime gioBatDau = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0);
                DateTime gioKetThuc = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 15, 0, 0);
                CCa_BUS.CaLamViec = new DTO.CCa_DTO(nhanVienSelect.maNhanVien, gioBatDau, gioKetThuc);
                this.Close();
            }
            else if (cmbCaLam.SelectedIndex == 1)
            {
                DateTime gioBatDau = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 15, 0, 0);
                DateTime gioKetThuc = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 21, 0, 0);
                CCa_BUS.CaLamViec = new DTO.CCa_DTO(nhanVienSelect.maNhanVien, gioBatDau, gioKetThuc);
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
