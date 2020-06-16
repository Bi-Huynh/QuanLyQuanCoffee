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
    class CNhanVien_BUS
    {
        private static QuanLyQuanCoffeeEntities quanLyQuanCoffee = new QuanLyQuanCoffeeEntities();

        // Trả về toàn bộ danh sách nhân viên
        public static List<NhanVien> toList()
        {
            List<NhanVien> list = quanLyQuanCoffee.NhanViens.ToList();
            return list == null ? new List<NhanVien>() : list;
        }

        // Trả về những nhân viên có mã loại được truyền vào
        public static List<NhanVien> toListByLoai(string maLoaiNhanVien)
        {
            List<NhanVien> list = quanLyQuanCoffee.NhanViens.Where(x => x.maLoaiNhanVien.Contains(maLoaiNhanVien) == true).ToList();
            return list == null ? new List<NhanVien>() : list;
        }

        // tìm kiếm nhân viên theo mã nhân viên
        public static NhanVien find(string maNhanVien)
        {
            maNhanVien = maNhanVien.ToUpper();
            NhanVien nhanVien = quanLyQuanCoffee.NhanViens.Find(maNhanVien);
            return nhanVien == null ? new NhanVien() : nhanVien;
        }

        // tìm kiếm nhân viên theo mã nhân viên
        public static List<NhanVien> findListMa(string maNhanVien)
        {
            maNhanVien = maNhanVien.ToUpper();
            List<NhanVien> list = quanLyQuanCoffee.NhanViens.
                Where(x => x.maNhanVien.Contains(maNhanVien) == true).ToList();
            return list == null ? new List<NhanVien>() : list;
        }

        // tìm kiếm nhân viên theo tên nhân viên
        public static List<NhanVien> findTen(string tenNhanVien)
        {
            tenNhanVien = CServices.formatChuoi(tenNhanVien).ToLower();
            List<NhanVien> list = toList().Where(x => x.tenNhanVien.
                ToLower().Contains(tenNhanVien) == true).ToList();
            return list == null ? new List<NhanVien>() : list;
        }

        // tìm kiếm nhân viên theo tên nhân viên
        public static List<NhanVien> findTenLoai(string tenLoaiNhanVien)
        {
            tenLoaiNhanVien = CServices.formatChuoi(tenLoaiNhanVien).ToLower();
            List<NhanVien> list = toList().Where(x => x.LoaiNhanVien.tenLoai.
                ToLower().Contains(tenLoaiNhanVien) == true).ToList();
            return list == null ? new List<NhanVien>() : list;
        }

        public static int tinhTuoi(DateTime ngaySinh)
        {
            int tuoi = 0;
            int now = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
            int ns = int.Parse(ngaySinh.ToString("yyyyMMdd"));
            tuoi = (now - ns) / 10000;
            return tuoi;
        }

        public static int tinhTuoi(NhanVien nhanVien)
        {
            return tinhTuoi(nhanVien.ngaySinh);
        }

        public static bool add(NhanVien nhanVien)
        {
            if (CServices.kiemTraThongTin(nhanVien))
            {
                try
                {
                    nhanVien.hoNhanVien = CServices.formatChuoi(nhanVien.hoNhanVien);
                    nhanVien.tenNhanVien = CServices.formatChuoi(nhanVien.tenNhanVien);
                    nhanVien.hoNhanVien = CServices.formatChuoi(nhanVien.hoNhanVien);
                    nhanVien.soDienThoai = nhanVien.soDienThoai.Trim();
                    nhanVien.cMND = nhanVien.cMND.Trim();
                    nhanVien.thuongTru = CServices.formatChuoi(nhanVien.thuongTru);
                    nhanVien.tamTru = CServices.formatChuoi(nhanVien.tamTru);

                    quanLyQuanCoffee.NhanViens.Add(nhanVien);
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

        public static bool edit(NhanVien nhanVien)
        {
            NhanVien temp = find(nhanVien.maNhanVien);
            if (temp == null || !CServices.kiemTraThongTin(nhanVien))
            {
                return false;
            }

            try
            {
                temp.maNhanVien = nhanVien.maNhanVien;
                temp.hoNhanVien = CServices.formatChuoi(nhanVien.hoNhanVien);
                temp.tenNhanVien = CServices.formatChuoi(nhanVien.tenNhanVien);
                temp.soDienThoai = nhanVien.soDienThoai.Trim();
                temp.ngaySinh = nhanVien.ngaySinh;
                temp.phai = nhanVien.phai;
                temp.cMND = nhanVien.cMND.Trim();
                temp.thuongTru = CServices.formatChuoi(nhanVien.thuongTru);
                temp.tamTru = CServices.formatChuoi(nhanVien.tamTru);
                temp.ngayVaoLam = nhanVien.ngayVaoLam;
                temp.maLoaiNhanVien = nhanVien.maLoaiNhanVien;
                temp.urlAnh = nhanVien.urlAnh;
                temp.trangThai = nhanVien.trangThai;
                quanLyQuanCoffee.SaveChanges();
            }
            catch (DbUpdateException)
            {
                MessageBox.Show("Lỗi! không thể sửa thông tin nhân viên");
                return false;
            }
            return true;
        }

        public static bool remove(NhanVien nhanVien)
        {
            NhanVien temp = find(nhanVien.maNhanVien);
            if (temp == null)
            {
                MessageBox.Show("Không tìm thấy nhân viên để xóa");
                return false;
            }
            if (temp.ChiTietChamCongs.Count > 0 ||
                temp.Luongs.Count > 0 ||
                temp.PhieuNhapNguyenLieux.Count > 0 ||
                temp.PhieuXuatNguyenLieux.Count > 0 ||
                temp.TaiKhoan != null ||
                temp.HoaDons.Count > 0)
            {
                MessageBox.Show("Không thể xóa nhân viên này");
                return false;
            }
            try
            {
                quanLyQuanCoffee.NhanViens.Remove(temp);
                quanLyQuanCoffee.SaveChanges();
            }
            catch (DbUpdateException)
            {
                MessageBox.Show("Lỗi! Không thể xóa nhân viên");
                return false;
            }
            return true;
        }
    }
}
