using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCoffee.BUS
{
    class CSanPham_BUS
    {
        private static QuanLyQuanCoffeeEntities2 quanLyQuanCoffee = new QuanLyQuanCoffeeEntities2();

        public static SanPham find(string maSanPham)
        {
            return quanLyQuanCoffee.SanPhams.Find(maSanPham);
        }
    }
}
