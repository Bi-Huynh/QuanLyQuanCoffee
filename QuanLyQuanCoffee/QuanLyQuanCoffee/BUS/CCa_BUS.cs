using QuanLyQuanCoffee.DTO;
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
    public class CCa_BUS
    {
        private static QuanLyQuanCoffeeEntities1 quanLyQuanCoffee = new QuanLyQuanCoffeeEntities1();
        private static bool daKetCa = false;
        private static CCa_DTO caLamViec;

        public static bool isDaKetCa { get => daKetCa; set => daKetCa = value; }
        public static CCa_DTO CaLamViec { get => caLamViec; set => caLamViec = value; }

        public static List<KetCa> toList()
        {
            List<KetCa> list = quanLyQuanCoffee.KetCas.ToList();
            return list == null ? new List<KetCa>() : list;
        }

        public static List<KetCa> toList(DateTime ngayBatDau, DateTime ngayKetThuc)
        {
            List<KetCa> list = quanLyQuanCoffee.KetCas
                .Where(x => x.ngayLap >= ngayBatDau && x.ngayLap <= ngayKetThuc)
                .ToList();
            return list == null ? new List<KetCa>() : list;
        }

        public static bool add(KetCa ketCa)
        {
            try
            {
                if (CServices.kiemTraThongTin(ketCa))
                {
                    quanLyQuanCoffee.KetCas.Add(ketCa);
                    quanLyQuanCoffee.SaveChanges();
                    isDaKetCa = true;
                }
            }
            catch (DbUpdateException)
            {
                MessageBox.Show("Lỗi! không thể lưu dữ liệu");
                return false;
            }
            catch (DbEntityValidationException)
            {
                MessageBox.Show("Lỗi! không thể lưu dữ liệu");
                return false;
            }

            return true;
        }
    }
}
