using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCoffee.BUS
{
    class CChiTietPhieuXuat_BUS
    {
        private static QuanLyQuanCoffeeEntities1 quanLyQuanCoffee = new QuanLyQuanCoffeeEntities1();
        public static List<ChiTietPhieuXuat> toList()
        {
            List<ChiTietPhieuXuat> list = quanLyQuanCoffee.ChiTietPhieuXuats.ToList();
            return list == null ? new List<ChiTietPhieuXuat>() : list;
        }
    }
}
