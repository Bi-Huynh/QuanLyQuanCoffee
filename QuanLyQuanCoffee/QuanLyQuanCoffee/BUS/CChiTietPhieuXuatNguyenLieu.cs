using QuanLyQuanCoffee.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCoffee.BUS
{
    class CChiTietPhieuXuatNguyenLieu
    {
        private static QuanLyQuanCoffeeEntities1 quanLyQuanCoffee = LoadDatabase.Instance();
        public static List<ChiTietPhieuXuat> toList(string maPhieuXuat)
        {
            List<ChiTietPhieuXuat> list = quanLyQuanCoffee.ChiTietPhieuXuats.Where(x => x.maPhieuXuat == maPhieuXuat).ToList();
            return list == null ? new List<ChiTietPhieuXuat>() : list;

        }
    }
}
