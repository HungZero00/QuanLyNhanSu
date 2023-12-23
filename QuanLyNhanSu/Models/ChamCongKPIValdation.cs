using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyNhanSu.Models
{
    public class ChamCongKPIValdation
    {
        public int id { get; set; }
        public string MaNhanVien { get; set; }
        public string HoTen { get; set; }
        public string MaPhongBan { get; set; }
        public string MaChucVuNV { get; set; }
        public Nullable<System.DateTime> ThangChamCong { get; set; }
        public Nullable<int> SoLuongCVHoanThanh { get; set; }
        public string GhiChu { get; set; }
        public Nullable<int> DiemSo { get; set; }

    }
}