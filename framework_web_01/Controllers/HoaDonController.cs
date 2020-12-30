using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using framework_web_01.Models;

namespace framework_web_01.Controllers
{

    public class HoaDonController : Controller
    {
        dbforwebprojectEntities DB = new dbforwebprojectEntities();
        // GET: HoaDon
        public ActionResult Index()
        {
            ViewBag.Title = "Index";
            var hd = (from p in DB.hoadons select p).ToList();
            return View(hd);
        }

        public ActionResult chitiethoadon(int mahd)
        {
            var chitiet = DB.cthds.Where(s => s.mahd == mahd).ToList().FirstOrDefault();
            var splq = DB.cthds.Where(s => s.mahd == chitiet.mahd).ToList();
            ViewBag.sqlq = splq;
            //Session["masp"] = id;
            return View(chitiet);
        }

    }
}