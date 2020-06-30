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
        public static List<HoaDon> DsHoaDon()
        {
            List<HoaDon> list = quanLyQuanCoffee.HoaDons.ToList();
            return list == null ? new List<HoaDon>() : list;
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
    }
}
