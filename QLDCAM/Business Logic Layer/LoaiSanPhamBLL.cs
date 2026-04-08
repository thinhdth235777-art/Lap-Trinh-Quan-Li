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
    public class LoaiSanPhamBLL
    {
        LoaiSanPhamDAL dal = new LoaiSanPhamDAL();

        // Lấy danh sách đổ lên DataGridView
        public DataTable LayDanhSachLoai()
        {
            return dal.GetLoaiSP();
        }

        // Kiểm tra logic rồi mới cho lưu
        public string KiemTraVaLuu(LoaiSanPhamDTO loai, string hanhDong)
        {
            // 1. Kiểm tra không được để trống tên loại
            if (string.IsNullOrWhiteSpace(loai.TenLoai))
                return "Tên loại sản phẩm không được để trống!";

            // 2. Thực thi
            if (hanhDong == "THEM")
            {
                if (dal.ThemLoai(loai)) return "Thành công";
            }
            else if (hanhDong == "SUA")
            {
                if (dal.SuaLoai(loai)) return "Thành công";
            }

            return "Thất bại";
        }

        public bool XoaLoai(int ma)
        {
            return dal.XoaLoai(ma);
        }
        public DataTable TimKiem(string tuKhoa)
        {
            return dal.TimKiem(tuKhoa);
        }
    }
}
