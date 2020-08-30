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
    class CChiTietPhieuNhapNguyenLieu_BUS
    {
        private static QuanLyQuanCoffeeEntities1 quanLyQuanCoffee = LoadDatabase.Instance();

        public static List<ChiTietPhieuNhap> toList()
        {
            //List<ChiTietPhieuNhap> list = quanLyQuanCoffee.ChiTietPhieuNhaps
            //    .Where(x => x.soLuong > 0).ToList();
            //return list == null ? new List<ChiTietPhieuNhap>() : list;

            List<ChiTietPhieuNhap> chiTietPhieuNhaps = new List<ChiTietPhieuNhap>();
            foreach (ChiTietPhieuNhap item in quanLyQuanCoffee.ChiTietPhieuNhaps.ToList())
            {
                if (item.ChiTietNguyenLieu.soLuong > 0)
                {
                    chiTietPhieuNhaps.Add(item);
                }
            }
            return chiTietPhieuNhaps;
        }

        public static List<ChiTietPhieuNhap> toList(string maPhieuNhap)
        {
            List<ChiTietPhieuNhap> list = quanLyQuanCoffee.ChiTietPhieuNhaps
                .Where(x => x.maPhieuNhap == maPhieuNhap).ToList();
            return list == null ? new List<ChiTietPhieuNhap>() : list;
            //List<ChiTietPhieuNhap> list = quanLyQuanCoffee.ChiTietPhieuNhaps
            //    .Include("ChiTietNguyenLieux").Include("NguyenLieux")
            //    .Select(x => new ChiTietPhieuNhap()
            //    {
            //        maChiTietPhieuNhap = x.maChiTietPhieuNhap,
            //        maChitietNguyenLieu = x.maChitietNguyenLieu,
            //        soLuong = x.soLuong,
            //        donGia = x.donGia,
            //        thanhTien = x.thanhTien,
            //        maPhieuNhap = x.maPhieuNhap,
            //        ChiTietNguyenLieu = x.ChiTietNguyenLieu == null ? null : new ChiTietNguyenLieu()
            //        {
            //            maChiTietNguyenLieu = x.ChiTietNguyenLieu.maChiTietNguyenLieu,
            //            maNguyenLieu = x.ChiTietNguyenLieu.maNguyenLieu,
            //            soLuong = x.ChiTietNguyenLieu.soLuong,
            //            ngayHetHan = x.ChiTietNguyenLieu.ngayHetHan,
            //            donViTinh = x.ChiTietNguyenLieu.donViTinh,
            //            NguyenLieu = x.ChiTietNguyenLieu.NguyenLieu == null ? null : new NguyenLieu()
            //            {
            //                maNguyenLieu = x.ChiTietNguyenLieu.NguyenLieu.maNguyenLieu,
            //                tenNguyenLieu = x.ChiTietNguyenLieu.NguyenLieu.tenNguyenLieu,
            //                maLoaiNguyenLieu = x.ChiTietNguyenLieu.NguyenLieu.maLoaiNguyenLieu,
            //                trangThai = x.ChiTietNguyenLieu.NguyenLieu.trangThai
            //            }
            //        }
            //    }).ToList<ChiTietPhieuNhap>();
            //return list == null ? new List<ChiTietPhieuNhap>() : list;
        }

        public static List<ChiTietPhieuNhap> toListAll()
        {
            List<ChiTietPhieuNhap> list = quanLyQuanCoffee.ChiTietPhieuNhaps.ToList();
            return list == null ? new List<ChiTietPhieuNhap>() : list;
        }

        public static List<ChiTietPhieuNhap> toListPhieuNhap(string maNguyenLieu)
        {
            List<ChiTietPhieuNhap> list = quanLyQuanCoffee.ChiTietPhieuNhaps
                .Where(x => x.ChiTietNguyenLieu.NguyenLieu.maNguyenLieu == maNguyenLieu).ToList();
            return list == null ? new List<ChiTietPhieuNhap>() : list;
        }

        public static int tongSoLuong(string maNguyenLieu)
        {
            int tong = 0;
            //List<ChiTietPhieuNhap> list = toListPhieuNhap(maNguyenLieu);
            //if (list.Count() > 0)
            //{
            //    list.ForEach(item =>
            //    {
            //        tong += item.soLuong.Value;
            //    });
            //}

            if (maNguyenLieu != null && maNguyenLieu != "")
            {
                NguyenLieu nguyenLieu = quanLyQuanCoffee.NguyenLieux.Find(maNguyenLieu);
                foreach (ChiTietNguyenLieu chiTietNguyenLieu in nguyenLieu.ChiTietNguyenLieux.ToList())
                {
                    tong += chiTietNguyenLieu.soLuong.Value;
                }
            }

            return tong;
        }

        public static List<ChiTietPhieuNhap> toListTenNguyenLieu(string tenNguyenLieu)
        {
            //List<ChiTietPhieuNhap> list = quanLyQuanCoffee.ChiTietPhieuNhaps
            //    .Where(x => x.ChiTietNguyenLieu.NguyenLieu.tenNguyenLieu == tenNguyenLieu).ToList();
            //return list == null ? new List<ChiTietPhieuNhap>() : list;

            List<ChiTietPhieuNhap> chiTietPhieuNhaps = new List<ChiTietPhieuNhap>();
            foreach (ChiTietPhieuNhap item in quanLyQuanCoffee.ChiTietPhieuNhaps.ToList())
            {
                if (item.ChiTietNguyenLieu.soLuong > 0 && 
                    item.ChiTietNguyenLieu.NguyenLieu.tenNguyenLieu == tenNguyenLieu)
                {
                    chiTietPhieuNhaps.Add(item);
                }
            }
            return chiTietPhieuNhaps;
        }

        public static ChiTietPhieuNhap find(string maChiTietPhieuNhap)
        {
            ChiTietPhieuNhap chiTietPhieuNhapNguyenLieu = quanLyQuanCoffee.ChiTietPhieuNhaps.Find(maChiTietPhieuNhap);
            return chiTietPhieuNhapNguyenLieu == null ? new ChiTietPhieuNhap() : chiTietPhieuNhapNguyenLieu;
        }

        public static ChiTietPhieuNhap findTheoMachiTietNL(string maChiTietNguyenLieu)
        {
            ChiTietPhieuNhap chiTietPhieuNhapNguyenLieu = quanLyQuanCoffee.ChiTietPhieuNhaps.Where(x => x.maChitietNguyenLieu == maChiTietNguyenLieu).FirstOrDefault();
            return chiTietPhieuNhapNguyenLieu == null ? new ChiTietPhieuNhap() : chiTietPhieuNhapNguyenLieu;
        }

        public static List<ChiTietNguyenLieu> findList(string maNguyenLieu)
        {
            List<ChiTietNguyenLieu> list = quanLyQuanCoffee.ChiTietNguyenLieux
                .Where(x => x.maNguyenLieu == maNguyenLieu && x.soLuong > 0).ToList();
            return list == null ? new List<ChiTietNguyenLieu>() : list;
        }

        public static double tongThanhTien(string maNguyenLieu)
        {
            double tongThanhTien = 0;
            List<ChiTietNguyenLieu> list = findList(maNguyenLieu);
            foreach (ChiTietNguyenLieu item in list)
            {
                tongThanhTien += item.ChiTietPhieuNhaps.FirstOrDefault().donGia.Value * item.soLuong.Value;

            }

            return tongThanhTien;
        }


        public static bool add(ChiTietPhieuNhap chiTietPhieuNhapNguyenLieu)
        {
            if (CServices.kiemTraThongTin(chiTietPhieuNhapNguyenLieu))
            {
                try
                {
                    quanLyQuanCoffee.ChiTietPhieuNhaps.Add(chiTietPhieuNhapNguyenLieu);
                    quanLyQuanCoffee.SaveChanges();
                    return true;
                }
                catch (DbUpdateException)
                {
                    MessageBox.Show("Lỗi kiểu dữ liệu, không thể lưu vào trong CSDL");
                }
            }
            return false;
        }

        public static bool edit(ChiTietPhieuNhap chiTietPhieuNhapNguyenLieu)
        {
            ChiTietPhieuNhap temp = find(chiTietPhieuNhapNguyenLieu.maChiTietPhieuNhap);
            if (temp == null)
            {
                MessageBox.Show("Không tìm thấy chi tiết phiếu nhập nguyên liệu để sửa thông tin");
                return false;
            }
            if (!CServices.kiemTraThongTin(chiTietPhieuNhapNguyenLieu))
            {
                MessageBox.Show("Thông tin không hợp lệ");
                return false;
            }
            temp.maChiTietPhieuNhap = chiTietPhieuNhapNguyenLieu.maChiTietPhieuNhap;
            temp.maChitietNguyenLieu = chiTietPhieuNhapNguyenLieu.maChitietNguyenLieu;
            temp.soLuong = chiTietPhieuNhapNguyenLieu.soLuong;
            temp.donGia = chiTietPhieuNhapNguyenLieu.donGia;
            temp.thanhTien = chiTietPhieuNhapNguyenLieu.thanhTien;
            quanLyQuanCoffee.SaveChanges();
            return true;
        }

        public static bool remove(string maChiTietPhieuNhap)
        {
            ChiTietPhieuNhap temp = find(maChiTietPhieuNhap);
            if (temp == null)
            {
                MessageBox.Show("Không tìm thấy chi tiết phiếu nhập nguyên liệu để xóa");
                return false;
            }
            if (temp.ChiTietNguyenLieu != null)
            {
                MessageBox.Show("Không thể xóa chi tiết phiếu nhập nguyên liệu này");
                return false;
            }
            quanLyQuanCoffee.ChiTietPhieuNhaps.Remove(temp);
            quanLyQuanCoffee.SaveChanges();
            return true;
        }


        //ham danh cho phiếu xuất
        public static ChiTietPhieuNhap findMaChiTietNguyenLieu(string maChiTietNguyenLieu)
        {
            ChiTietPhieuNhap chiTietPhieuNhapNguyenLieu = quanLyQuanCoffee.ChiTietPhieuNhaps.Find(maChiTietNguyenLieu);
            return chiTietPhieuNhapNguyenLieu == null ? new ChiTietPhieuNhap() : chiTietPhieuNhapNguyenLieu;
        }


        public static bool remove(ChiTietPhieuNhap chiTietPhieuNhap)
        {
            return remove(chiTietPhieuNhap.maChiTietPhieuNhap);
        }

        public static bool CapNhapSoLuong_CTPhieuNhap(List<ChiTietPhieuXuat> list)
        {
            if (list.Count > 0)
            {
                foreach (var temp in list)
                {
                    ChiTietPhieuNhap chiTietNL = findTheoMachiTietNL(temp.maChitietNguyenLieu);
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
