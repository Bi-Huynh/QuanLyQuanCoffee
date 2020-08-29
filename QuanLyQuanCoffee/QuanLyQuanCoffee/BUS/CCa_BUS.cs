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
        private static QuanLyQuanCoffeeEntities1 quanLyQuanCoffee = LoadDatabase.Instance();
        private static bool daKetCa = false;
        private static CCa_DTO caLamViec = null;

        public static bool isDaKetCa { get => daKetCa; set => daKetCa = value; }
        public static CCa_DTO CaLamViec { get => caLamViec; set => caLamViec = value; }

        public static List<KetCa> toList()
        {
            List<KetCa> list = quanLyQuanCoffee.KetCas.ToList();
            return list == null ? new List<KetCa>() : list;
        }

        public static KetCa find(string maKetCa)
        {
            KetCa ketCa = quanLyQuanCoffee.KetCas.Find(maKetCa);
            return ketCa == null ? new KetCa() : ketCa;
        }

        public static List<KetCa> toListMaNV(string maNhanVien)
        {
            List<KetCa> ketCas = new List<KetCa>();

            try
            {
                foreach (KetCa ketCa in quanLyQuanCoffee.KetCas.ToList())
                {
                    if (ketCa.maNhanVien.Contains(maNhanVien))
                    {
                        ketCas.Add(ketCa);
                    }
                }
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show("Lỗi: " + ex);
            }

            return ketCas;
        }

        public static List<KetCa> toListTenNV(string tenNhanVien)
        {
            List<KetCa> ketCas = new List<KetCa>();

            try
            {
                foreach (KetCa ketCa in quanLyQuanCoffee.KetCas.ToList())
                {
                    string ten = ketCa.NhanVien.hoNhanVien + " " + ketCa.NhanVien.tenNhanVien;
                    if (ten.ToLower().Contains(tenNhanVien.ToLower()))
                    {
                        ketCas.Add(ketCa);
                    }
                }
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show("Lỗi: " + ex);
            }

            return ketCas;
        }

        public static List<KetCa> toListTongTienBan(string tongTienBan)
        {
            List<KetCa> ketCas = new List<KetCa>();

            try
            {
                foreach (KetCa ketCa in quanLyQuanCoffee.KetCas.ToList())
                {
                    if (ketCa.tongTienBan.ToString().Contains(tongTienBan))
                    {
                        ketCas.Add(ketCa);
                    }
                }
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show("Lỗi: " + ex);
            }

            return ketCas;
        }

        public static List<KetCa> toListTongDoanhThu(string tongDoanhThu)
        {
            List<KetCa> ketCas = new List<KetCa>();

            try
            {
                foreach (KetCa ketCa in quanLyQuanCoffee.KetCas.ToList())
                {
                    if (ketCa.tongDoanhThu.ToString().Contains(tongDoanhThu))
                    {
                        ketCas.Add(ketCa);
                    }
                }
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show("Lỗi: " + ex);
            }

            return ketCas;
        }

        public static List<KetCa> toListNgayLap(DateTime ngayLap)
        {
            List<KetCa> ketCas = new List<KetCa>();

            try
            {
                foreach (KetCa ketCa in quanLyQuanCoffee.KetCas.ToList())
                {
                    if (ketCa.ngayLap.Value.Date == ngayLap.Date)
                    {
                        ketCas.Add(ketCa);
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show("Lỗi: " + ex);
            }

            return ketCas;
        }

        public static List<KetCa> toListNgayLap(DateTime ngayBatDau, DateTime ngayKetThuc)
        {
            List<KetCa> ketCas = new List<KetCa>();

            try
            {
                foreach (KetCa ketCa in quanLyQuanCoffee.KetCas.ToList())
                {
                    if (ketCa.ngayLap.Value.Date >= ngayBatDau.Date && 
                        ketCa.ngayLap.Value.Date <= ngayKetThuc.Date)
                    {
                        ketCas.Add(ketCa);
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show("Lỗi: " + ex);
            }

            return ketCas;
        }

        public static List<KetCa> toListHoaDonKetCa(string maKetCa)
        {
            List<KetCa> ketCas = new List<KetCa>();


            return ketCas;
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
                    ketCa.gioKetThuc = DateTime.Now;
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
