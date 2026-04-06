using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDCAM.Data_Transfer_Object
{
    public class SanPhamDTO
    {
        public int MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public int MaLoai { get; set; }
        public int MaThuongHieu { get; set; }
        public decimal GiaBan { get; set; }
        public int SoLuongTon { get; set; }
        public string MoTa { get; set; }
        public string HinhAnh { get; set; } 

        public string TenLoai { get; set; }
        public string TenThuongHieu { get; set; }
    }
}
