using Prism.Regions;
using QuanLyQuanCoffee.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace QuanLyQuanCoffee.BUS
{
    class CChiTietNguyenLieu_BUS
    {
        public static QuanLyQuanCoffeeEntities1 quanLyQuanCoffee = new QuanLyQuanCoffeeEntities1();

        public static List<ChiTietNguyenLieu> toList()
        {
            List<ChiTietNguyenLieu> list = quanLyQuanCoffee.ChiTietNguyenLieux.ToList();
            return list == null ? new List<ChiTietNguyenLieu>() : list;
        }

        public static List<ChiTietNguyenLieu> find(string maNguyenLieu)
        {
            List<ChiTietNguyenLieu> list = new List<ChiTietNguyenLieu>();
            toList().ForEach(item =>
            {
                if (item.maNguyenLieu == maNguyenLieu)
                {
                    list.Add(item);
                }
            });
            return list;
        }

        public static int tongSoLuong(string maNguyenLieu)
        {
            int tong = 0;
            List<ChiTietNguyenLieu> list = find(maNguyenLieu);
            if (list.Count() > 0)
            {
                list.ForEach(item =>
                {
                    tong += item.soLuong.Value;
                });
            }
            return tong;
        }

        public static int soNgayConLai(DateTime ngayHetHan)
        {
            int soNgay = 0;
            DateTime ngayHienTai = DateTime.Now;
            if (ngayHetHan > ngayHienTai)
            {
                TimeSpan timeSpan = ngayHetHan - ngayHienTai;
                soNgay = timeSpan.Days;
            }
            return soNgay;
        }

        public static bool add(ChiTietNguyenLieu chiTiet)
        {
            if (CServices.kiemTraThongTin(chiTiet))
            {
                try
                {
                    quanLyQuanCoffee.ChiTietNguyenLieux.Add(chiTiet);
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
    }
}
