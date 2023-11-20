using DocumentFormat.OpenXml.Office2010.Excel;
using QuanLyNhanSu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyNhanSu.Areas.admin.Controllers
{
    public class HeSoLuongController : Controller
    {
        QuanLyNhanSuEntities1 db = new QuanLyNhanSuEntities1();
        // GET: admin/HeSoLuong
        public ActionResult Index()
        {
            var lb1 = db.LuongBac1.ToList();
            return View(lb1);
        }
        [HttpGet]
        public ActionResult ThemHSL()
        {
            var lb1 = db.LuongBac1.ToList();

            return View(new LuongBac1());
        }
        [HttpPost]
        public ActionResult ThemHSL(LuongBac1 lb1)
        {
            //var ct = db.ChiTietLuongs.Where(n => n.MaNhanVien == kt.MaNhanVien).FirstOrDefault();

            LuongBac1 ad = new LuongBac1();
            ad.id= lb1.id;
            ad.HeSo= lb1.HeSo;

            db.LuongBac1.Add(ad);
            db.SaveChanges();
            return Redirect("/admin/HeSoLuong");
        }
        [HttpGet]
        public ActionResult SuaHSL(int id)
        {
            var luongBac1 = db.LuongBac1.Find(id);
            if (luongBac1 == null)
            {
                return HttpNotFound();
            }

            var lb1 = new LuongBac1
            {
                id = luongBac1.id,
                HeSo = luongBac1.HeSo
            };

            return View(lb1);
        }

        [HttpPost]
        public ActionResult SuaHSL(LuongBac1 lb1)
        {
            if (ModelState.IsValid)
            {
                var luongBac1 = db.LuongBac1.Find(lb1.id);
                if (luongBac1 != null)
                {
                    luongBac1.HeSo = lb1.HeSo;
                    db.SaveChanges();
                    return Redirect("/admin/HeSoLuong");
                }
                else
                {
                    return HttpNotFound();
                }
            }

            return View(lb1);
        }



        public ActionResult XoaHSL(int id)
        {
            var lb1 = db.LuongBac1.Find(id);
            if (lb1!= null)
            {
                db.LuongBac1.Remove(lb1);
                db.SaveChanges();
            }

            return Redirect("/admin/HeSoLuong");
        }// end
    }
}