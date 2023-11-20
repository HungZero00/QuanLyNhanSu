using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace QuanLyNhanSu.Models
{
    public class ChucVuValidation
    {
        [Required(ErrorMessage = "Nhập mã chức vụ")]
        [RegularExpression(@"[A-Za-z0-9]*$", ErrorMessage = "Mã chứa kí tự đặc biệt")]
        [MaxLength(30, ErrorMessage = "vượt quá số kí tự 30")]
        public string MaChucVuNV { get; set; }
        [Required(ErrorMessage = "Nhập tên chức vụ")]
        [MaxLength(50, ErrorMessage = "vượt quá số kí tự 50")]
        public string TenChucVu { get; set; }
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Chỉ được nhập số")]
        public double? HSPC { get; set; }
    }
}