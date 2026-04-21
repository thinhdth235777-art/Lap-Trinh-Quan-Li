using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDCAM.Data_Transfer_Object
{
    internal class DonHangDTO
    {
        public int MaDonHang { get; set; }
        public int MaNhanVien { get; set; }
        public int MaKhachHang { get; set; }
        public DateTime NgayDatHang { get; set; }
        public decimal TongTien { get; set; }
        public string TenNhanVien { get; set; }
        public string TenKhachHang { get; set; }
    }
}
