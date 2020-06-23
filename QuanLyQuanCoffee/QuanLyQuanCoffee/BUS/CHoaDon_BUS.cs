using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        //public static Boolean add(HoaDon hoaDon)
        //{
        //    if()
        //    quanLyQuanCoffee.HoaDons.Add(hoaDon);
        //    return true;
        //}
    }
}
