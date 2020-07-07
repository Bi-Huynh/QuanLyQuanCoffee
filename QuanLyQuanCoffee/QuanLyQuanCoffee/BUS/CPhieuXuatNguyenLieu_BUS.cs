using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCoffee.BUS
{
    class CPhieuXuatNguyenLieu_BUS
    {
        private static QuanLyQuanCoffeeEntities1 quanLyQuanCoffee = new QuanLyQuanCoffeeEntities1();
        public static List<PhieuXuatNguyenLieu> toList()
        {
            List<PhieuXuatNguyenLieu> list = quanLyQuanCoffee.PhieuXuatNguyenLieux.Where(x => x.trangThai == 0).ToList();
            return list == null ? new List<PhieuXuatNguyenLieu>() : list;
        }
        public static PhieuXuatNguyenLieu find(string maPhieuXuat)
        {
            PhieuXuatNguyenLieu PhieuXuatNguyenLieu = quanLyQuanCoffee.PhieuXuatNguyenLieux.Find(maPhieuXuat);
            return PhieuXuatNguyenLieu;
        }

    }
}
