using QLDCAM.Data_Access_Layer;
using QLDCAM.Data_Transfer_Object;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDCAM.Business_Logic_Layer
{
    public class ThuongHieuBLL
    {
        ThuongHieuDAL dal = new ThuongHieuDAL();

        public DataTable LayDanhSachTH()
        {
            return dal.GetThuongHieu();
        }

        public string KiemTraVaLuu(ThuongHieuDTO th, string hanhDong)
        {
            if (string.IsNullOrWhiteSpace(th.TenThuongHieu))
                return "Tên thương hiệu không được để trống!";

            if (hanhDong == "THEM")
            {
                if (dal.ThemTH(th)) return "Thành công";
            }
            else if (hanhDong == "SUA")
            {
                if (dal.SuaTH(th)) return "Thành công";
            }

            return "Thất bại";
        }

        public bool XoaTH(int ma)
        {
            return dal.XoaTH(ma);
        }

        public DataTable TimKiem(string tuKhoa)
        {
            return dal.TimKiem(tuKhoa);
        }
    }
}
