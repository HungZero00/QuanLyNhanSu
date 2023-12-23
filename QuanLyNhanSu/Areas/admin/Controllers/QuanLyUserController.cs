using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyNhanSu.Models;
using System.Web.Security;
//using cExcel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Entity.Migrations;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace QuanLyNhanSu.Areas.admin.Controllers
{
    public class QuanLyUserController : AuthorController
    {
        QuanLyNhanSuEntities1 db = new QuanLyNhanSuEntities1();
        //
        // GET: /admin/QuanLyUser/
        public ActionResult Index()
        {
            var user = db.NhanViens.Where(x => x.MaNhanVien != "admin" && x.TrangThaiID == 2).ToList();
            return View(user);
        }


        public ActionResult XoaUser(String id)
        {

            var a = db.NhanViens.Where(x => x.MaNhanVien == id).SingleOrDefault();
            var hd = db.HopDongs.Where(x => x.MaHopDong == id).SingleOrDefault();
            var luong = db.Luongs.Where(x => x.MaNhanVien == id).SingleOrDefault();
            var ctLuong = db.ChiTietLuongs.Where(x => x.MaNhanVien == id).ToList();
            foreach (var item in ctLuong)
            {
                db.ChiTietLuongs.Remove(item);
            }

            db.Luongs.Remove(luong);
            db.NhanViens.Remove(a);
            db.HopDongs.Remove(hd);

            db.SaveChanges();
            return Redirect("~/admin/QuanLyUser");
        }
        [HttpGet]
        public ActionResult UpdateUser(String id)
        {
            var user = db.NhanViens.Where(n => n.MaNhanVien == id).FirstOrDefault();
            UserValidate userVal = new UserValidate();

            userVal.MaNhanVien = user.MaNhanVien;
            userVal.HoTen = user.HoTen;
            userVal.MatKhau = user.MatKhau;
            userVal.GioiTinh = user.GioiTinh;

            userVal.MaChucVuNV = user.MaChucVuNV;
            userVal.QueQuan = user.QueQuan;
            userVal.HinhAnh = user.HinhAnh;
            userVal.DanToc = user.DanToc;
            userVal.sdt_NhanVien = user.SDT;
            userVal.MaHopDong = user.MaHopDong;

            userVal.NgaySinh = user.NgaySinh;
            userVal.TrangThaiID = (int)user.TrangThaiID;
            userVal.MaChuyenNganh = user.MaChuyenNganh;
            userVal.MaTrinhDoHocVan = user.MaTrinhDoHocVan;
            userVal.MaPhongBan = user.MaPhongBan;

            userVal.CMND = user.CMND;
            userVal.XacNhanMatKhau = user.MatKhau;

            return View(userVal);
            //  return View(user);
        }
        [HttpPost]
        public ActionResult UpdateUser(UserValidate upUser)
        {
            upUser.XacNhanMatKhau = upUser.MatKhau;
            var us = db.NhanViens.Where(n => n.MaNhanVien == upUser.MaNhanVien).FirstOrDefault();

            if (ModelState.IsValid)
            {
                //var us = db.NhanViens.Where(n => n.MaNhanVien == upUser.MaNhanVien).FirstOrDefault();
                if (us != null)
                {

                    CapNhatTrinhDoHocVan capNhat = new CapNhatTrinhDoHocVan();
                    capNhat.MaNhanVien = upUser.MaNhanVien;
                    capNhat.NgayCapNhat = DateTime.Now.Date;
                    capNhat.MaTrinhDoTruoc = us.MaTrinhDoHocVan;
                    capNhat.MaTrinhDoCapNhat = upUser.MaTrinhDoHocVan;

                    us.MaNhanVien = upUser.MaNhanVien;
                    us.HoTen = upUser.HoTen;
                    us.MatKhau = upUser.MatKhau;
                    us.GioiTinh = upUser.GioiTinh;

                    us.MaChucVuNV = upUser.MaChucVuNV;
                    us.QueQuan = upUser.QueQuan;
                    us.HinhAnh = upUser.HinhAnh;
                    us.DanToc = upUser.DanToc;
                    us.SDT = upUser.sdt_NhanVien;
                    us.MaHopDong = upUser.MaHopDong;

                    us.NgaySinh = upUser.NgaySinh;
                    us.TrangThaiID = upUser.TrangThaiID;
                    us.MaChuyenNganh = upUser.MaChuyenNganh;
                    us.MaTrinhDoHocVan = upUser.MaTrinhDoHocVan;
                    us.MaPhongBan = upUser.MaPhongBan;
                    us.CMND = upUser.CMND;

                    var trinhdo = db.TrinhDoHocVans.Where(n => n.MaTrinhDoHocVan.Equals(us.MaTrinhDoHocVan)).FirstOrDefault();

                    var luong = db.Luongs.Where(n => n.MaNhanVien.Equals(us.MaNhanVien)).FirstOrDefault();

                    if (trinhdo.HeSoBac != null)
                    {
                        luong.HeSoLuong = luong.HeSoLuong < (double)trinhdo.HeSoBac ? (double)trinhdo.HeSoBac : luong.HeSoLuong;
                    }
                    else
                    {
                        luong.HeSoLuong = 1;
                    }

                    db.CapNhatTrinhDoHocVans.AddOrUpdate(capNhat);

                    db.SaveChanges();
                    return Redirect("/admin/QuanLyUser");

                }
            }
            return View(upUser);

        }//end update

        [HttpGet]

        public ActionResult ThemUser()
        {
            var chucvu = db.ChucVuNhanViens.ToList();
            var phongban = db.PhongBans.ToList();
            var hopdong = db.HopDongs.ToList();
            var chuyennganh = db.ChuyenNganhs.ToList();
            var trinhdo = db.TrinhDoHocVans.ToList();
            List<ChucVuNhanVien> list = chucvu;

            return View(new UserValidate());
        }


        [HttpPost]
        public ActionResult ThemUser(UserValidate nv)
        {
            nv.XacNhanMatKhau = nv.MatKhau;
            Random random = new Random();
            string newCode = "MNV" + random.Next(100000, 999999).ToString();
            nv.MaNhanVien = newCode;
            ViewBag.MaNV = newCode;

            if (ModelState.IsValid)
            {
                ViewBag.err = String.Empty;
                var checkMaNhanVien = db.NhanViens.Any(x => x.MaNhanVien == nv.MaNhanVien);

                if (checkMaNhanVien)
                {
                    ViewBag.err = "tài khoản đã tồn tại";
                    //ModelState.AddModelError("MaNhanVien", "Mã tài khoản đã tồn tại");
                    return View(nv);
                }
                else
                {
                    Luong luong = new Luong();
                    HopDong hd = new HopDong();
                    NhanVien nvAdd = new NhanVien();
                    nvAdd.MaNhanVien = nv.MaNhanVien;
                    nvAdd.MatKhau = nv.MatKhau;
                    nvAdd.HoTen = nv.HoTen;
                    nvAdd.NgaySinh = nv.NgaySinh;
                    nvAdd.QueQuan = nv.QueQuan;
                    nvAdd.GioiTinh = nv.GioiTinh;
                    nvAdd.DanToc = nv.DanToc;
                    nvAdd.MaChucVuNV = nv.MaChucVuNV;
                    nvAdd.MaPhongBan = nv.MaPhongBan;
                    nvAdd.MaChuyenNganh = nv.MaChuyenNganh;
                    nvAdd.MaTrinhDoHocVan = nv.MaTrinhDoHocVan;
                    nvAdd.MaHopDong = nv.MaNhanVien;
                    nvAdd.TrangThaiID = 2;
                    nvAdd.HinhAnh = "no-photo.jfif";

                    //add hop dong
                    hd.MaHopDong = nv.MaNhanVien;
                    hd.NgayBatDau = DateTime.Now.Date;

                    //tao bang luong
                    luong.MaNhanVien = nv.MaNhanVien;
                    luong.LuongToiThieu = 1150000;
                    luong.BHXH = 8;
                    luong.BHYT = 1.5;
                    luong.BHTN = 1;
                    var trinhdo = db.TrinhDoHocVans.Where(n => n.MaTrinhDoHocVan.Equals(nv.MaTrinhDoHocVan)).FirstOrDefault();
                    var chucvu = db.ChucVuNhanViens.Where(n => n.MaChucVuNV.Equals(nv.MaChucVuNV)).SingleOrDefault();

                    if (trinhdo.MaTrinhDoHocVan.Equals(nv.MaTrinhDoHocVan))
                    {
                        luong.HeSoLuong = (double)trinhdo.HeSoBac;
                    }


                    if (chucvu.MaChucVuNV.Equals(nv.MaChucVuNV))
                    {
                        if (chucvu.HSPC != null)
                        {
                            luong.PhuCap = (double)chucvu.HSPC;
                        }
                        else
                        { luong.PhuCap = 0; }
                    }

                    // tmp.Image = "~/Content/images/icon.jpg";
                    db.NhanViens.Add(nvAdd);
                    db.HopDongs.Add(hd);

                    db.Luongs.Add(luong);
                    // @ViewBag.add = "Đăng ký thành công";
                    db.SaveChanges();
                    //xác thực tài khoản trong ứng dụng
                    FormsAuthentication.SetAuthCookie(nvAdd.MaNhanVien, false);
                    //trả về trang quản lý

                    return Redirect("/admin/QuanLyUser");
                }
            }
            else
            {

                return View(nv);
            }
        }//end add nhan vien
        public ActionResult HopDong()
        {
            var hd = db.HopDongs.ToList();

            return View(hd);
        }

        [HttpGet]
        public ActionResult SuaHopDong(String id)
        {
            var hd = db.HopDongs.Where(n => n.MaHopDong == id).FirstOrDefault();
            HopDong hopdong = new HopDong();

            hopdong.MaHopDong = hd.MaHopDong;
            hopdong.LoaiHopDong = hd.LoaiHopDong;
            hopdong.NgayBatDau = hd.NgayBatDau;
            hopdong.NgayKetThuc = hd.NgayKetThuc;
            hopdong.NoiDung = hd.NoiDung;

            return View(hopdong);
            //  return View(hopdong);
        }
        [HttpPost]
        public ActionResult SuaHopDong(HopDong uphd)
        {
            var hd = db.HopDongs.Where(n => n.MaHopDong == uphd.MaHopDong).FirstOrDefault();

            if (ModelState.IsValid)
            {
                //var us = db.NhanViens.Where(n => n.MaNhanVien == upUser.MaNhanVien).FirstOrDefault();
                if (hd != null)
                {
                    hd.MaHopDong = uphd.MaHopDong;
                    hd.LoaiHopDong = uphd.LoaiHopDong;
                    hd.NgayBatDau = uphd.NgayBatDau;
                    hd.NgayKetThuc = uphd.NgayKetThuc;
                    hd.NoiDung = uphd.NoiDung;

                    db.HopDongs.AddOrUpdate(uphd);
                    db.SaveChanges();
                    return Redirect("/admin/QuanLyUser/HopDong");

                }
            }
            return View(uphd);

        }//end update
        public ActionResult QuaTrinhCongTac(String id)
        {
            var ds = db.LichSuChuyenNhanViens.Where(n => n.MaNhanVien == id).ToList();
            return View(ds);
        }

        public ActionResult XuatFileExel()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var ds = db.NhanViens.Where(n => n.MaNhanVien != "admin" && n.MaHopDong != null).ToList();
            var phong = db.PhongBans.ToList();

            using (ExcelPackage pck = new ExcelPackage())
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Sheet1");

                ws.Cells["A1"].Value = "Họ tên";
                ws.Cells["B1"].Value = "Phòng ban";
                ws.Cells["C1"].Value = "Chức vụ";
                ws.Cells["D1"].Value = "Học vấn";
                ws.Cells["E1"].Value = "Chuyên ngành";

                // tiêu đề in đậm
                ws.Cells["A1:E1"].Style.Font.Bold = true;

                /*  // Tạo khung tiêu đề
                  ws.Cells["A1:E1"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                  ws.Cells["A1:E1"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                  ws.Cells["A1:E1"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                  ws.Cells["A1:E1"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;*/


                int rowStart = 2;
                foreach (var item in ds)
                {
                    ws.Cells[string.Format("A{0}", rowStart)].Value = item.HoTen;
                    ws.Cells[string.Format("B{0}", rowStart)].Value = item.PhongBan.TenPhongBan;
                    ws.Cells[string.Format("C{0}", rowStart)].Value = item.ChucVuNhanVien.TenChucVu;
                    ws.Cells[string.Format("D{0}", rowStart)].Value = item.TrinhDoHocVan.TenTrinhDo;
                    ws.Cells[string.Format("E{0}", rowStart)].Value = item.ChuyenNganh.TenChuyenNganh;

                    rowStart++;
                }
                // Tạo khung cho tất cả các ô dữ liệu
                var cellRange = ws.Cells[1, 1, rowStart - 1, 5];
                cellRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                cellRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                cellRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                cellRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                // Auto mở rộng cột cho phù hợp với văn bản
                ws.Cells[ws.Dimension.Address].AutoFitColumns();

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment; filename=Nhan_vien.xlsx");
                Response.BinaryWrite(pck.GetAsByteArray());
                Response.End();
            }

            return Redirect("/admin/QuanLyUser");
        }
        public ActionResult QuaTrinhHoc(String id)
        {
            var ht = db.CapNhatTrinhDoHocVans.Where(n => n.MaNhanVien == id).ToList();
            return View(ht);
        }

    }   //end lass
}