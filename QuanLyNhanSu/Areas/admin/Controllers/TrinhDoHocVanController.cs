using QuanLyNhanSu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyNhanSu.Areas.admin.Controllers
{
    public class TrinhDoHocVanController : Controller
    {
        // GET: admin/TrinhDoHocVan
        QuanLyNhanSuEntities1 db = new QuanLyNhanSuEntities1();
        public ActionResult Index()
        {
            var list = db.TrinhDoHocVans.ToList();
            return View(list);
        }
        [HttpGet]
        public ActionResult suaHocVan(String id)
        {
            var cv = db.TrinhDoHocVans.Where(n => n.MaTrinhDoHocVan == id).FirstOrDefault();
            if (cv != null)
            {
                TrinhDoHocVanValidation tmp = new TrinhDoHocVanValidation();
                tmp.MaTrinhDoHocVan = cv.MaTrinhDoHocVan;
                tmp.TenTrinhDo = cv.TenTrinhDo;
                tmp.HeSoBac = cv.HeSoBac;
                return View(tmp);
            }
            else
            {
                return Redirect("/");
            }
        }

        [HttpPost]
        public ActionResult suaHocVan(TrinhDoHocVan HocVan)
        {

            if (ModelState.IsValid)
            {

                var tmp = db.TrinhDoHocVans.Where(n => n.MaTrinhDoHocVan == HocVan.MaTrinhDoHocVan).FirstOrDefault();
                if (tmp != null)
                {
                    tmp.MaTrinhDoHocVan = HocVan.MaTrinhDoHocVan;
                    tmp.TenTrinhDo = HocVan.TenTrinhDo;
                    tmp.HeSoBac = HocVan.HeSoBac;
                    db.SaveChanges();
                    return Redirect("/admin/TrinhDoHocVan");
                }
            }
            return View(HocVan);

        }
        [HttpGet]
        public ActionResult ThemHocVan()
        {
            return View(new TrinhDoHocVanValidation());
        }
        [HttpPost]
        public ActionResult ThemHocVan(TrinhDoHocVanValidation nv)
        {
            if (ModelState.IsValid)
            {
                var checknv = db.TrinhDoHocVans.Any(x => x.MaTrinhDoHocVan == nv.MaTrinhDoHocVan);

                if (checknv == false)
                {
                    TrinhDoHocVan add = new TrinhDoHocVan();
                    add.MaTrinhDoHocVan = nv.MaTrinhDoHocVan;
                    add.TenTrinhDo = nv.TenTrinhDo;
                    add.HeSoBac = nv.HeSoBac;
                    db.TrinhDoHocVans.Add(add);
                    db.SaveChanges();
                    return Redirect("/admin/TrinhDoHocVan");
                }
                else
                {
                    ViewBag.err = "Mã chức vụ đã tồn tại ";
                    return View(nv);
                }
            }
            else
            {
                return View(nv);
            }
        }//end them
        public ActionResult XoaHocVan(String id)
        {
            var cv = db.TrinhDoHocVans.Where(n => n.MaTrinhDoHocVan == id).FirstOrDefault();
            var nv = db.NhanViens.Where(n => n.MaTrinhDoHocVan == id).ToList();
            var hd = db.HopDongs.Where(n => n.MaHopDong == id).ToList();
            if (cv != null)
            {
                foreach (var item in hd)
                {
                    foreach (var i in nv)
                    {
                        db.NhanViens.Remove(i);
                        db.HopDongs.Remove(item);
                    }
                }
                db.TrinhDoHocVans.Remove(cv);
                db.SaveChanges();
            }

            return Redirect("/admin/TrinhDoHocVan");
        }// end
    }
}