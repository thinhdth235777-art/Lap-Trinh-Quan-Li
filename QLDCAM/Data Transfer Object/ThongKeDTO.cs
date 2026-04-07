using System;

namespace QLDCAM.Data_Transfer_Object
{
    internal class ThongKeDTO
    {
        // For monthly revenue rows
        public int Thang { get; set; }
        public decimal TongDoanhThu { get; set; }

        // For top products
        public int MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public int TongSoLuong { get; set; }
        public int SoLuongTon { get; set; }

        // For top customers
        public int MaKhachHang { get; set; }
        public string HoTen { get; set; }
        public decimal TongMua { get; set; }
    }
}