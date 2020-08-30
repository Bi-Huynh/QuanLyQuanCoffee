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
        private static QuanLyQuanCoffeeEntities1 quanLyQuanCoffee = LoadDatabase.Instance();

        // Trả về toàn bộ danh sách nhân viên
        public static List<NhanVien> toList()
        {
            List<NhanVien> list = quanLyQuanCoffee.NhanViens.Where(x => x.trangThai == 0 &&
                x.maNhanVien != "0000000000" ||
                x.trangThai == 1).ToList();
            return list == null ? new List<NhanVien>() : list;
        }

        // Trả về toàn bộ danh sách nhân viên
        public static List<NhanVien> toListNotAccount()
        {
            List<NhanVien> nhanViens = new List<NhanVien>();
            foreach (NhanVien nhanVien in toList())
            {
                bool flag = false;
                foreach (TaiKhoan taiKhoan in CTaiKhoan_BUS.toList())
                {
                    if (taiKhoan.maNhanVien == nhanVien.maNhanVien)
                    {
                        flag = true;
                    }
                }
                if (flag == false)
                {
                    nhanViens.Add(nhanVien);
                }
            }
            return nhanViens;
        }

        // Trả về toàn bộ danh sách nhân viên
        public static List<NhanVien> toListAll()
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
            //maNhanVien = maNhanVien.ToUpper();
            NhanVien nhanVien = quanLyQuanCoffee.NhanViens
                .Where(x => x.maNhanVien == maNhanVien && x.trangThai == 0 || x.trangThai == 1).FirstOrDefault();
            return nhanVien == null ? new NhanVien() : nhanVien;
        }

        // tìm kiếm nhân viên theo mã nhân viên
        public static List<NhanVien> findListMa(string maNhanVien)
        {
            maNhanVien = maNhanVien.ToUpper();
            List<NhanVien> list = quanLyQuanCoffee.NhanViens.
                Where(x => x.maNhanVien.Contains(maNhanVien) == true && x.trangThai == 0).ToList();
            return list == null ? new List<NhanVien>() : list;
        }

        // tìm kiếm nhân viên theo tên nhân viên
        public static List<NhanVien> findTen(string tenNhanVien)
        {
            tenNhanVien = CServices.formatChuoi(tenNhanVien).ToLower();
            List<NhanVien> list = new List<NhanVien>();
            try
            {
                foreach (NhanVien nhanVien in toList())
                {
                    if (nhanVien.tenNhanVien.ToLower().Contains(tenNhanVien) &&
                        nhanVien.trangThai == 0)
                    {
                        list.Add(nhanVien);
                    }
                }
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return list;
        }

        // tìm kiếm nhân viên theo tên nhân viên
        public static string findTenbyMa(string tenNhanVien)
        {
            if (tenNhanVien != null && tenNhanVien != "")
            {
                foreach (NhanVien nhanVien in toList())
                {
                    string hoTen = nhanVien.hoNhanVien + " " + nhanVien.tenNhanVien;
                    if (hoTen == tenNhanVien)
                    {
                        return nhanVien.maNhanVien;
                    }
                }
            }
            return "";
        }

        // tìm kiếm nhân viên theo tên mã nhân viên trả về tên nhân viên đó
        public static string findMabyTen(string maNhanVien)
        {
            string hoTen = "";
            foreach (NhanVien nhanVien in toList())
            {
                if (nhanVien.maNhanVien == maNhanVien)
                {
                    hoTen = nhanVien.hoNhanVien + " " + nhanVien.tenNhanVien;
                    return hoTen;
                }
            }
            return "";
        }

        // tìm kiếm nhân viên theo tên nhân viên
        public static List<NhanVien> findTenLoai(string tenLoaiNhanVien)
        {
            tenLoaiNhanVien = CServices.formatChuoi(tenLoaiNhanVien).ToLower();
            List<NhanVien> list = new List<NhanVien>();
            try
            {
                foreach (NhanVien nhanVien in toList())
                {
                    if (nhanVien.LoaiNhanVien.tenLoai.ToLower().Contains(tenLoaiNhanVien) &&
                        nhanVien.trangThai == 0)
                    {
                        list.Add(nhanVien);
                    }
                }
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return list;
        }

        public static int tinhTuoi(DateTime ngaySinh)
        {
            int tuoi = 0;
            try
            {
                int now = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
                int ns = int.Parse(ngaySinh.ToString("yyyyMMdd"));
                tuoi = (now - ns) / 10000;
                if (tuoi < 18 || tuoi > 65)
                {
                    return -1;
                }
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Lỗi! để dữ liệu rỗng");
            }
            catch (FormatException)
            {
                MessageBox.Show("Lỗi! định dạng kiểu dữ liệu");
            }
            catch (OverflowException)
            {
                MessageBox.Show("Lỗi! dữ liệu có giá trị vượt quá giới hạn cho phép");
            }

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

                if (temp.trangThai == 0)
                {
                    temp.TaiKhoans.FirstOrDefault().trangThai = 0;
                }

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
            try
            {
                if (temp.PhieuNhapNguyenLieux.Count > 0 ||
                    temp.PhieuXuatNguyenLieux.Count > 0 ||
                    temp.TaiKhoans != null ||
                    temp.HoaDons.Count > 0)
                {
                    temp.trangThai = 2;
                    temp.TaiKhoans.FirstOrDefault().trangThai = 1;

                    quanLyQuanCoffee.SaveChanges();
                }
                else
                {
                    quanLyQuanCoffee.NhanViens.Remove(temp);
                    quanLyQuanCoffee.SaveChanges();
                }
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
