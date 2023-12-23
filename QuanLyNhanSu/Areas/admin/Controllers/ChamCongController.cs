using QuanLyNhanSu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyNhanSu.Areas.admin.Controllers
{
    public class ChamCongController : Controller
    {
        QuanLyNhanSuEntities1 db = new QuanLyNhanSuEntities1();
        public ActionResult Index()
        {
            var list = db.ChamCongKPIs.ToList();
            return View(list);
        }
        [HttpGet]
        public ActionResult suaChamCong(int id)
        {
            var cc = db.ChamCongKPIs.Where(n => n.id == id).FirstOrDefault();
            if (cc != null)
            {
                ChamCongKPI tmp = new ChamCongKPI();
                tmp.MaNhanVien = cc.MaNhanVien;
                tmp.HoTen = cc.HoTen;
                tmp.MaPhongBan = cc.MaPhongBan;
                tmp.ThangChamCong = cc.ThangChamCong;
                tmp.SoLuongCVHoanThanh = cc.SoLuongCVHoanThanh;
                tmp.GhiChu = cc.GhiChu;
                tmp.DiemSo = cc.DiemSo;
                tmp.MaChucVuNV = cc.MaChucVuNV;
                return View(tmp);
            }
            else
            {
                return Redirect("/admin/ChamCong");
            }//
        }

        [HttpPost]
        public ActionResult suaChamCong(ChamCongKPI cc)
        {

            if (ModelState.IsValid)
            {

                var tmp = db.ChamCongKPIs.Where(n => n.MaNhanVien == cc.MaNhanVien).FirstOrDefault();
                if (tmp != null)
                {
                    tmp.MaNhanVien = cc.MaNhanVien;
                    tmp.HoTen = cc.HoTen;
                    tmp.MaPhongBan = cc.MaPhongBan;
                    tmp.ThangChamCong = cc.ThangChamCong;
                    tmp.SoLuongCVHoanThanh = cc.SoLuongCVHoanThanh;
                    tmp.GhiChu = cc.GhiChu;
                    tmp.DiemSo = cc.DiemSo;
                    tmp.MaChucVuNV = cc.MaChucVuNV;
                    db.SaveChanges();
                    return Redirect("/admin/ChamCong");
                }
            }
            return View(cc);

        }
        [HttpGet]
        public ActionResult ThemChamCong()
        {
            return View(new ChamCongKPIValdation());
        }
        [HttpPost]
        public ActionResult ThemChamCong(ChamCongKPIValdation cc)
        {
            if (ModelState.IsValid)
            {
                var checknv = db.ChamCongKPIs.Any(x => x.MaNhanVien == cc.MaNhanVien);

                if (checknv == false)
                {
                    ChamCongKPI add = new ChamCongKPI();
                    add.MaNhanVien = cc.MaNhanVien;
                    add.HoTen = cc.HoTen;
                    add.MaPhongBan = cc.MaPhongBan;
                    add.MaChucVuNV = cc.MaChucVuNV;
                    add.ThangChamCong = cc.ThangChamCong;
                    add.SoLuongCVHoanThanh = cc.SoLuongCVHoanThanh;
                    add.GhiChu = cc.GhiChu;
                    add.DiemSo = cc.DiemSo;
                    db.ChamCongKPIs.Add(add);
                    db.SaveChanges();
                    return Redirect("/admin/ChamCong");
                }
                else
                {
                    ViewBag.err = "Mã chấm công đã tồn tại ";
                    return View(cc);
                }
            }
            else
            {
                return View(cc);
            }
        }//end them
        public ActionResult XoaChamCong(int id)
        {
            var cv = db.ChamCongKPIs.Where(n => n.id == id).FirstOrDefault();
        
            if (cv != null)
            {
                db.ChamCongKPIs.Remove(cv);
                db.SaveChanges();
            }

            return Redirect("/admin/ChamCong");
        }// end
    }
}