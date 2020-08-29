using QuanLyQuanCoffee.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLyQuanCoffee.BUS
{
    class CTaiKhoan_BUS
    {
        private static QuanLyQuanCoffeeEntities1 quanLyQuanCoffee = LoadDatabase.Instance();
        private static string key = "2giotoitaigoccayda";

        public static List<TaiKhoan> toList()
        {
            List<TaiKhoan> list = quanLyQuanCoffee.TaiKhoans.ToList();
            return list == null ? new List<TaiKhoan>() : list;
        }

        public static List<TaiKhoan> toListNotAdmin()
        {
            List<TaiKhoan> taiKhoans = new List<TaiKhoan>();
            foreach (TaiKhoan taiKhoan in quanLyQuanCoffee.TaiKhoans)
            {
                if (taiKhoan.maNhanVien != "0000000000")
                {
                    taiKhoans.Add(taiKhoan);
                }
            }
            return taiKhoans;
        }

        public static bool findTK(string tenTaiKhoan)
        {
            foreach (TaiKhoan taiKhoan in quanLyQuanCoffee.TaiKhoans)
            {
                if (taiKhoan.tenTaiKhoan == tenTaiKhoan)
                {
                    return true;
                }
            }
            return false; // chưa tồn tại
        }

        public static TaiKhoan find(string maTaiKhoan)
        {
            TaiKhoan taiKhoan = new TaiKhoan();
            foreach (TaiKhoan tk in quanLyQuanCoffee.TaiKhoans.ToList())
            {
                if (tk.maTaiKhoan == maTaiKhoan)
                {
                    taiKhoan = tk;
                    break;
                }
            }

            return taiKhoan;
        }

        public static Boolean findTrangThai(string maTaiKhoan)
        {
            TaiKhoan taiKhoan = quanLyQuanCoffee.TaiKhoans.Find(maTaiKhoan);
            if (taiKhoan.trangThai == 0)
            {
                return false;
            }
            return true;
        }

        public static string KTtaiKhoanDaXoa(TaiKhoan taiKhoan)
        {
            string a = MessageBox.Show("Nhân viên này đã có tài khoản nhưng đã được xóa, bạn có muốn phục hồi không", "Thông báo", MessageBoxButton.YesNo).ToString();
            return a;
        }
        public static List<string> toListByMaLoaiNV()
        {
            List<string> list = quanLyQuanCoffee.NhanViens.Select(x => x.maNhanVien).ToList();
            return list == null ? new List<string>() : list;
        }

        /// <summary>
        /// Mã hóa chuỗi có mật khẩu
        /// </summary>
        /// <param name="toEncrypt">Chuỗi cần mã hóa</param>
        /// <returns>Chuỗi đã mã hóa</returns>
        public static string Encrypt(string toEncrypt)
        {
            bool useHashing = true;
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static bool add(TaiKhoan taiKhoan)
        {
            if (CServices.kiemTraThongTin(taiKhoan))
            {
                try
                {
                    quanLyQuanCoffee.TaiKhoans.Add(taiKhoan);
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

        public static bool remove(TaiKhoan taiKhoan)
        {
            try
            {
                TaiKhoan temp = find(taiKhoan.maTaiKhoan);
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

        public static bool edit(TaiKhoan taiKhoan)
        {
            TaiKhoan temp = find(taiKhoan.maTaiKhoan);
            if (temp != null)
            {
                try
                {
                    temp.tenTaiKhoan = taiKhoan.tenTaiKhoan;
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

        public static bool hoiPhucTK(TaiKhoan taiKhoan)
        {
            TaiKhoan temp = find(taiKhoan.maNhanVien);
            if (temp != null)
            {
                try
                {
                    temp.tenTaiKhoan = taiKhoan.tenTaiKhoan;
                    temp.matKhau = taiKhoan.matKhau;
                    //temp.LoaiTaiKhoan = taiKhoan.LoaiTaiKhoan;
                    temp.trangThai = 0;
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

        public static bool KTRong(TaiKhoan taiKhoan)
        {
            if (taiKhoan.maNhanVien == null)
            {
                return false;
            }
            if (taiKhoan.tenTaiKhoan == "" || taiKhoan.matKhau == "")
            {
                return false;
            }

            return true;
        }

        public static bool doiMatKhau(TaiKhoan tk = null, string matKhauMoi = "")
        {
            try
            {
                TaiKhoan taiKhoan = quanLyQuanCoffee.TaiKhoans.Find(tk.maTaiKhoan);
                if (taiKhoan == null)
                {
                    MessageBox.Show("Lỗi! Không tìm được tài khoản");
                    return false;
                }
                else
                {
                    if (matKhauMoi == "1")
                    {
                        MessageBox.Show("Bạn không thể đổi mật khẩu mới là mật khẩu mặc định");
                        return false;
                    }
                    taiKhoan.matKhau = Encrypt(matKhauMoi);
                    taiKhoan.trangThai = 0;

                    quanLyQuanCoffee.SaveChanges();
                    return true;
                }
            }
            catch (DbUpdateException)
            {
                MessageBox.Show("Không thể lưu vào cơ sở dữ liệu");
            }

            return false;
        }

        public static bool khoaTaiKhoan(string maTaiKhoan)
        {
            try
            {
                TaiKhoan taiKhoan = find(maTaiKhoan);
                if (taiKhoan.maTaiKhoan == "0000000001")
                {
                    MessageBox.Show("Không thể khóa tài khoản này");
                }
                else
                {
                    taiKhoan.trangThai = 1;
                    quanLyQuanCoffee.SaveChanges();
                }
            }
            catch (DbUpdateException)
            {
                MessageBox.Show("lỗi khóa tài khoản ,Không thể cập nhập database");
                return false;
            }

            return true;
        }

        public static bool khoaTaiKhoanNV(string maNhanVien)
        {
            TaiKhoan taiKhoan = new TaiKhoan();
            foreach (TaiKhoan tk in quanLyQuanCoffee.TaiKhoans.ToList())
            {
                if (tk.maNhanVien == maNhanVien)
                {
                    taiKhoan = tk;
                    break;
                }
            }
            if (taiKhoan != null)
            {
                return khoaTaiKhoan(taiKhoan.maTaiKhoan);
            }
            MessageBox.Show("Không tìm thấy tài khoản của nhân viên này");
            return false;
        }

        public static bool moKhoaTaiKhoan(string maTaiKhoan)
        {
            try
            {
                TaiKhoan taiKhoan = find(maTaiKhoan);
                if (taiKhoan != null)
                {
                    //NhanVien nhanVien = CNhanVien_BUS.find(taiKhoan.maNhanVien);
                    if (taiKhoan.NhanVien.trangThai == 2)
                    {
                        MessageBox.Show("Nhân viên này đã nghỉ việc, không thể mở tài khoản này");
                        return false;
                    }
                    else
                    {
                        taiKhoan.trangThai = 0;
                        quanLyQuanCoffee.SaveChanges();
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thầy tài khoản");
                    return false;
                }
            }
            catch (DbUpdateException)
            {
                MessageBox.Show("lỗi mở khóa tài khoản ,Không thể cập nhập database");
                return false;
            }

            return true;
        }
    }
}
