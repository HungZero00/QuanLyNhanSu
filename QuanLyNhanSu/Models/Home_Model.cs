using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyNhanSu.Models
{
    public class Home_Model
    {
        public List<KhenThuong> list_khenthuong { get; set; }
        public List<KyLuat> list_kyluat { get; set; }
        public List<ThoiViec> list_thoiviec { get; set; }
        public List<LuongBac1> luongB1 { get; set; }
        public List<LuongBac2> luongB2 { get; set; }
        public List<LuongBac3> luongB3 { get; set; }

    }
    
   
}