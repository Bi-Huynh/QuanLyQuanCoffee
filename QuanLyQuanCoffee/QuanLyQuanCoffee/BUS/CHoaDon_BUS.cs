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
        public static HoaDon find(string maHoaDon)
        {
            return quanLyQuanCoffee.HoaDons.Find(maHoaDon);
        }

        public static bool add(HoaDon hoaDon)
        {

            return true;
        }
    }
}
