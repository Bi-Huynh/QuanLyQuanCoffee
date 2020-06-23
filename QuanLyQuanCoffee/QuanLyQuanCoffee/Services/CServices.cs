using Prism.Regions;
using QuanLyQuanCoffee.BUS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLyQuanCoffee.Services
{
    class CServices
    {
        private static QuanLyQuanCoffeeEntities1 quanLyQuanCoffee = new QuanLyQuanCoffeeEntities1();

        // khởi tạo mã tự động
        public static string randomMa()
        {
            Random random = new Random();
            string ma = "";
            for (int i = 0; i < 10; i++)
            {
                ma += Convert.ToString((char)random.Next(65, 90));
            }
            return ma;
        }

        public static string taoMa<T>(List<T> list)
        {
            string ma = "";
            if (list.Count() == 0)
            {
                ma = "0000000001";
            }
            else
            {
                try
                {
                    T temp = list[list.Count() - 1];
                    double thuTu = int.Parse(temp.ToString());
                    ++thuTu;
                    if (thuTu == 9999999999)
                    {
                        MessageBox.Show("Mã đã tới giới hạn, không thể tăng nữa");
                        return "";
                    }
                    ma = String.Format("{0:0000000000}", thuTu);
                }
                catch (ArgumentNullException)
                {
                    MessageBox.Show("Lỗi, tạo mã tự động 1");
                }
                catch (FormatException)
                {
                    MessageBox.Show("Lỗi, tạo mã tự động 2");
                }
                catch (OverflowException)
                {
                    MessageBox.Show("Lỗi, tạo mã tự động 3");
                }
            }
            return ma;
        }

        public static string taoMaLoaiTaiKhoan(List<LoaiTaiKhoan> list)
        {
            string ma = "";
            if (list.Count() == 0)
            {
                ma = "00001";
            }
            else
            {
                try
                {
                    LoaiTaiKhoan temp = list[list.Count() - 1];
                    double thuTu = int.Parse(temp.ToString());
                    ++thuTu;
                    if (thuTu == 99999)
                    {
                        MessageBox.Show("Mã đã tới giới hạn, không thể tăng nữa");
                        return "";
                    }
                    ma = String.Format("{0:00000}", thuTu);
                }
                catch (ArgumentNullException)
                {
                    MessageBox.Show("Lỗi, tạo mã tự động 1");
                }
                catch (FormatException)
                {
                    MessageBox.Show("Lỗi, tạo mã tự động 2");
                }
                catch (OverflowException)
                {
                    MessageBox.Show("Lỗi, tạo mã tự động 3");
                }
            }
            return ma;
        }

        // định dạng lại chuỗi được truyền vào thành chuỗi chuẩn
        public static string formatChuoi(string strInput)
        {
            if (strInput == "" || strInput == null)
            {
                return "";
            }
            string result = "";
            result = strInput.Trim();
            while (result.Contains("  ") == true)
            {
                result = result.Replace("  ", " ");
            }
            result = result.ToLower();
            string[] arr = result.Split(' ');
            result = string.Empty;
            foreach (var item in arr)
            {
                String temp = item.Substring(0, 1).ToUpper() + item.Substring(1);
                result += temp + " ";
            }
            result = result.Trim();
            return result;
        }

        private static bool kiemTraTonTaiSo(string chuoi)
        {
            foreach (char item in chuoi)
            {
                if ((int)item >= 48 && (int)item <= 57)
                {
                    return true;
                }
            }
            return false;
        }

        private static bool kiemTraTonTaiChu(string chuoi)
        {
            foreach (char item in chuoi)
            {
                if ((int)item < 48 || (int)item > 57)
                {
                    return true;
                }
            }
            return false;
        }
        private static bool kiemTraTonTaiKhoangTrang(string chuoi)
        {
            string chuoi1;
            for (int i = 0; i < chuoi.Length; i++)
            {
                chuoi1 = chuoi.Substring(i, 1);
                if (chuoi1 == " ")
                {
                    return false;
                }
            }
            return true;
        }

        public static bool kiemTraThongTin(NhanVien nhanVien)
        {
            if (kiemTraTonTaiSo(nhanVien.hoNhanVien) == true ||
                kiemTraTonTaiSo(nhanVien.tenNhanVien) == true)
            {
                MessageBox.Show("Họ Tên không được có ký tự số");
                return false;
            }
            if (kiemTraTonTaiChu(nhanVien.soDienThoai.Trim()) == true ||
                kiemTraTonTaiChu(nhanVien.cMND.Trim()) == true)
            {
                MessageBox.Show("số điện thoại và CMND chỉ có ký tự số");
                return false;
            }
            if (nhanVien.cMND.Trim().Count() != 9 && nhanVien.cMND.Trim().Count() != 12)
            {
                MessageBox.Show("CMND chỉ có 9 ký tự số hoặc 12 ký tự số");
                return false;
            }
            if (nhanVien.soDienThoai.Trim().Count() != 10)
            {
                MessageBox.Show("Số điện thoại chỉ có 10 số");
                return false;
            }
            if (CNhanVien_BUS.tinhTuoi(nhanVien) == -1)
            {
                MessageBox.Show("Tuổi đi làm là 18 đến 65");
                return false;
            }
            return true;
        }

        public static bool kiemTraThongTin(LoaiNhanVien loaiNhanVien)
        {
            if (loaiNhanVien.luongCoBan < 0)
            {
                MessageBox.Show("Lương cơ bản phải là số nguyên dương");
                return false;
            }
            return true;
        }

        public static bool kiemTraThongTin(TaiKhoan taiKhoan)
        {
            if (kiemTraTonTaiKhoangTrang(taiKhoan.taiKhoan1) == false)
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu không được dùng Khoảng Trắng(Space)");
                return false;
            }
            if (kiemTraTonTaiKhoangTrang(taiKhoan.matKhau) == false)
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu không được dùng Khoảng Trắng(Space)");
                return false;
            }
            return true;
        }

        public static string formatTK_MT(string strInput)
        {
            string TK_MK = "";
            for (int i = 0; i <= strInput.Length; i++)
            {
                if (strInput[i] != ' ')
                {
                    TK_MK = TK_MK + strInput[i];
                }
            }
            return TK_MK;
        }

        public static bool kiemTraThongTin(NguyenLieu nguyenLieu)
        {
            //if (nguyenLieu.soLuong <= 0)
            //{
            //    MessageBox.Show("Lương cơ bản phải là số nguyên dương và lớn hơn 0");
            //    return false;
            //}
            //if (nguyenLieu.ngayHetHan < DateTime.Now)
            //{
            //    MessageBox.Show("Ngày hết hạn phải lớn hơn hoặc bằng ngày hiện tại");
            //    return false;
            //}
            return true;
        }
        public static bool kiemTraThongTin(HoaDon hoaDon)
        {
            if (hoaDon.tongThanhTien < 0)
            {
                return false;
            }
            return true;
        }
        public static bool kiemTraThongTin(LoaiNguyenLieu loaiNguyenLieu)
        {
            return true;
        }

        public static bool kiemTraThongTin(ChiTietNguyenLieu chiTietNguyenLieu)
        {
            return true;
        }

        public static bool kiemTraThongTin(PhieuNhapNguyenLieu phieuNhapNguyenLieu)
        {
            return true;
        }

        public static bool kiemTraThongTin(ChiTietPhieuNhapNguyenLieu chiTietPhieuNhapNguyenLieu)
        {
            return true;
        }

        public static bool kiemTraThongTin(SanPham sanPham)
        {
            if (sanPham.donGia < 0)
            {
                MessageBox.Show("Đơn giá phải lơn hơn 0");
                return false;
            }
            return true;
        }
    }
}
