using QuanLyQuanCoffee.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCoffee.BUS
{
    class CChiTietHoaDon_BUS
    {
        private static QuanLyQuanCoffeeEntities1 quanLyQuanCoffee = new QuanLyQuanCoffeeEntities1();

        public static List<ChiTietHoaDon> toList(string maHoaDon)
        {
            List<ChiTietHoaDon> list = quanLyQuanCoffee.ChiTietHoaDons.Where(x => x.maHoaDon == maHoaDon).ToList();
            return list == null ? new List<ChiTietHoaDon>() : list;
        }
        public static ChiTietHoaDon find(string maHoaDon)
        {
            return quanLyQuanCoffee.ChiTietHoaDons.Find(maHoaDon);
        }

        public static double tinhThanhTien(ChiTietHoaDon chiTietHoaDon)
        {
            var thanhTien = chiTietHoaDon.soLuong * chiTietHoaDon.SanPham.donGia;
            return double.Parse(thanhTien.ToString());
        }
        public static bool  add(ChiTietHoaDon chiTietHoaDon)
        {
            if(CServices.kiemTraThongTin(chiTietHoaDon))
            {
                quanLyQuanCoffee.ChiTietHoaDons.Add(chiTietHoaDon);
            }
            return true;
        }
    }
}
