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
    internal class NhanVienBLL
    {
        NhanVienDAL dal = new NhanVienDAL();

        // Lấy danh sách
        public DataTable LayDanhSachNV()
        {
            return dal.LayTatCaNhanVien();
        }

        // Xóa
        public string XoaNhanVien(int maNV)
        {
            // Kiểm tra có đơn hàng không
            if (dal.KiemTraTonTaiDonHang(maNV))
                return "Nhân viên đã có đơn hàng, không thể xóa!";

            if (dal.XoaNV(maNV)) return "Thành công";

            return "Thất bại";
        }

        // Thêm + Sửa
        public string KiemTraVaLuu(NhanVienDTO nv, string hanhDong)
        {
            // 1. Check rỗng
            if (string.IsNullOrWhiteSpace(nv.HoTen))
                return "Họ tên không được để trống!";

            if (string.IsNullOrWhiteSpace(nv.SoDienThoai))
                return "SĐT không được để trống!";

            // 2. Check số điện thoại
            if (!nv.SoDienThoai.All(char.IsDigit) ||
                nv.SoDienThoai.Length < 10 || nv.SoDienThoai.Length > 11)
                return "SĐT không hợp lệ!";

            // 3. Thực hiện
            if (hanhDong == "THEM")
            {
                if (dal.ThemNV(nv)) return "Thành công";
            }
            else if (hanhDong == "SUA")
            {
                if (dal.SuaNV(nv)) return "Thành công";
            }

            return "Thất bại";
        }

        // Tìm kiếm
        public DataTable TimKiem(string ten)
        {
            return dal.TimKiemNV(ten);
        }
    }
}
