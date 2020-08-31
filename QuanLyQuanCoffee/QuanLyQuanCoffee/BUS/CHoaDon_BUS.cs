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
    class CHoaDon_BUS
    {
        private static QuanLyQuanCoffeeEntities1 quanLyQuanCoffee = LoadDatabase.Instance();
        public static HoaDon hoaDonTreo = null;

        public static List<HoaDon> toList()
        {
            List<HoaDon> list = quanLyQuanCoffee.HoaDons.ToList();
            return list == null ? new List<HoaDon>() : list;
        }

        public static List<HoaDon> toListMaNhanVien(string maNhanVien)
        {
            List<HoaDon> hoaDons = new List<HoaDon>();
            foreach (HoaDon hoaDon in quanLyQuanCoffee.HoaDons.ToList())
            {
                if (hoaDon.maNhanVien.Contains(maNhanVien) && hoaDon.trangThai == 0)
                {
                    hoaDons.Add(hoaDon);
                }
            }
            return hoaDons;
        }

        public static List<HoaDon> toList(DateTime ngayBatDau, DateTime ngayKetThuc)
        {
            List<HoaDon> hoaDons = new List<HoaDon>();
            foreach (var hoaDon in quanLyQuanCoffee.HoaDons.ToList())
            {
                if (hoaDon.ngayLap.Date >= ngayBatDau.Date && hoaDon.ngayLap.Date <= ngayKetThuc.Date)
                {
                    hoaDons.Add(hoaDon);
                }
            }
            return hoaDons;
        }

        public static List<HoaDon> toListMaHoaDon(string maHoaDon)
        {
            List<HoaDon> hoaDons = new List<HoaDon>();
            foreach (HoaDon hoaDon in quanLyQuanCoffee.HoaDons.ToList())
            {
                if (hoaDon.maHoaDon.Contains(maHoaDon) && hoaDon.trangThai == 0)
                {
                    hoaDons.Add(hoaDon);
                }
            }
            return hoaDons;
        }

        public static List<HoaDon> toListTongThanhTien(double tongThanhTien)
        {
            List<HoaDon> hoaDons = new List<HoaDon>();
            foreach (HoaDon hoaDon in quanLyQuanCoffee.HoaDons.ToList())
            {
                if (hoaDon.tongThanhTien >= tongThanhTien && hoaDon.trangThai == 0)
                {
                    hoaDons.Add(hoaDon);
                }
            }
            return hoaDons;
        }

        public static List<HoaDon> toListNgayLap(DateTime ngayLap)
        {
            List<HoaDon> hoaDons = new List<HoaDon>();
            foreach (HoaDon hoaDon in quanLyQuanCoffee.HoaDons.ToList())
            {
                if (hoaDon.ngayLap.Date == ngayLap.Date && hoaDon.trangThai == 0)
                {
                    hoaDons.Add(hoaDon);
                }
            }
            return hoaDons;
        }

        public static List<HoaDon> toList(CCa_DTO caLam)
        {
            List<HoaDon> hoaDons = new List<HoaDon>();
            foreach (var hoaDon in quanLyQuanCoffee.HoaDons.ToList())
            {
                if (caLam.GioBatDau.Date == hoaDon.ngayLap.Date
                        && hoaDon.ngayLap.TimeOfDay >= caLam.GioBatDau.TimeOfDay)
                {
                    hoaDons.Add(hoaDon);
                }
            }
            return hoaDons;
        }


        public static HoaDon find(string maHoaDon)
        {
            HoaDon hoaDon = quanLyQuanCoffee.HoaDons.Find(maHoaDon);
            return hoaDon;
        }
        public static List<HoaDon> DsHoaDon(DateTime gioBatDau, DateTime gioKetThuc)
        {
            List<HoaDon> list = new List<HoaDon>();
            foreach (var item in toList())
            {
                // không thể truy vấn bằng cách sử dụng where vì nó không hỗ trợ timeofday
                if (item.ngayLap.TimeOfDay >= gioBatDau.TimeOfDay &&
                    item.ngayLap.TimeOfDay <= gioKetThuc.TimeOfDay)
                {
                    list.Add(item);
                }
            }
            return list;
        }

        public static int demSoLuongHoaDon(DateTime gioBatDau, DateTime gioKetThuc)
        {
            int soLuongHoaDon = 0;
            try
            {
                soLuongHoaDon = DsHoaDon(gioBatDau, gioKetThuc).Count();
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Đếm số lượng hóa đơn lỗi, CHoaDon, ArgNull");
            }
            catch (OverflowException)
            {
                MessageBox.Show("Đếm số lượng hóa đơn lỗi, CHoaDon, Overflow");
            }
            return soLuongHoaDon;
        }

        public static int demSoLuongHoaDon(string maNhanVien, int thang)
        {
            int soLuongHoaDon = 0;
            foreach (var hoaDon in toList())
            {
                if (hoaDon.maNhanVien == maNhanVien && hoaDon.ngayLap.Month == thang)
                {
                    soLuongHoaDon++;
                }
            }
            return soLuongHoaDon;
        }

        public static int demSoLuongLyBanDuoc(DateTime gioBatDau, DateTime gioKetThuc)
        {
            int soLuongHoaDon = 0;
            try
            {
                List<HoaDon> hoaDons = DsHoaDon(gioBatDau, gioKetThuc);
                hoaDons.ForEach(x => soLuongHoaDon += x.ChiTietHoaDons.Count());
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Đếm số lượng chi tiết hóa đơn lỗi, CHoaDon, ArgNull");
            }
            catch (OverflowException)
            {
                MessageBox.Show("Đếm số lượng chi tiết hóa đơn lỗi, CHoaDon, Overflow");
            }
            return soLuongHoaDon;
        }

        public static int demSoLuongLyBanDuoc(string maNhanVien, int thang)
        {
            int soLuongHoaDon = 0;
            try
            {
                foreach (var hoaDon in toList())
                {
                    if (hoaDon.maNhanVien == maNhanVien && hoaDon.ngayLap.Month == thang)
                    {
                        foreach (var chiTietHoaDon in hoaDon.ChiTietHoaDons)
                        {
                            soLuongHoaDon += chiTietHoaDon.soLuong.Value;
                        }
                    }
                }
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Đếm số lượng chi tiết hóa đơn lỗi, CHoaDon, ArgNull");
            }
            catch (OverflowException)
            {
                MessageBox.Show("Đếm số lượng chi tiết hóa đơn lỗi, CHoaDon, Overflow");
            }
            return soLuongHoaDon;
        }

        public static int demSoLuongSanPham(string maSanPham, int thang)
        {
            int soLuongSanPham = 0;
            try
            {
                List<HoaDon> hoaDons = toList().Where(x => x.ngayLap.Month == thang && x.ngayLap.Year == DateTime.Now.Year && x.trangThai == 0).ToList();
                foreach (var hoaDon in hoaDons)
                {
                    foreach (var chiTietHoaDon in hoaDon.ChiTietHoaDons)
                    {
                        if (chiTietHoaDon.maSanPham == maSanPham)
                        {
                            soLuongSanPham += chiTietHoaDon.soLuong.Value;
                        }
                    }
                }
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Đếm số lượng chi tiết hóa đơn lỗi, CHoaDon, ArgNull");
            }
            catch (OverflowException)
            {
                MessageBox.Show("Đếm số lượng chi tiết hóa đơn lỗi, CHoaDon, Overflow");
            }
            return soLuongSanPham;
        }

        public static Boolean add(HoaDon hoaDon)
        {
            if (CServices.kiemTraThongTin(hoaDon))
            {
                try
                {
                    quanLyQuanCoffee.HoaDons.Add(hoaDon);
                    quanLyQuanCoffee.SaveChanges();
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
            }
            else
            {
                MessageBox.Show("Xem lại đơn giá");
            }
            return true;
        }

        public static double tongTienBan(DateTime gioBatDau, DateTime gioKetThuc)
        {
            double result = 0;
            DsHoaDon(gioBatDau, gioKetThuc).ForEach(x => result += x.tongThanhTien);
            return result;
        }

        public static double tongTienBan(List<HoaDon> hoaDons)
        {
            double result = 0;
            hoaDons.ForEach(x => result += x.tongThanhTien);
            return result;
        }

        public static double tongTienBan(string maNhanVien, int thang)
        {
            double result = 0;
            try
            {
                foreach (var hoaDon in toList())
                {
                    if (hoaDon.maNhanVien == maNhanVien && hoaDon.ngayLap.Month == thang)
                    {
                        result += hoaDon.tongThanhTien;
                    }
                }
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Đếm số lượng chi tiết hóa đơn lỗi, CHoaDon, ArgNull");
            }
            catch (OverflowException)
            {
                MessageBox.Show("Đếm số lượng chi tiết hóa đơn lỗi, CHoaDon, Overflow");
            }
            return result;
        }

        public static double tongTienBanSanPham(string maSanPham, int thang)
        {
            double result = 0;
            try
            {
                List<HoaDon> hoaDons = toList().Where(x => x.ngayLap.Month == thang && x.ngayLap.Year == DateTime.Now.Year).ToList();
                foreach (var hoaDon in hoaDons)
                {
                    foreach (var chiTietHoaDon in hoaDon.ChiTietHoaDons)
                    {
                        if (chiTietHoaDon.maSanPham == maSanPham)
                        {
                            result += chiTietHoaDon.thanhTien.Value;
                        }
                    }
                }
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Đếm tổng tiền bán của sản phẩm lỗi, CHoaDon, ArgNull");
            }
            catch (OverflowException)
            {
                MessageBox.Show("Đếm tổng tiền bán của sản phẩm lỗi, CHoaDon, Overflow");
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Giá trị vượt qua giới hạn, CHoaDon, Invali");
            }
            return result;
        }

        public static double tongTienBanSanPham_(string maSanPham, int soLuong)
        {
            double result = 0;
            SanPham sanPham = CSanPham_BUS.find(maSanPham);
            if (sanPham != null)
            {
                result = sanPham.donGia.Value * soLuong;
            }
            else
            {
                result = tongTienBanSanPham(maSanPham, DateTime.Now.Month);
            }
            return result;
        }
    }
}
