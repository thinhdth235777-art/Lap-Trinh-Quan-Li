using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDCAM.Data_Transfer_Object
{
    public class ChiTietPhieuNhapDTO
    {
        public int MaPhieuNhap { get; set; }
        public int MaSanPham { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGiaNhap { get; set; }
    }
}
