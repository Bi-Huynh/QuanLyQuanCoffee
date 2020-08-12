using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
            PhieuXuatNguyenLieu PhieuXuatNguyenLieu = quanLyQuanCoffee.PhieuXuatNguyenLieux.Where(x=>x.maPhieuXuat==maPhieuXuat).FirstOrDefault();
            return PhieuXuatNguyenLieu;/*==null?new PhieuXuatNguyenLieu():PhieuXuatNguyenLieu;*/
         
        }
        public static List<PhieuXuatNguyenLieu> toListTongThanhTien(double tongThanhTien)
        {
            List<PhieuXuatNguyenLieu> list = quanLyQuanCoffee.PhieuXuatNguyenLieux.
                Where(x => x.tongThanhTien >= tongThanhTien && x.trangThai == 0).ToList();

            return list == null ? new List<PhieuXuatNguyenLieu>() : list;
        }
        public static List<PhieuXuatNguyenLieu> toListMa(string maPhieuXuat)
        {
            List<PhieuXuatNguyenLieu> list = quanLyQuanCoffee.PhieuXuatNguyenLieux.
                Where(x => x.maPhieuXuat.Contains(maPhieuXuat) == true && x.trangThai == 0).ToList();
            return list == null ? new List<PhieuXuatNguyenLieu>() : list;
        }
        public static bool remove(string maPhieuXuat)
        {
            PhieuXuatNguyenLieu temp = find(maPhieuXuat);
            if (temp == null)
            {
                MessageBox.Show("Không tìm thấy phiếu xuất nguyên liệu để xóa");
                return false;
            }
            try
            {
                if (temp.ChiTietPhieuXuats.Count > 0)
                {
                    temp.trangThai = 1;
                    quanLyQuanCoffee.SaveChanges();
                }
                else
                {
                    quanLyQuanCoffee.PhieuXuatNguyenLieux.Remove(temp);
                    quanLyQuanCoffee.SaveChanges();
                }
            }
            catch (DbUpdateException)
            {
                MessageBox.Show("Lỗi! Không thể xóa Phiếu Xuất này");
                return false;
            }

            return true;
        }
        public static bool remove(PhieuXuatNguyenLieu phieuXuatNguyenLieu)
        {
            return remove(phieuXuatNguyenLieu.maPhieuXuat);
        }
    }
}
