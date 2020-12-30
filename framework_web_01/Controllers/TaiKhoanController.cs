using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using framework_web_01.Models;
using System.Data.Entity;

namespace framework_web_01.Controllers
{
    public class TaiKhoanController : Controller
    {
        // GET: TaiKhoan
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        dbforwebprojectEntities DB = new dbforwebprojectEntities();
        // GET: TaiKhoan

        #region[Create]
        public ActionResult DangKy()
        {
            ViewBag.Title = "DangKy";
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(FormCollection collection, taikhoan tk)
        {
            string stendangnhap = collection["username"];
            string shoten = collection["fullname"];
            string smatkhau = collection["password"];
            string ssdt = collection["telNumber"];
            string sdiachi = collection["address"];
            int irole = 0;
            if (collection["role"] == null)
            {
                irole = 0;
            }
            else
            {
                irole = int.Parse(collection["role"]);
            }
            //gan du lieu vao model
            tk.tendangnhap = stendangnhap;
            tk.hoten = shoten;
            tk.matkhau = smatkhau;
            tk.sdt = ssdt;
            tk.diachi = sdiachi;
            tk.role = irole;

            DB.taikhoans.Add(tk);
            DB.SaveChanges();
            return RedirectToAction("DangNhap");
        }
        #endregion;


        public ActionResult DangNhap()
        {
            return View();
        }

        public ActionResult XuLyDangNhap(FormCollection collection, framework_web_01.Models.taikhoan user)
        {
            ViewBag.er = "vui lòng đang nhâp";
            string stendangnhap = collection["username"];
            string smatkhau = collection["password"];
            var tk = (from p in DB.taikhoans where p.tendangnhap == stendangnhap where p.matkhau == smatkhau select p).ToList();

            if (tk.Count == 1)
            {
                for (int i = 0; i < tk.Count; i++)
                {
                    Session["role"] = tk[i].role;
                    Session["tendangnhap"] = tk[i].tendangnhap;
                }
                ViewBag.er = "thanh cong";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.er = "sai mat khau hoac ten dang nhap";
                return RedirectToAction("DangNhap");
            }
        }
        public ActionResult DangXuat()
        {
            Session.Remove("giohang");
            Session.Remove("role");
            Session.Remove("tendangnhap");
            Session.Remove("giohang");
            return View();
        }


        //xóa tài khoản cá nhân + xem chi tiết tài khoản cá nhân
        public ActionResult QuanLyTaiKhoan()//khách hàng , xóa tài khoản
        {
            if (Session["role"] == null)
            {
                Response.Redirect("https://localhost:44321/TaiKhoan/DangNhap");
            }
            string username = Convert.ToString(Session["tendangnhap"]);
            var chitiet = DB.taikhoans.Where(s => s.tendangnhap == username).ToList().FirstOrDefault();
            var splq = DB.taikhoans.Where(s => s.tendangnhap == chitiet.tendangnhap).ToList().Take(1);
            ViewBag.sqlq = splq;
            return View(chitiet);
        }
        [HttpPost]
        public ActionResult QuanLyTaiKhoan(FormCollection collection) //xoa
        {
            string user = Convert.ToString(Session["tendangnhap"]);
            var x = DB.taikhoans.Where(s => s.tendangnhap == user).FirstOrDefault();
            DB.taikhoans.Remove(x);
            DB.SaveChanges();
            Session.Remove("tendangnhap");
            return RedirectToAction("DangXuat", "TaiKhoan");
        }



        //sửa tài khoản cá nhân
        public ActionResult SuaTK()
        {
            if (Session["role"] == null)
            {
                Response.Redirect("https://localhost:44321/TaiKhoan/DangNhap");
            }
            string user = Convert.ToString(Session["tendangnhap"]);
            var chitiet = DB.taikhoans.Where(s => s.tendangnhap == user).ToList().FirstOrDefault();
            var splq = DB.taikhoans.Where(s => s.tendangnhap == chitiet.tendangnhap).ToList().Take(1);
            ViewBag.sqlq = splq;
            return View(chitiet);
        }
        [HttpPost]
        public ActionResult SuaTK(taikhoan Sanpham)
        {
            try
            {
                DB.Entry(Sanpham).State = EntityState.Modified;
                DB.SaveChanges();
                return RedirectToAction("QuanLyTaiKhoan", "TaiKhoan");
            }
            catch
            {
                return View();
            }
        }




        //Chức năng admin
        //xem danh sách tai khoản (danh cho admin)
        public ActionResult DanhSachTK()
        {
            if (Convert.ToInt32(Session["role"]) !=1 )
            {
                Response.Redirect("https://localhost:44321/");
            }
            ViewBag.Title = "DanhSachTK";
            var sp = (from p in DB.taikhoans select p).ToList();
            return View(sp);
        }

        //xoa tai khoan + xem chi tiet (danh cho admin)
        public ActionResult Chitietkhachhang(string tendangnhap)
        {
            if (Convert.ToInt32(Session["role"]) == 1)
            {

                var kt = DB.taikhoans.Where(s => s.tendangnhap == tendangnhap).Count();//kiem tra taikhoan co ton tai trong database hay k
                if (Convert.ToInt32(kt) > 0)
                {
                    var chitiet = DB.taikhoans.Where(s => s.tendangnhap == tendangnhap).ToList().FirstOrDefault();
                    var splq = DB.taikhoans.Where(s => s.tendangnhap == chitiet.tendangnhap).ToList().Take(1);
                    ViewBag.sqlq = splq;
                    Session["ten"] = tendangnhap;
                    return View(chitiet);
                }
                else
                {
                    return View();
                }
                
            }

            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult Chitietkhachhang(FormCollection collection) //xoa
        {
            string user = Convert.ToString(Session["ten"]);
            var x = DB.taikhoans.Where(s => s.tendangnhap == user).FirstOrDefault();
            DB.taikhoans.Remove(x);
            DB.SaveChanges();
            return RedirectToAction("DanhSachTK", "TaiKhoan");
        }

        public ActionResult CapNhatTaiKhoan(string tendangnhap)
        {
            if (Convert.ToInt32(Session["role"]) == 1)
            {
                var chitiet = DB.taikhoans.Where(s => s.tendangnhap == tendangnhap).ToList().FirstOrDefault();
                var splq = DB.taikhoans.Where(s => s.tendangnhap == chitiet.tendangnhap).ToList().Take(1);
                ViewBag.sqlq = splq;
                return View(chitiet);
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult CapNhatTaiKhoan(taikhoan TaiKhoan)
        {
            try
            {
                DB.Entry(TaiKhoan).State = EntityState.Modified;
                DB.SaveChanges();
                return RedirectToAction("DanhSachTK", "TaiKhoan");
            }
            catch
            {
                return View();
            }
        }
    }
}