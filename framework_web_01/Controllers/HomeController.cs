using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using framework_web_01.Models;

namespace framework_web_01.Controllers
{
    public class HomeController : Controller
    {
        dbforwebprojectEntities DB = new dbforwebprojectEntities();

        public ActionResult Index()
        {
            ViewBag.Title = "Home";
            var sp = (from p in DB.sanphams orderby p.masp descending select p).Take(8).ToList();
            if (Session["tim"] != null)
            {
                Session.Remove("tim");
            }
                return View(sp);
        }

        public ActionResult Tui()
        {
            ViewBag.Title = "Tui";
            var sp = (from p in DB.sanphams where p.loai > 0 where p.loai < 2 select p).ToList();
            return View(sp);
        }
        public ActionResult Giay()
        {
            ViewBag.Title = "Giay";
            var sp = (from p in DB.sanphams where p.loai > 1 where p.loai < 3 select p).ToList();
            return View(sp);
        }
        public ActionResult Ao()
        {
            ViewBag.Title = "Ao";
            var sp = (from p in DB.sanphams where p.loai > 2 where p.loai < 4 select p).ToList();
            return View(sp);
        }
        public ActionResult Quan()
        {
            ViewBag.Title = "Quan";
            var sp = (from p in DB.sanphams where p.loai > 3 where p.loai < 5 select p).ToList();
            return View(sp);
        }
        public ActionResult Yem()
        {
            ViewBag.Title = "Yem";
            var sp = (from p in DB.sanphams where p.loai > 4 where p.loai < 6 select p).ToList();
            return View(sp);
        }
        public ActionResult ChanVay()
        {
            ViewBag.Title = "ChanVay";

            var sp = (from p in DB.sanphams where p.loai > 5 where p.loai < 7 select p).ToList();
            return View(sp);
        }
        public ActionResult Vay()
        {
            ViewBag.Title = "Vay";
            var sp = (from p in DB.sanphams where p.loai > 6 where p.loai < 8 select p).ToList();
            return View(sp);
        }


    }
}