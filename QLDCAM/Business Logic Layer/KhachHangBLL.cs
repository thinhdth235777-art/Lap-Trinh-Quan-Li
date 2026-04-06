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
    internal class KhachHangBLL
    {
        KhachHangDAL dal = new KhachHangDAL();
        // Lấy danh sách
        public DataTable LayDanhSachKH()
        {
            return dal.LayTatCaKhachHang();
        }

        // Xóa
        public bool XoaKhachHang(int maKH)
        {
            return dal.XoaKH(maKH);
        }

        // Thêm + Sửa
        public string KiemTraVaLuu(KhachHangDTO kh, string hanhDong)
        {
            // 1. Check rỗng
            if (string.IsNullOrWhiteSpace(kh.HoTen))
                return "Họ tên không được để trống!";

            if (string.IsNullOrWhiteSpace(kh.SoDienThoai))
                return "SĐT không được để trống!";

            // 2. Check SĐT
            if (!kh.SoDienThoai.All(char.IsDigit) ||
                kh.SoDienThoai.Length < 10 || kh.SoDienThoai.Length > 11)
                return "SĐT không hợp lệ!";

            // 3. Check trùng SĐT (chỉ khi thêm)
            if (hanhDong == "THEM")
            {
                if (dal.KiemTraTrungSDT(kh.SoDienThoai))
                    return "SĐT đã tồn tại!";
            }

            // 4. Thực hiện
            if (hanhDong == "THEM")
            {
                if (dal.ThemKH(kh)) return "Thành công";
            }
            else if (hanhDong == "SUA")
            {
                if (dal.SuaKH(kh)) return "Thành công";
            }

            return "Thất bại";
        }

        // Tìm kiếm
        public DataTable TimKiem(string ten)
        {
            return dal.TimKiemKH(ten);
        }
    }
}
