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

        public static bool findTK(string tenTaiKhoan)
        {
            foreach(TaiKhoan taiKhoan in quanLyQuanCoffee.TaiKhoans)
            {
                if (taiKhoan.taiKhoan1 == tenTaiKhoan)
                {
                    return true;
                }
            }    
            return false; // chưa tồn tại
        }

        public static TaiKhoan find(string maNhanVien)
        {
            TaiKhoan taiKhoan = quanLyQuanCoffee.TaiKhoans.Find(maNhanVien);
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

        public static List<string> toListByMaLoaiTK()
        {
            List<string> list = quanLyQuanCoffee.LoaiTaiKhoans.Select(x => x.maLoaiTaiKhoan).ToList();
            return list == null ? new List<string>() : list;
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
                TaiKhoan temp = find(taiKhoan.maNhanVien);
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
            TaiKhoan temp = find(taiKhoan.maNhanVien);
            if (temp != null)
            {
                try
                {
                    temp.taiKhoan1 = taiKhoan.taiKhoan1;
                    temp.matKhau = taiKhoan.matKhau;
                    temp.maLoaiTaiKhoan = taiKhoan.maLoaiTaiKhoan;
                    temp.trangThai = taiKhoan.trangThai;
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
                    temp.taiKhoan1 = taiKhoan.taiKhoan1;
                    temp.matKhau = taiKhoan.matKhau;
                    temp.LoaiTaiKhoan = taiKhoan.LoaiTaiKhoan;
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


            if (taiKhoan.maNhanVien.Length > 10)
            {
                return false;
            }
            if (taiKhoan.taiKhoan1 == "" || taiKhoan.matKhau == "" || taiKhoan.maLoaiTaiKhoan == "")
            {
                return false;
            }
            if (taiKhoan.maLoaiTaiKhoan == null)
            {
                return false;
            }

            return true;
        }

        public static bool doiMatKhau(TaiKhoan tk = null, string matKhauMoi = "")
        {
            try
            {
                TaiKhoan taiKhoan = quanLyQuanCoffee.TaiKhoans.Find(tk.maNhanVien);
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

        public static bool khoaTaiKhoan(string maNhanVien)
        {
            try
            {
                TaiKhoan taiKhoan = find(maNhanVien);
                if (taiKhoan.LoaiTaiKhoan.maLoaiTaiKhoan == "00001")
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

        public static bool moKhoaTaiKhoan(string maNhanVien)
        {
            try
            {
                TaiKhoan taiKhoan = find(maNhanVien);
                taiKhoan.trangThai = 0;
                quanLyQuanCoffee.SaveChanges();
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
