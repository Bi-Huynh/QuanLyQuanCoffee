using QuanLyQuanCoffee.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCoffee.DTO
{
    class CLoaiNguyenLieu_DTO : CBaseVM
    {
        private string maloainguyenlieu;
        private string tenLoaiNguyenLieu;

        public string maLoaiNguyenLieu
        {
            get => maloainguyenlieu;
            set
            {
                maloainguyenlieu = value;
                capNhat("maLoaiNguyeLieu");
            }
        }
        public string TenLoaiNguyenLieu
        {
            get => tenLoaiNguyenLieu;
            set 
            { 
                tenLoaiNguyenLieu = value;
                capNhat("tenLoaiNguyenLieu");
            }
        }

        public CLoaiNguyenLieu_DTO()
        {
            this.maloainguyenlieu = "";
            this.tenLoaiNguyenLieu = "";
        }
    }
}
