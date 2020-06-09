using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyQuanCoffee.Command
{
    class CMyCommand_LoaiNguyenLieu
    {
        public static RoutedCommand lenhThemLoaiNguyenLieu = new RoutedCommand();
        public static RoutedCommand lenhXoaLoaiNguyenLieu = new RoutedCommand();
        public static RoutedCommand lenhSuaLoaiNguyenLieu = new RoutedCommand();
        public static RoutedCommand lenhLuuLoaiNguyenLieu = new RoutedCommand();
    }
}
