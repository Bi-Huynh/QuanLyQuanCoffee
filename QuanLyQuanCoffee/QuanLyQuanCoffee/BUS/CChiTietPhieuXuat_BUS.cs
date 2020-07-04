using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

        public static string findNgayXuat(string maChiTietNguyenLieu)
        {
            string ngayXuat = "NULL";
            var list = toList();
            foreach (var x in list)
            {
                try
                {
                    if (x.maChitietNguyenLieu == maChiTietNguyenLieu)
                    {
                        ngayXuat = x.PhieuXuatNguyenLieu.ngayXuat.Value.ToString("dd/MM/yyyy");
                        break;
                    }
                }
                catch (ArgumentNullException)
                {
                    MessageBox.Show("Không tìm thấy Phiếu xuất nguyên liệu trong chi tiết phiếu xuất");
                }
            }
            return ngayXuat;
        }
    }
}
