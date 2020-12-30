using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace framework_web_01.Models
{
    public class GioHang
    {
        public sanpham sanpham { get; set; }
        public double SoLuong { get; set; }
        public GioHang(sanpham sp, int soluong)
        {
            this.sanpham = sp;
            this.SoLuong = soluong;
        }
    }
}