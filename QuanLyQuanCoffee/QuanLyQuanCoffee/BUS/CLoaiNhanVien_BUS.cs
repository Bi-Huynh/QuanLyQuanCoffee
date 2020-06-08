﻿using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCoffee.BUS
{
    class CLoaiNhanVien_BUS
    {
        private static QuanLyQuanCoffeeEntities quanLyQuanCoffee = new QuanLyQuanCoffeeEntities();

        public static LoaiNhanVien find(string maLoaiNhanVien)
        {
            return quanLyQuanCoffee.LoaiNhanViens.Find(maLoaiNhanVien);
        }

        public static LoaiNhanVien find(LoaiNhanVien loaiNhanVien)
        {
            return find(loaiNhanVien.maLoaiNhanvien);
        }

        public static string findMaLoaiByTenLoai(string tenLoai)
        {
            // so sánh cái tên của loại nhân viên và lấy ra cái mã
            return quanLyQuanCoffee.LoaiNhanViens.Where(x => tenLoai == x.tenLoai).FirstOrDefault().maLoaiNhanvien;
        }

        public static List<LoaiNhanVien> toList()
        {
            return quanLyQuanCoffee.LoaiNhanViens.ToList();
        }

        public static List<string> toListTenLoai()
        {
            return quanLyQuanCoffee.LoaiNhanViens.Select(x => x.tenLoai).ToList();
        }

        public static bool kiemTraThongTin(LoaiNhanVien loaiNhanVien)
        {
            return true;
        }

        public static bool add(LoaiNhanVien loaiNhanVien)
        {
            if (kiemTraThongTin(loaiNhanVien))
            {
                quanLyQuanCoffee.LoaiNhanViens.Add(loaiNhanVien);
                quanLyQuanCoffee.SaveChanges();
                return true;
            }
            return false;
        }
        
        public static bool edit(LoaiNhanVien loaiNhanVien)
        {
            LoaiNhanVien temp = find(loaiNhanVien.maLoaiNhanvien);
            if (temp == null || !kiemTraThongTin(loaiNhanVien))
            {
                return false;
            }
            temp.copyData(loaiNhanVien);
            quanLyQuanCoffee.SaveChanges();
            return true;
        }

        public static bool remove(LoaiNhanVien loaiNhanVien)
        {
            LoaiNhanVien temp = find(loaiNhanVien.maLoaiNhanvien);
            if (temp == null)
            {
                return false;
            }
            quanLyQuanCoffee.LoaiNhanViens.Remove(temp);
            quanLyQuanCoffee.SaveChanges();
            return true;
        }
    }
}
