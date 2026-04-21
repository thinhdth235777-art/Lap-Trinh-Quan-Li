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

        public DataTable LayDS() => dal.LayDanhSachHoaDon();

        public DataTable LayCT(int maHD) => dal.LayChiTietHoaDon(maHD);
        public string LuuHoaDon(DonHangDTO dh, string hanhDong)
        {
            if (dh.MaKhachHang <= 0) return "Chưa chọn khách hàng!";

            if (hanhDong == "THEM")
            {
                return dal.ThemDH(dh) ? "Thành công" : "Thất bại";
            }
            else
            {
                return dal.SuaDH(dh) ? "Thành công" : "Thất bại";
            }
        }

        public bool XoaHoaDon(int maDH)
        {
            return dal.XoaDH(maDH);
        }
    }
}
