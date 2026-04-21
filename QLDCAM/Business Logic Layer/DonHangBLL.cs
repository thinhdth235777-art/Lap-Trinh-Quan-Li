using QLDCAM.Data_Access_Layer;
using QLDCAM.Data_Transfer_Object;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLDCAM.Business_Logic_Layer
{
    internal class DonHangBLL
    {
        DonHangDAL dal = new DonHangDAL();

        public DataTable LayDSKhachHang() => dal.LayBangDuLieu("SELECT * FROM KhachHang");
        public DataTable LayDSSanPham() => dal.LayBangDuLieu("SELECT * FROM SanPham");
        public DataTable LayDSHoaDon() => dal.LayDSHoaDon();
        public DataTable LayChiTiet(int maHD) => dal.LayChiTietTheoMa(maHD);

        public decimal TinhPhiShip(string tinhThanh)
        {
            // Logic: Khách ở An Giang phí = 0, tỉnh khác 30k
            return (tinhThanh != null && tinhThanh.Contains("An Giang")) ? 0 : 30000;
        }

        public bool LuuHoaDon(DonHangDTO hd, List<ChiTietDonHangDTO> ds) => dal.LuuHoaDonFull(hd, ds);
    }
}
