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
        public static List<ChiTietPhieuXuatNguyenLieu> toList(string maPhieuXuat)
        {
            List<ChiTietPhieuXuatNguyenLieu> list = quanLyQuanCoffee.ChiTietPhieuXuatNguyenLieux.Where(x => x.maPhieuXuat == maPhieuXuat).ToList();
            return list == null ? new List<ChiTietPhieuXuatNguyenLieu>() : list;
        }
    }
}
