﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLyQuanCoffee.BUS
{
    class CLoaiNguyenLieu_BUS
    {
        private static QuanLyQuanCoffeeEntities quanLyQuanCoffee = new QuanLyQuanCoffeeEntities();

        public static List<LoaiNguyenLieu> toList()
        {
            List<LoaiNguyenLieu> list = quanLyQuanCoffee.LoaiNguyenLieux.ToList();
            return list == null ? new List<LoaiNguyenLieu>() : list;
        }

        public static List<string> toListTenLoai()
        {
            List<string> list = quanLyQuanCoffee.LoaiNguyenLieux.Select(x => x.tenLoaiNguyenLieu).ToList();
            return list == null ? new List<string>() : list;
        }

        public static LoaiNguyenLieu find(string maNguyenLieu)
        {
            return quanLyQuanCoffee.LoaiNguyenLieux.Find(maNguyenLieu);
        }

        public static LoaiNguyenLieu find(LoaiNguyenLieu loaiNguyenLieu)
        {
            return find(loaiNguyenLieu.maLoaiNguyenLieu);
        }

        public static string findMaLoaibyTenLoai(string tenLoai)
        {
            // so sánh cái tên của loại nguyên liệu và lấy ra cái mã
            return quanLyQuanCoffee.LoaiNguyenLieux.Where(x => x.tenLoaiNguyenLieu == tenLoai).FirstOrDefault().maLoaiNguyenLieu;

        }

        public static bool kiemTraThongTin(LoaiNguyenLieu loaiNguyenLieu)
        {
            return true;
        }

        public static bool add(LoaiNguyenLieu loaiNguyenLieu)
        {
            if (kiemTraThongTin(loaiNguyenLieu))
            {
                quanLyQuanCoffee.LoaiNguyenLieux.Add(loaiNguyenLieu);
                quanLyQuanCoffee.SaveChanges();
                return true;
            }
            return false;
        }

        public static bool edit(LoaiNguyenLieu loaiNguyenLieu)
        {
            LoaiNguyenLieu temp = find(loaiNguyenLieu.maLoaiNguyenLieu);
            if (temp == null || !kiemTraThongTin(loaiNguyenLieu))
            {
                return false;
            }
            temp.copyData(loaiNguyenLieu);
            quanLyQuanCoffee.SaveChanges();
            return true;
        }

        public static bool remove(LoaiNguyenLieu loaiNguyenLieu)
        {
            LoaiNguyenLieu temp = find(loaiNguyenLieu.maLoaiNguyenLieu);

            if (temp == null)
            {
                MessageBox.Show("Không tìm thấy loại nhân viên để xóa");
                return false;
            }

            if (temp.NguyenLieux.Count > 0)
            {
                MessageBox.Show("Có nhân viên đang thuộc loại này, Không thể xóa");
                return false;
            }

            quanLyQuanCoffee.LoaiNguyenLieux.Remove(temp);
            quanLyQuanCoffee.SaveChanges();
            return true;
        }
    }
}
