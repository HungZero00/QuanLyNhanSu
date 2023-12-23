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
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace QuanLyNhanSu.Areas.admin.Controllers
{
    public class QuanLyPhongBanController : AuthorController
    {
        QuanLyNhanSuEntities1 db = new QuanLyNhanSuEntities1();
        //
        // GET: /admin/QuanLyPhongBan/
        public ActionResult Index()
        {
            var phongban = db.PhongBans.ToList();
            return View(phongban);
        }
        [HttpGet]
        public ActionResult SuaPhongBan(String id)
        {
            var pb = db.PhongBans.Where(n => n.MaPhongBan == id).FirstOrDefault();
            if (pb != null)
            {
                PhongBanValidation tmp = new PhongBanValidation();
                tmp.MaPhongBan = pb.MaPhongBan;
                tmp.TenPhongBan = pb.TenPhongBan;
                tmp.sdt_PhongBan = pb.sdt_PhongBan;
                tmp.DiaChi = pb.DiaChi;
                return View(tmp);
            }
            else
            {
                return Redirect("/");
            }
        }
        [HttpPost]
        public ActionResult SuaPhongBan(PhongBanValidation pb)
        {
            if (ModelState.IsValid)
            {

                var tmp = db.PhongBans.Where(n => n.MaPhongBan == pb.MaPhongBan).FirstOrDefault();
                if (tmp != null)
                {
                    tmp.MaPhongBan = pb.MaPhongBan;
                    tmp.TenPhongBan = pb.TenPhongBan;
                    tmp.sdt_PhongBan = pb.sdt_PhongBan;
                    tmp.DiaChi = pb.DiaChi;
                    db.SaveChanges();
                    return Redirect("/admin/QuanLyPhongBan");
                }
            }
            return View(pb);
        }//end update
        [HttpGet]
        public ActionResult ThemPhongBan()
        {
            return View(new PhongBanValidation());
        }
        [HttpPost]
        public ActionResult ThemPhongBan(PhongBanValidation pb)
        {
            if (ModelState.IsValid)
            {
                var checkPB = db.PhongBans.Any(x => x.MaPhongBan == pb.MaPhongBan);

                if (checkPB == false)
                {
                    PhongBan add = new PhongBan();
                    add.MaPhongBan = pb.MaPhongBan;
                    add.TenPhongBan = pb.TenPhongBan;
                    add.DiaChi = pb.DiaChi;
                    add.sdt_PhongBan = pb.sdt_PhongBan;
                    db.PhongBans.Add(add);
                    db.SaveChanges();
                    return Redirect("/admin/QuanLyPhongBan");
                }
                else
                {
                    ViewBag.err = "mã phòng ban đã tồn tại ";
                    return View(pb);
                }
            }
            else
            {
                return View(pb);
            }
        }//end them

        public ActionResult DanhSachNhanVien(String id)
        {
            var name = db.PhongBans.Where(n => n.MaPhongBan == id).SingleOrDefault().TenPhongBan;
            ViewBag.ten = name.ToString();

            var list = db.NhanViens.Where(n => n.MaPhongBan == id).ToList();
            ViewBag.id = id;
            return View(list);
        }
        [HttpGet]
        public ActionResult ChuyenNhanVien(String id)
        {
            var nv = db.NhanViens.Where(n => n.MaNhanVien == id).FirstOrDefault();

            if (nv != null)
            {
                return View(nv);
            }
            else
            {
                return Redirect("/admin/QuanLyPhongBan");
            }
        }
        [HttpPost]
        public ActionResult ChuyenNhanVien(NhanVien nv, LichSuChuyenNhanVien fl)
        {
            var nvChuyen = db.NhanViens.Where(n => n.MaNhanVien == nv.MaNhanVien).FirstOrDefault();

            nvChuyen.MaNhanVien = nv.MaNhanVien;
            nvChuyen.HoTen = nv.HoTen;
            nvChuyen.MaChucVuNV = nv.MaChucVuNV;
            nvChuyen.MaPhongBan = fl.PhongBanDen;

            nvChuyen.MatKhau = nv.MatKhau;
            nvChuyen.GioiTinh = nv.GioiTinh;
            nvChuyen.QueQuan = nv.QueQuan;
            nvChuyen.HinhAnh = nv.HinhAnh;
            nvChuyen.DanToc = nv.DanToc;
            nvChuyen.SDT = nv.SDT;
            nvChuyen.MaHopDong = nv.MaHopDong;
            nvChuyen.NgaySinh = nv.NgaySinh;
            nvChuyen.TrangThai = nv.TrangThai;
            nvChuyen.MaChuyenNganh = nv.MaChuyenNganh;
            nvChuyen.MaTrinhDoHocVan = nv.MaTrinhDoHocVan;
            nvChuyen.CMND = nv.CMND;

            //add vao bang luan chuyen nhan vien
            LichSuChuyenNhanVien tableChuyen = new LichSuChuyenNhanVien();
            tableChuyen.MaNhanVien = nv.MaNhanVien;
            tableChuyen.NgayChuyen = DateTime.Now.Date;
            tableChuyen.PhongBanChuyen = nv.MaPhongBan; //

            tableChuyen.PhongBanDen = fl.PhongBanDen;
            tableChuyen.LyDoChuyen = fl.LyDoChuyen;
            //cap nhat lại phụ cấp chức vụ
            var luong = db.Luongs.Where(n => n.MaNhanVien.Equals(nv.MaNhanVien)).SingleOrDefault();
            var chucvu = db.ChucVuNhanViens.Where(n => n.MaChucVuNV.Equals(nv.MaChucVuNV)).SingleOrDefault();

            if (chucvu.HSPC != null)
            {
                luong.PhuCap = chucvu.HSPC;

            }
            else
            {
                luong.PhuCap = 0;
            }


            //add vao csdl quá trình công tác
            db.LichSuChuyenNhanViens.Add(tableChuyen);
            db.SaveChanges();
            return Redirect("/admin/QuanLyPhongBan");
        }


        public ActionResult XoaPhongBan(String id)
        {
            var pb = db.PhongBans.Where(n => n.MaPhongBan == id).FirstOrDefault();
            var hd = db.HopDongs.Where(n => n.MaHopDong == id).ToList();
            var nv = db.NhanViens.Where(n => n.MaNhanVien == id).ToList();
            // những nhân viên phòngban
            //   var nv = db.NhanViens.Where(n => n.MaPhongBan == id).ToList();

            //danh sách hợp đồng


            /*
             * mã phòng ban
             * mã hợp đồng = mã nhân viên
             */

            if (pb != null)
            {
               
                foreach (var item in hd)
                {
                    foreach (var i in nv)
                    {
                        db.NhanViens.Remove(i);
                        db.SaveChanges();

                    }
                    db.HopDongs.Remove(item);
                    db.SaveChanges();
                }

                db.PhongBans.Remove(pb);
                db.SaveChanges();
            }

            return Redirect("/admin/QuanLyPhongBan");
        }// end xoa phong ban

        [HttpGet]
        public ActionResult CapNhatPhuCap()
        {
            var pbcv = db.ChucVuNhanViens.ToList();
            return View(pbcv);
        }
        [HttpPost]
        public ActionResult CapNhatPhuCap(ChucVuNhanVien cv, String id, FormCollection f)
        {
            var pc = db.ChucVuNhanViens.Where(n => n.MaChucVuNV.Equals(id)).FirstOrDefault();
            //pc.MaChucVuNV = cv.MaChucVuNV;
            var x = f["HSPC"];
            pc.HSPC = x == null ? 0 : double.Parse(x.ToString());
            db.SaveChanges();

            return RedirectToAction("CapNhatPhuCap", "QuanLyPhongBan");
        }

        public ActionResult XuatFileExel(String id)
        {
            // Đặt môi trường cấp phép cho EPPlus
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            // Lấy danh sách nhân viên từ cơ sở dữ liệu
            var ds = db.NhanViens.Where(n => n.MaPhongBan == id).ToList();
            var phong = db.PhongBans.ToList();

            using (ExcelPackage pck = new ExcelPackage())
            {
                // Tạo một worksheet mới
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Sheet1");

                // Đặt tiêu đề cho các cột
                ws.Cells["A1"].Value = "Họ tên";
                ws.Cells["B1"].Value = "Phòng ban";
                ws.Cells["C1"].Value = "Chức vụ";
                ws.Cells["D1"].Value = "Học vấn";
                ws.Cells["E1"].Value = "Chuyên ngành";

                // Đặt tiêu đề in đậm
                ws.Cells["A1:E1"].Style.Font.Bold = true;

                int rowStart = 2;
                foreach (var item in ds)
                {
                    // Thêm dữ liệu cho mỗi hàng
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

                // Điều chỉnh độ rộng của các cột để phù hợp với nội dung
                ws.Cells[ws.Dimension.Address].AutoFitColumns();

                // Chuẩn bị response, đặt loại nội dung và tên file
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment; filename=Danh-sach.xlsx");

                // Gửi file Excel như một phần của response
                Response.BinaryWrite(pck.GetAsByteArray());
                Response.End();
            }

            // Chuyển hướng đến trang QuanLyPhongBan
            return Redirect("/admin/QuanLyPhongBan");
        }
    }//end classs
}