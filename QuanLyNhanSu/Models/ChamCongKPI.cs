//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLyNhanSu.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ChamCongKPI
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
    
        public virtual ChucVuNhanVien ChucVuNhanVien { get; set; }
        public virtual NhanVien NhanVien { get; set; }
        public virtual PhongBan PhongBan { get; set; }
    }
}