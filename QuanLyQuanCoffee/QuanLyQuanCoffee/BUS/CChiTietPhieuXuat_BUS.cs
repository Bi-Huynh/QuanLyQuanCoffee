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
        private static QuanLyQuanCoffeeEntities1 quanLyQuanCoffee = LoadDatabase.Instance();
        public static List<ChiTietPhieuXuat> toList()
        {
            List<ChiTietPhieuXuat> list = quanLyQuanCoffee.ChiTietPhieuXuats.ToList();
            return list == null ? new List<ChiTietPhieuXuat>() : list;
        }

        public static bool CapNhapSoLuong_CTNguyenLieu(List<ChiTietPhieuXuat> list)
        {
            if (list.Count > 0)
            {
                foreach (var temp in list)
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

        
        
        public static string findNgayXuat(string maChiTietNguyenLieu)
        {
            string ngayXuat = "NULL";
            var list = toList();
            foreach (var x in list)
            {
                try
                {
                    if (x.maChitietNguyenLieu == maChiTietNguyenLieu)
                    {
                        ngayXuat = x.PhieuXuatNguyenLieu.ngayXuat.Value.ToString("dd/MM/yyyy");
                        break;
                    }
                }
                catch (ArgumentNullException)
                {
                    MessageBox.Show("Không tìm thấy Phiếu xuất nguyên liệu trong chi tiết phiếu xuất");
                }
            }
            return ngayXuat;
        }
    }
}
