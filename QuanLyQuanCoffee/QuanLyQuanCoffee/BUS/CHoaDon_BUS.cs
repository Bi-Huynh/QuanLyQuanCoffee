using QuanLyQuanCoffee.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLyQuanCoffee.BUS
{
    class CHoaDon_BUS
    {
        private static QuanLyQuanCoffeeEntities1 quanLyQuanCoffee = new QuanLyQuanCoffeeEntities1();

        public static List<HoaDon> toList()
        {
            List<HoaDon> list = quanLyQuanCoffee.HoaDons.ToList();
            return list == null ? new List<HoaDon>() : list;
        }

        public static HoaDon find(string maHoaDon)
        {
            HoaDon hoaDon = quanLyQuanCoffee.HoaDons.Find(maHoaDon);
            return hoaDon;
        }
        public static List<HoaDon> DsHoaDon(DateTime gioBatDau, DateTime gioKetThuc)
        {
            List<HoaDon> list = new List<HoaDon>();
            foreach(var item in toList())
            {
                if (item.ngayLap.TimeOfDay >= gioBatDau.TimeOfDay && 
                    item.ngayLap.TimeOfDay <= gioKetThuc.TimeOfDay)
                {
                    list.Add(item);
                }
            }
            return list;
        }
        public static Boolean add(HoaDon hoaDon)
        {
            if (CServices.kiemTraThongTin(hoaDon))
            {
                try
                {
                    quanLyQuanCoffee.HoaDons.Add(hoaDon);
                    quanLyQuanCoffee.SaveChanges();

                }
                catch (DbUpdateException)
                {
                    MessageBox.Show("Lỗi! không thể lưu dữ liệu");
                    return false;
                }
                catch (DbEntityValidationException)
                {
                    MessageBox.Show("Lỗi! không thể lưu dữ liệu");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Xem lại đơn giá");
            }
            return true;
        }

        public static int demSoLuongBanDuoc(DateTime gioBatDau, DateTime gioKetThuc)
        {
            int result = 0;
            List<HoaDon> list = DsHoaDon(gioBatDau, gioKetThuc);
            if (list != null && list.Count() > 0)
            {
                foreach (var item in list)
                {
                    foreach (var i in item.ChiTietHoaDons)
                    {
                        result += i.soLuong.Value;
                    }
                }
            }
            return result;
        }

        public static double tongTienBan(DateTime gioBatDau, DateTime gioKetThuc)
        {
            double result = 0;
            List<HoaDon> list = DsHoaDon(gioBatDau, gioKetThuc);
            foreach(var item in list)
            {
                result += item.tongThanhTien;
            }
            return result;
        }
    }
}
