using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCoffee.BUS
{
    class CSanPham_BUS
    {
        private static QuanLyQuanCoffeeEntities quanLyQuanCoffee = new QuanLyQuanCoffeeEntities();

        public static SanPham find(string maSanPham)
        {
            return quanLyQuanCoffee.SanPhams.Find(maSanPham);
        }
    }
}
