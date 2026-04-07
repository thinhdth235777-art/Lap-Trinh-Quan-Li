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
        public DateTime NgayLap { get; set; }
        public int MaKH { get; set; }
        public int MaNV { get; set; }
        public decimal TongTien { get; set; }
        public decimal PhiShip { get; set; }
    }
}
