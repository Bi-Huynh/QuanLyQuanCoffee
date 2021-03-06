﻿using Prism.Regions;
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
        public static QuanLyQuanCoffeeEntities1 quanLyQuanCoffee = LoadDatabase.Instance();

        public static List<ChiTietNguyenLieu> toList()
        {
            List<ChiTietNguyenLieu> list = quanLyQuanCoffee.ChiTietNguyenLieux.ToList();
            return list == null ? new List<ChiTietNguyenLieu>() : list;
        }

        public static string taoMa(string ma, List<ChiTietNguyenLieu> list)
        {
            string result = "";
            string temp = "";

            if (list.Count() == 0)
            {
                temp = "0000000001";
            }
            else
            {
                try
                {
                    string maCTNguyenLieu = list[list.Count() - 1].maChiTietNguyenLieu;
                    string maLast = maCTNguyenLieu.Substring(0, 10);
                    double thuTu = int.Parse(maLast.ToString());
                    ++thuTu;
                    if (thuTu == 9999999999)
                    {
                        MessageBox.Show("Mã đã tới giới hạn, không thể tăng nữa");
                        return "";
                    }
                    temp = String.Format("{0:0000000000}", thuTu);
                }
                catch (ArgumentNullException)
                {
                    MessageBox.Show("Lỗi tạo mã chi tiết nguyên liệu, để dữ liệu rỗng");
                }
                catch (FormatException)
                {
                    MessageBox.Show("Lỗi tạo mã chi tiết nguyên liệu, định dạng dữ liệu không hợp lệ");
                }
                catch (OverflowException)
                {
                    MessageBox.Show("Lỗi tạo mã chi tiết nguyên liệu, giá trị vượt quá giới hạn cho phép");
                }
            }
            result = temp + ma;
            return result;
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
       
        public static ChiTietNguyenLieu findChiTietNguyenLieu(string maChiTietNguyenLieu)
        {
            ChiTietNguyenLieu chiTiet = new ChiTietNguyenLieu();
            chiTiet = quanLyQuanCoffee.ChiTietNguyenLieux.Where(x => x.maChiTietNguyenLieu == maChiTietNguyenLieu).FirstOrDefault();
            return chiTiet;
        }

        public static ChiTietNguyenLieu findCT(string maChiTietNguyenLieu)
        {
            ChiTietNguyenLieu chiTietNguyenLieu = quanLyQuanCoffee.ChiTietNguyenLieux.Find(maChiTietNguyenLieu);
            return chiTietNguyenLieu == null ? new ChiTietNguyenLieu() : chiTietNguyenLieu;
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
                    return true;
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
            return false;
        }

        public static bool remove(string maChiTietNguyenLieu)
        {
            ChiTietNguyenLieu chiTietNguyenLieu = findCT(maChiTietNguyenLieu);
            if (chiTietNguyenLieu.ChiTietPhieuNhaps.Count() > 0 || 
                chiTietNguyenLieu.ChiTietPhieuXuats.Count() > 0)
            {
                MessageBox.Show("Không thể xóa chi tiết nguyên liệu này");
                return false;
            }
            try
            {
                quanLyQuanCoffee.ChiTietNguyenLieux.Remove(chiTietNguyenLieu);
                quanLyQuanCoffee.SaveChanges();
            }
            catch (DbUpdateException)
            {
                MessageBox.Show("Lỗi! Không thể xóa chi tiết nguyên liệu");
                return false;
            }
            catch (DbEntityValidationException)
            {
                MessageBox.Show("Lỗi! Kiểu dữ liệu không hợp lệ");
                return false;
            }
            return true;
        }
    }
}
