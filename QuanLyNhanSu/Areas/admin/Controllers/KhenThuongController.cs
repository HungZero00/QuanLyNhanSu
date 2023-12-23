using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyNhanSu.Models;

namespace QuanLyNhanSu.Areas.admin.Controllers
{
    public class KhenThuongController : Controller
    {
        QuanLyNhanSuEntities1 db = new QuanLyNhanSuEntities1();

        //
        // GET: /admin/KhenThuong/
        public ActionResult Index()
        {
            Home_Model home_Model = new Home_Model();

            home_Model.list_khenthuong = db.KhenThuongs.ToList();
            home_Model.list_kyluat = db.KyLuats.ToList();
            home_Model.list_thoiviec = db.ThoiViecs.ToList();
            //   var tupleModel = new Tuple<list_khenthuong, ListOtherModel1, ListOtherModel2>(list_khenthuong, listOtherModel1, listOtherModel2);
            return View(home_Model);
        }
        [HttpGet]
        public ActionResult khen()
        {
            var nv = db.NhanViens.ToList();
             
            return View(new KhenThuong());
        }
        [HttpGet]
        public ActionResult kyluat()
        {
            var nv = db.NhanViens.ToList();

            return View(new KyLuat());
        }
        [HttpGet]
        public ActionResult thoiviec()
        {
            var nv = db.NhanViens.ToList();

            return View(new ThoiViec());
        }
        [HttpPost]
        public ActionResult khen(KhenThuong kt)
        {
            //var ct = db.ChiTietLuongs.Where(n => n.MaNhanVien == kt.MaNhanVien).FirstOrDefault();

            KhenThuong ad = new KhenThuong();
            ad.MaNhanVien = kt.MaNhanVien;
            ad.ThangThuong = kt.ThangThuong;
            ad.TienThuong = kt.TienThuong;
            ad.LyDo = kt.LyDo;

            db.KhenThuongs.Add(ad);
            db.SaveChanges();
            return Redirect("/admin/KhenThuong");
        }
        [HttpPost]
        public ActionResult kyluat(KyLuat kl)
        {
            //var ct = db.ChiTietLuongs.Where(n => n.MaNhanVien == kt.MaNhanVien).FirstOrDefault();

            KyLuat ad = new KyLuat();
            ad.MaNhanVien = kl.MaNhanVien;
            ad.ThangKiLuat = kl.ThangKiLuat;
            ad.TienKyLuat = kl.TienKyLuat;
            ad.LyDo = kl.LyDo;

            db.KyLuats.Add(ad);
            db.SaveChanges();
            return Redirect("/admin/KhenThuong");
        }
        [HttpPost]
        public ActionResult thoiviec(ThoiViec tv)
        {

            var nv = db.NhanViens.Where(n => n.MaNhanVien == tv.MaNhanVien).ToList();
            NhanVien nv1 = db.NhanViens.Find(tv.MaNhanVien);
            TrangThai tt = db.TrangThais.FirstOrDefault(x => x.TrangThaiID == nv1.TrangThaiID);
            if (nv1.TrangThaiID == 2)
            {

                nv1.TrangThaiID = 1;
            }
            else 
            {
                nv1.TrangThaiID = 1;
            }

            ThoiViec ad = new ThoiViec();
            ad.MaNhanVien = tv.MaNhanVien;
            ad.NgayThoiViec = tv.NgayThoiViec;
            ad.Lydo = tv.Lydo;
            db.NhanViens.AddOrUpdate(nv1);
            db.ThoiViecs.Add(tv);
            db.SaveChanges();

            return Redirect("/admin/KhenThuong");

        }



    }
}