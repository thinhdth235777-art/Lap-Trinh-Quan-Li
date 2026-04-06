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
    public class SanPhamBLL
    {
        SanPhamDAL dal = new SanPhamDAL();

        public DataTable LayDanhSachSP()
        {
            return dal.LayTatCaSanPham();
        }
        public bool XoaSanPham(int maSP)
        {
            // Gọi xuống DAL để thực hiện lệnh DELETE
            return dal.XoaSP(maSP);
        }
        public string KiemTraVaLuu(SanPhamDTO sp, string hanhDong)
        {
            // 1. Kiểm tra để trống
            if (string.IsNullOrWhiteSpace(sp.TenSanPham))
                return "Tên sản phẩm không được để trống!";

            // 2. Kiểm tra giá bán (> 0)
            if (sp.GiaBan <= 0)
                return "Giá bán phải là số dương (> 0)!";

            // 3. Kiểm tra số lượng (>= 0)
            if (sp.SoLuongTon < 0)
                return "Số lượng tồn không được âm!";

            // --- LOGIC THỰC THI THEO HÀNH ĐỘNG ---
            if (hanhDong == "THEM")
            {
                if (dal.ThemSP(sp)) return "Thành công";
            }
            else if (hanhDong == "SUA")
            {
                // Gọi hàm SuaSP dưới DAL (nhớ truyền đối tượng sp chứa ID vào)
                if (dal.SuaSP(sp)) return "Thành công";
            }

            return "Thất bại";
        }
        public DataTable TimKiem(string ten)
        {
            return dal.TimKiemSP(ten);
        }
    }
}
