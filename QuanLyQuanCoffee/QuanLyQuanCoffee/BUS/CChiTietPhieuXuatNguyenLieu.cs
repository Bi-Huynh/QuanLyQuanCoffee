using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCoffee.BUS
{
    class CChiTietPhieuXuatNguyenLieu
    {
        private static QuanLyQuanCoffeeEntities1 quanLyQuanCoffee = new QuanLyQuanCoffeeEntities1();
        public static List<ChiTietPhieuXuat> toList(string maPhieuXuat)
        {
            List<ChiTietPhieuXuat> list = quanLyQuanCoffee.ChiTietPhieuXuats.Where(x => x.maPhieuXuat == maPhieuXuat).ToList();
            return list == null ? new List<ChiTietPhieuXuat>() : list;

        }
    }
}
