using QuanLyQuanCoffee.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLyQuanCoffee.BUS
{
    class CChiTietPhieuXuat_BUS
    {
        private static QuanLyQuanCoffeeEntities1 quanLyQuanCoffee = new QuanLyQuanCoffeeEntities1();
        public static List<ChiTietPhieuXuat> toList()
        {
            List<ChiTietPhieuXuat> list = quanLyQuanCoffee.ChiTietPhieuXuats.ToList();
            return list == null ? new List<ChiTietPhieuXuat>() : list;
        }

        //Ham dùng cho sau khi xuất nguyên liệu thì sẽ tìm nguyên liêu theo mã chi tiết nguyên liệu và cập nhập số lượng lại bằng 0
        //public static bool CapNhapSoLuong_PhieuNhap(ChiTietPhieuXuat chiTietPhieuXuatNguyenLieu)
        //{
        //    ChiTietNguyenLieu temp = CChiTietNguyenLieu_BUS.findChiTietNguyenLieu(chiTietPhieuXuatNguyenLieu.maChitietNguyenLieu);
        //    if (temp == null)
        //    {
        //        MessageBox.Show("Không tìm thấy nguyên liệu để xuất");
        //        return false;
        //    }
        //    temp.soLuong = 0; 
        //    quanLyQuanCoffee.SaveChanges();
        //    return true;
        //}
        public static bool CapNhapSoLuong_CTNguyenLieu(List<ChiTietPhieuXuat> list)
        {
            if(list.Count>0)
            {
                foreach(var temp in list)
                {
                    ChiTietNguyenLieu chiTietNL = CChiTietNguyenLieu_BUS.findChiTietNguyenLieu(temp.maChitietNguyenLieu);
                    chiTietNL.soLuong = 0;
                    try
                    {
                        quanLyQuanCoffee.SaveChanges();
                    }
                    catch (DbUpdateException)
                    {

                        MessageBox.Show("Lỗi không Lưu được dữ liệu");
                    }
                    catch (DbEntityValidationException)
                    {

                        MessageBox.Show("Lỗi không Lưu được dữ liệu");
                    }
                }
                

            }
            

            return true;
        }
    }
}
