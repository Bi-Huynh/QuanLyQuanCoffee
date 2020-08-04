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
    class CThongKe
    {
        private static QuanLyQuanCoffeeEntities1 quanLyQuanCoffee = new QuanLyQuanCoffeeEntities1();

        public static List<ThongKe> toList()
        {
            List<ThongKe> thongKes = quanLyQuanCoffee.ThongKes.ToList();
            return thongKes == null ? new List<ThongKe>() : thongKes;


        }

        public static List<ThongKe> toList(string maThongKe)
        {
            List<ThongKe> thongKes = quanLyQuanCoffee.ThongKes
                .Where(x => x.maThongKe.Contains(maThongKe)).ToList();
            return thongKes == null ? new List<ThongKe>() : thongKes;


        }

        public static List<ThongKe> toList(DateTime ngayBatDau, DateTime ngayKetThuc)
        {
            List<ThongKe> thongKes = quanLyQuanCoffee.ThongKes

                .Where(x => x.ngayLap.Value.Date >= ngayBatDau.Date && x.ngayLap.Value.Date <= ngayKetThuc.Date).ToList();
            return thongKes == null ? new List<ThongKe>() : thongKes;

            //    .Where(x => x.ngayLap >= ngayBatDau && x.ngayLap <= ngayKetThuc).ToList();
            //return thongKes == null ? new List<ThongKe>() : thongKes;


        }

        public static ThongKe find(string maThongKe)
        {
            ThongKe thongKe = quanLyQuanCoffee.ThongKes.Find(maThongKe);
            return thongKe;
        }

        public static bool add(ThongKe thongKe)
        {
            if (CServices.kiemTraThongTin(thongKe))
            {
                try
                {
                    quanLyQuanCoffee.ThongKes.Add(thongKe);
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
            }
            return true;
        }

        public static List<ThongKe> toList(int thang)
        {
            List<ThongKe> thongKes = new List<ThongKe>();
            foreach (var thongKe in toList())
            {
                if (thongKe.ngayLap.Value.Month == thang)
                {
                    thongKes.Add(thongKe);
                }
            }
            return thongKes;
        }
    }
}
