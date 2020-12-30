using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using framework_web_01.Models;

namespace framework_web_01.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang
        dbforwebprojectEntities DB = new dbforwebprojectEntities();
        // GET: GioHang
        const string cartname = "giohang";
        public ActionResult Index()
        {
            var giohang = (List<GioHang>)Session[cartname];

            //taikhoan khachhang = DB.taikhoans.Where(s => s.tendangnhap == (string)Session["tendangnhap"]).ToList().FirstOrDefault();
            return View(giohang);
        }


        public ActionResult ThemGioHang(int id)
        {
            if (Session["role"] == null)
            {
                Response.Redirect("https://localhost:44321/TaiKhoan/DangNhap");
            }
            var giohang = (List<GioHang>)Session[cartname];
            var sanpham = DB.sanphams.Find(id);
            if (giohang != null)
            {
                if (giohang.Exists(x => x.sanpham.masp == id))
                {
                    var sp = giohang.Where(s => s.sanpham.masp == id).FirstOrDefault();
                    sp.SoLuong += sp.SoLuong;
                }
                else
                {
                    GioHang gh = new GioHang(sanpham, 1);
                    giohang.Add(gh);
                }
                Session[cartname] = giohang;
            }
            else
            {
                GioHang gh = new GioHang(sanpham, 1);
                var list = new List<GioHang>();
                list.Add(gh);
                Session[cartname] = list;
            }
            return Redirect(Request.UrlReferrer.ToString());
        }
        [HttpPost]
        public ActionResult TinhLai(int[] id, int[] soluong)
        {
            var k = 0;
            var giohang = (List<GioHang>)Session[cartname];
            foreach (int i in id)
            {
                if (giohang.Exists(s => s.sanpham.masp == i))
                {
                    var sp = giohang.Where(s => s.sanpham.masp == i).FirstOrDefault();
                    sp.SoLuong = soluong[k];
                }
                k++;
            }
            return RedirectToAction("Index");
        }
        public ActionResult Diachi()
        {

            ViewBag.Title = "Diachi";
            string tendn = (string)Session["tendangnhap"];
            var sp = (from p in DB.taikhoans where p.tendangnhap == tendn select p).ToList();
            return View(sp);
        }
        [HttpPost]
        public ActionResult Diachi(FormCollection collection, hoadon hd)
        {
            hd = new hoadon();
            string stendangnhap = (string)Session["tendangnhap"];
            double stongtien = Convert.ToDouble(Session["tongtien"]);
            string ssdt = collection["sodt"];
            string sdiachi = collection["diachi"];
            string shoten = collection["name"];
            //gan du lieu vao model
            hd.tendangnhap = stendangnhap;
            hd.tien = stongtien;
            hd.sdt = ssdt;
            hd.diachi = sdiachi;
            hd.hoten = shoten;
            Session["dathang"] = hd;
            return RedirectToAction("DatHang");
        }
        [HttpGet]
        public ActionResult DatHang()
        {
            ViewBag.gh = (List<GioHang>)Session[cartname];
            ViewBag.dh = (hoadon)Session["dathang"];
            return View();
        }
        [HttpPost]
        public ActionResult DatHang(FormCollection collection)
        {
            List<GioHang> gh = (List<GioHang>)Session[cartname];
            hoadon dh = (hoadon)Session["dathang"];
            DB.hoadons.Add(dh);
            DB.SaveChanges();

            int mahdMax = DB.hoadons.Max(u => u.mahd);

            foreach (var item in gh)
            {
                cthd chitiet = new cthd();
                chitiet.tensp = item.sanpham.tensp;
                chitiet.mahd = mahdMax;
                chitiet.masp = item.sanpham.masp;
                chitiet.soluong = Convert.ToInt32(item.SoLuong);
                chitiet.thanhtien = Convert.ToInt32(item.SoLuong) * Convert.ToInt32(item.sanpham.gia);
                DB.cthds.Add(chitiet);
                DB.SaveChanges();

            }

            Session.Remove("giohang");
            return RedirectToAction("ThongBao");
        }
        public ActionResult ThongBao()
        {
            return View();
        }



    }
}