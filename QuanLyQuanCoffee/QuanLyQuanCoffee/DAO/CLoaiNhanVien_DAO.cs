using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCoffee.DAO
{
    class CLoaiNhanVien_DAO
    {
        private static QuanLyQuanCoffeeEntities quanLyQuanCoffee = new QuanLyQuanCoffeeEntities();
        public static List<LoaiNhanVien> LoaiNhanViens ()
        {
            return quanLyQuanCoffee.LoaiNhanViens.ToList();
        }

        public static LoaiNhanVien find(string maNhanVien)
        {
            return quanLyQuanCoffee.LoaiNhanViens.Find(maNhanVien);
        }
    }
}
