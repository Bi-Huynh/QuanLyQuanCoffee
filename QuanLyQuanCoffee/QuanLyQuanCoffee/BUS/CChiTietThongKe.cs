using QuanLyQuanCoffee.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLyQuanCoffee.BUS
{
    class CChiTietThongKe
    {
        private static QuanLyQuanCoffeeEntities1 quanLyQuanCoffee = new QuanLyQuanCoffeeEntities1();

        public static List<ChiTietThongKe> toList()
        {
            List<ChiTietThongKe> chiTietThongKes = quanLyQuanCoffee.ChiTietThongKes.ToList();
            return chiTietThongKes == null ? new List<ChiTietThongKe>() : chiTietThongKes;
        }

        public static List<ChiTietThongKe> toList(DateTime ngayBatDau, DateTime ngayKetThuc)
        {
            List<ChiTietThongKe> chiTietThongKes = quanLyQuanCoffee.ChiTietThongKes
                .Where(x => x.ngayLap >= ngayBatDau && x.ngayLap <= ngayKetThuc).ToList();
            return chiTietThongKes == null ? new List<ChiTietThongKe>() : chiTietThongKes;
        }

        public static bool add(ChiTietThongKe chiTietThongKe)
        {
            try
            {
                quanLyQuanCoffee.ChiTietThongKes.Add(chiTietThongKe);
                quanLyQuanCoffee.SaveChanges();
            }
            catch (DbUpdateException)
            {
                MessageBox.Show("Lỗi! Không thể thêm dữ liệu");
                return false;
            }
            catch (DbEntityValidationException)
            {
                MessageBox.Show("Lỗi! Kiểu dữ liệu được truyền vào không hợp lệ");
                return false;
            }
            return true;
        }
    }
}
