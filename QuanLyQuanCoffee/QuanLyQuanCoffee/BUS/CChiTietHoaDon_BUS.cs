using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCoffee.BUS
{
    class CChiTietHoaDon_BUS
    {
        private static QuanLyQuanCoffeeEntities quanLyQuanCoffee = new QuanLyQuanCoffeeEntities();
        public static ChiTietHoaDon find(string maHoaDon)
        {
            return quanLyQuanCoffee.ChiTietHoaDons.Find(maHoaDon);
        }

        public static double tinhThanhTien(ChiTietHoaDon chiTietHoaDon)
        {
            var thanhTien = chiTietHoaDon.soLuong * chiTietHoaDon.SanPham.donGia;
            return double.Parse(thanhTien.ToString());
        }
    }
}
