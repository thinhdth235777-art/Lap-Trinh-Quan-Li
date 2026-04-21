using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDCAM.Data_Transfer_Object
{
    internal class ChiTietDonHangDTO
    {
        public int MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien => SoLuong * DonGia;
    }
}
