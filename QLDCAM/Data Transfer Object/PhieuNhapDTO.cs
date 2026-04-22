using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDCAM.Data_Transfer_Object
{
    public class PhieuNhapDTO
    {
        public int MaPhieuNhap { get; set; }
        public int MaNhanVien { get; set; }
        public int MaNhaCungCap { get; set; }
        public DateTime NgayNhap { get; set; }
        public decimal TongTien { get; set; }
    }
}
