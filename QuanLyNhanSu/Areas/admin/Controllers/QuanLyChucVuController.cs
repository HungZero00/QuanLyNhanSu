using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyNhanSu.Models;
using System.Data;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;

namespace QuanLyNhanSu.Areas.admin.Controllers
{
    public class QuanLyChucVuController : Controller
    {
        QuanLyNhanSuEntities1 db = new QuanLyNhanSuEntities1();
        public ActionResult Index()
        {
            var list = db.ChucVuNhanViens.ToList();
            return View(list);
        }
        [HttpGet]
        public ActionResult suaChucVu(String id)
        {
            var cv = db.ChucVuNhanViens.Where(n => n.MaChucVuNV == id).FirstOrDefault();
            if (cv != null)
            {
                ChucVuValidation tmp = new ChucVuValidation();
                tmp.MaChucVuNV = cv.MaChucVuNV;
                tmp.TenChucVu = cv.TenChucVu;
                tmp.HSPC = cv.HSPC;
                return View(tmp);
            }
            else
            {
                return Redirect("/");
            }
        }

        [HttpPost]
        public ActionResult suaChucVu(ChucVuNhanVien chucvu)
        {

            if (ModelState.IsValid)
            {

                var tmp = db.ChucVuNhanViens.Where(n => n.MaChucVuNV== chucvu.MaChucVuNV).FirstOrDefault();
                if (tmp != null)
                {
                    tmp.MaChucVuNV= chucvu.MaChucVuNV;
                    tmp.TenChucVu = chucvu.TenChucVu;
                    tmp.HSPC= chucvu.HSPC;
                    db.SaveChanges();
                    return Redirect("/admin/QuanLyChucVu");
                }
            }
            return View(chucvu);

        } 
        [HttpGet]
        public ActionResult ThemChucVu()
        {
            return View(new ChucVuValidation());
        }
        [HttpPost]
        public ActionResult ThemChucVu(ChucVuValidation nv)
        {
            if (ModelState.IsValid)
            {
                var checknv = db.ChucVuNhanViens.Any(x => x.MaChucVuNV == nv.MaChucVuNV);

                if (checknv == false)
                {
                    ChucVuNhanVien add = new ChucVuNhanVien();
                    add.MaChucVuNV = nv.MaChucVuNV;
                    add.TenChucVu = nv.TenChucVu;
                    add.HSPC = nv.HSPC;
                    db.ChucVuNhanViens.Add(add);
                    db.SaveChanges();
                    return Redirect("/admin/QuanLyChucVu");
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
        public ActionResult XoaChucVu(String id)
        {
            var cv = db.ChucVuNhanViens.Where(n => n.MaChucVuNV== id).FirstOrDefault();
            var nv = db.NhanViens.Where(n => n.MaChucVuNV == id).ToList();
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
                db.ChucVuNhanViens.Remove(cv);
                db.SaveChanges();
            }

            return Redirect("/admin/QuanLyChucVu");
        }// end
    }
}