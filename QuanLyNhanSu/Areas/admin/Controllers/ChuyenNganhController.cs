using QuanLyNhanSu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyNhanSu.Areas.admin.Controllers
{
    public class ChuyenNganhController : Controller
    {
        // GET: admin/ChuyenNganh
        QuanLyNhanSuEntities1 db = new QuanLyNhanSuEntities1();
        public ActionResult Index()
        {
            var list = db.ChuyenNganhs.ToList();
            return View(list);
        }
        [HttpGet]
        public ActionResult suaChuyenNganh(String id)
        {
            var cv = db.ChuyenNganhs.Where(n => n.MaChuyenNganh == id).FirstOrDefault();
            if (cv != null)
            {
                ChuyenNganhValidation tmp = new ChuyenNganhValidation();
                tmp.MaChuyenNganh = cv.MaChuyenNganh;
                tmp.TenChuyenNganh = cv.TenChuyenNganh;
                return View(tmp);
            }
            else
            {
                return Redirect("/");
            }
        }

        [HttpPost]
        public ActionResult suaChuyenNganh(ChuyenNganh ChuyenNganh)
        {

            if (ModelState.IsValid)
            {

                var tmp = db.ChuyenNganhs.Where(n => n.MaChuyenNganh == ChuyenNganh.MaChuyenNganh).FirstOrDefault();
                if (tmp != null)
                {
                    tmp.MaChuyenNganh = ChuyenNganh.MaChuyenNganh;
                    tmp.TenChuyenNganh = ChuyenNganh.TenChuyenNganh;
                    db.SaveChanges();
                    return Redirect("/admin/ChuyenNganh");
                }
            }
            return View(ChuyenNganh);

        }
        [HttpGet]
        public ActionResult ThemChuyenNganh()
        {
            return View(new ChuyenNganhValidation());
        }
        [HttpPost]
        public ActionResult ThemChuyenNganh(ChuyenNganhValidation nv)
        {
            if (ModelState.IsValid)
            {
                var checknv = db.ChuyenNganhs.Any(x => x.MaChuyenNganh == nv.MaChuyenNganh);

                if (checknv == false)
                {
                    ChuyenNganh add = new ChuyenNganh();
                    add.MaChuyenNganh = nv.MaChuyenNganh;
                    add.TenChuyenNganh = nv.TenChuyenNganh;
                    db.ChuyenNganhs.Add(add);
                    db.SaveChanges();
                    return Redirect("/admin/ChuyenNganh");
                }
                else
                {
                    ViewBag.err = "Mã chuyên ngành đã tồn tại ";
                    return View(nv);
                }
            }
            else
            {
                return View(nv);
            }
        }//end them
        public ActionResult XoaChuyenNganh(String id)
        {
            var cv = db.ChuyenNganhs.Where(n => n.MaChuyenNganh == id).FirstOrDefault();
            var nv = db.NhanViens.Where(n => n.MaChuyenNganh == id).ToList();
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
                db.ChuyenNganhs.Remove(cv);
                db.SaveChanges();
            }

            return Redirect("/admin/ChuyenNganh");
        }// end
    }
}