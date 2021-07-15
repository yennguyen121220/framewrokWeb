using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using framework_web_01.Models;
using System.Data.Entity;

namespace framework_web_01.Controllers
{
    public class SanPhamController : Controller
    {
        // GET: SanPham
        dbforwebprojectEntities DB = new dbforwebprojectEntities();
        // GET: SanPham
        #region[Create]
        public ActionResult ThemSP()
        {

            if (Convert.ToInt32(Session["role"]) == 1)
            {
                ViewBag.Title = "ThemSP";
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult ThemSP(FormCollection collection, sanpham sp)
        {
            int iloai = int.Parse(collection["loaisp"]);
            string stensp = collection["tensp"];
            double dgia = double.Parse(collection["gia"]);
            string shinhanh = collection["hinhanh"];
            string smota = collection["mota"];

            //gan du lieu vao model
            sp.loai = iloai;
            sp.tensp = stensp;
            sp.gia = dgia;
            sp.hinhanh = shinhanh;
            sp.mota = smota;

            DB.sanphams.Add(sp);
            int c = DB.SaveChanges();
            return RedirectToAction("Index","Home");

        }
        #endregion



        //xem chi tiết sản phầm + xóa
        public ActionResult Chitietsanpham(int id)
        {
            var kt = DB.sanphams.Where(s => s.masp == id).Count();//kiem tra taikhoan co ton tai trong database hay k
            if (Convert.ToInt32(kt) > 0)
            {
                var chitiet = DB.sanphams.Where(s => s.masp == id).ToList().FirstOrDefault();
                var splq = DB.sanphams.Where(s => s.masp == chitiet.masp).ToList().Take(1);
                ViewBag.sqlq = splq;
                Session["masp"] = id;
                return View(chitiet);
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult Chitietsanpham(FormCollection collection) //xoa
        {
            int idsp = int.Parse(collection["masp"]);
            var x = DB.sanphams.Where(s => s.masp == idsp).FirstOrDefault();
            DB.sanphams.Remove(x);
            DB.SaveChanges();
            return RedirectToAction("Index","Home");
        }



        [HttpGet]
        public ActionResult SuaSP()
        {
            if (Convert.ToInt32(Session["role"]) == 1)
            {
                int id = Convert.ToInt32(Session["masp"]);
                var chitiet = DB.sanphams.Where(s => s.masp == id).ToList().FirstOrDefault();
                var splq = DB.sanphams.Where(s => s.masp == chitiet.masp).ToList().Take(1);
                ViewBag.sqlq = splq;
                return View(chitiet);
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult SuaSP( sanpham Sanpham)
        {
            try
            {
                DB.Entry(Sanpham).State = EntityState.Modified;
                DB.SaveChanges();
                return RedirectToAction("Index", "Home");
            }catch{
                return View();
            }
            
        }



        //timkiem
        public ActionResult TimKiem()
        {
            string tim=Request.Form["search_name"];
            Session["timkiem"] = tim;
            //string tim = Convert.ToString(Content("${search_name}"));
            ViewBag.Title = "TimKiem";
            //string tim = Convert.ToString(Session["tim"]);
            //string tim = "nam";
            var sp = (from p in DB.sanphams where p.tensp.Contains(tim) select p).ToList();
            return View(sp);
        }
    }
}