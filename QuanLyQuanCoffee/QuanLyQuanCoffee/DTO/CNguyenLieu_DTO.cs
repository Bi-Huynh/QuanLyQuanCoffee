using QuanLyQuanCoffee.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCoffee.DTO
{
    class CNguyenLieu_DTO : CBaseVM
    {
        private string manguyenlieu;
        private string tennguyenlieu;
        private string dongia;
        private string soluong;
        private string donvitinh;
        private string ngayhethan;
        private string ngaynhap;
        private string maloainguyenlieu;

        public string maNguyenLieu
        {
            get => manguyenlieu; set
            {
                manguyenlieu = value;
                capNhat("maNguyenLieu");
            }
        }
        public string tenNguyenLieu
        {
            get => tennguyenlieu; set
            {
                tennguyenlieu = value;
                capNhat("tenNguyenLieu");
            }
        }
        public string donGia
        {
            get => dongia; set
            {
                dongia = value;
                capNhat("donGia");
            }
        }
        public string soLuong
        {
            get => soluong; set
            {
                soluong = value;
                capNhat("soLuong");
            }
        }
        public string donViTinh
        {
            get => donvitinh; set
            {
                donvitinh = value;
                capNhat("donViTinh");
            }
        }
        public string ngayHetHan
        {
            get => ngayhethan; set
            {
                ngayhethan = value;
                capNhat("ngayHetHan");
            }
        }
        public string ngayNhap
        {
            get => ngaynhap; set
            {
                ngaynhap = value;
                capNhat("ngayNhap");
            }
        }
        public string maLoaiNguyenLieu
        {
            get => maloainguyenlieu; set
            {
                maloainguyenlieu = value;
                capNhat("maLoaiNguyenLieu");
            }
        }

        public CNguyenLieu_DTO()
        {
            this.manguyenlieu = "";
            this.tennguyenlieu = "";
            this.dongia = "0";
            this.soluong = "0";
            this.donvitinh = "";
            this.ngayHetHan = "";
            this.ngaynhap = "";
            this.maloainguyenlieu = "";
        }

        public NguyenLieu parse()
        {
            return new NguyenLieu(
                this.manguyenlieu,
                this.tennguyenlieu,
                double.Parse(this.dongia),
                int.Parse(this.soluong),
                this.donViTinh,
                DateTime.Parse(this.ngayHetHan),
                DateTime.Parse(this.ngayNhap),
                this.maLoaiNguyenLieu
                );
        }
    }
}
