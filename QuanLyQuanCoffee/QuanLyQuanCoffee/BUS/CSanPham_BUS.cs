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
    class CSanPham_BUS
    {
        private static QuanLyQuanCoffeeEntities1 quanLyQuanCoffee = LoadDatabase.Instance();

        public static SanPham find(string maSanPham)
        {
            SanPham sanPham = quanLyQuanCoffee.SanPhams.Find(maSanPham);
            return sanPham;
        }
        public static LoaiSanPham findTen(string tenLoaiSP)
        {
            LoaiSanPham loaisanPham = quanLyQuanCoffee.LoaiSanPhams.Where(x=> x.tenLoai==tenLoaiSP).FirstOrDefault();
            return loaisanPham;
        }

        public static List<SanPham> toList()
        {
            List<SanPham> list = quanLyQuanCoffee.SanPhams.Where(x => x.trangThai == 0).ToList();
            return list == null ? new List<SanPham>() : list;
        }

        public static List<string> toListTenSanPham()
        {
            List<string> sanPhams = new List<string>();
            foreach (SanPham sanPham in quanLyQuanCoffee.SanPhams.ToList())
            {
                sanPhams.Add(sanPham.tenSanPham.Trim());
            }
            return sanPhams;
        }

        public static List<SanPham> toListTenSanPham(string tenSanPham)
        {
            List<SanPham> sanPhams = new List<SanPham>();
            foreach (SanPham sanPham in quanLyQuanCoffee.SanPhams.ToList())
            {
                if (sanPham.tenSanPham.Contains(tenSanPham))
                {
                    sanPhams.Add(sanPham);
                }
            }
            return sanPhams;
        }

        public static List<SanPham> toListDonGia(int donGia)
        {
            List<SanPham> sanPhams = new List<SanPham>();
            foreach (SanPham sanPham in quanLyQuanCoffee.SanPhams.ToList())
            {
                if (sanPham.donGia.Value >= donGia)
                {
                    sanPhams.Add(sanPham);
                }
            }
            return sanPhams;
        }

        public static List<SanPham> toListTK(string maSP)
        {
            List<SanPham> list = quanLyQuanCoffee.SanPhams.Where(x => x.trangThai == 0&&x.maSanPham.Contains(maSP)==true).ToList();
            return list == null ? new List<SanPham>() : list;
        }
        public static List<SanPham> DsSanPham()
        {
            List<SanPham> list = quanLyQuanCoffee.SanPhams.ToList();
            return list == null ? new List<SanPham>() : list;
        }
        public static List<string> toListByMaSP()
        {
            List<string> list = quanLyQuanCoffee.LoaiSanPhams.Select(x => x.maLoaiSanPham ).ToList();
            return list == null ? new List<string>() : list;
        }
        public static List<string> toListByMaTenLoaiSP()
        {
            List<string> list = quanLyQuanCoffee.LoaiSanPhams.Select(x => x.tenLoai).ToList();
            return list == null ? new List<string>() : list;
        }
        public static List<SanPham> hienthiTheoMa(string maLoai)
        {
            List<SanPham> list = quanLyQuanCoffee.SanPhams.Where(x => x.trangThai == 0 && x.maLoaiSanPham == maLoai).ToList();
            return list == null ? new List<SanPham>() : list;
        }
        public static bool add(SanPham sanPham)
        {
            if (CServices.kiemTraThongTin(sanPham))
            {
                try
                {
                    sanPham.tenSanPham = CServices.formatChuoi(sanPham.tenSanPham);

                    quanLyQuanCoffee.SanPhams.Add(sanPham);
                    quanLyQuanCoffee.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    MessageBox.Show("Lỗi! không thể lưu dữ liệu");
                    return false;
                }
                catch (DbEntityValidationException)
                {
                    MessageBox.Show("Lỗi! kiểu dữ liệu");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Xem lại đơn giá");
            }
            return true;
        }
        public static bool thaydoiLoai(LoaiSanPham loaiSanPham)
        {
            string maLoai = loaiSanPham.maLoaiSanPham;
            int trangthai = int.Parse(loaiSanPham.trangThai.ToString());
            List<SanPham> list = quanLyQuanCoffee.SanPhams.Where(x => x.maLoaiSanPham == maLoai).ToList();
            if (list.Count > 0)
            {
                foreach (var a in list)
                {
                    a.trangThai = trangthai;
                    quanLyQuanCoffee.SaveChanges(); 
                }
                
                return true;
            }
            else
            {
                return false;
            }    
           
        }
        public static bool remove(SanPham sanPham)
        {
            try
            {
                SanPham temp = find(sanPham.maSanPham);
                if (temp != null)
                {
                    if (temp.trangThai == 0)
                    {
                        temp.trangThai = 1;
                        quanLyQuanCoffee.SaveChanges();
                    }
                    else
                    {
                        temp.trangThai = 0;
                        quanLyQuanCoffee.SaveChanges();
                    }    
                }
            }
            catch (DbUpdateException)
            {
                MessageBox.Show("Lỗi! không thể lưu dữ liệu");
                return false;
            }
            catch (DbEntityValidationException)
            {
                MessageBox.Show("Lỗi! kiểu dữ liệu");
                return false;
            }
            return true;
        }
        public static bool KTRong(SanPham sanPham)
        {


            if (sanPham.maSanPham.Length > 10)
            {
                return false;
            }
            if (sanPham.tenSanPham == "" || sanPham.donViTinh == "")
            {
                return false;
            }
            if (sanPham.maLoaiSanPham == null)
            {
                return false;
            }

            return true;
        }

        public static bool edit(SanPham sanPham)
        {
            SanPham temp = find(sanPham.maSanPham);
            if (temp != null)
            {
                try
                {
                    temp.tenSanPham = sanPham.tenSanPham;
                    temp.donViTinh = sanPham.donViTinh;
                    temp.donGia = sanPham.donGia;
                    temp.maLoaiSanPham = sanPham.maLoaiSanPham;
                    temp.trangThai = sanPham.trangThai;
                    temp.ThanhPhans = sanPham.ThanhPhans;
                    quanLyQuanCoffee.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    MessageBox.Show("Lỗi! không thể lưu dữ liệu");
                    return false;
                }
                catch (DbEntityValidationException)
                {
                    MessageBox.Show("Lỗi! kiểu dữ liệu");
                    return false;
                }
            }
            return true;
        }
    }
}
