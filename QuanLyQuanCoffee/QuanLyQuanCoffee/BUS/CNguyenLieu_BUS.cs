using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCoffee.BUS
{
    class CNguyenLieu_BUS
    {
        private static QuanLyQuanCoffeeEntities quanLyQuanCoffee = new QuanLyQuanCoffeeEntities();

        public static List<NguyenLieu> toList()
        {
            List<NguyenLieu> list = quanLyQuanCoffee.NguyenLieux.ToList();
            return list == null ? new List<NguyenLieu>() : list;
        }

        public static NguyenLieu find(string maNguyenLieu)
        {
            return quanLyQuanCoffee.NguyenLieux.Find(maNguyenLieu);
        }

        public static NguyenLieu find(NguyenLieu nguyenLieu)
        {
            return find(nguyenLieu.maNguyenLieu);
        }

        public static void add(NguyenLieu nguyenLieu)
        {
            quanLyQuanCoffee.NguyenLieux.Add(nguyenLieu);
            quanLyQuanCoffee.SaveChanges();
        }

        public static bool edit(NguyenLieu nguyenLieu)
        {
            NguyenLieu temp = find(nguyenLieu.maNguyenLieu);
            if (temp == null)
            {
                return false;
            }
            temp.copyData(nguyenLieu);
            quanLyQuanCoffee.SaveChanges();
            return true;
        }

        public static bool remove(NguyenLieu nguyenLieu)
        {
            NguyenLieu temp = find(nguyenLieu);
            if (temp == null)
            {
                return false;
            }
            quanLyQuanCoffee.NguyenLieux.Remove(temp);
            quanLyQuanCoffee.SaveChanges();
            return true;
        }
    }
}
