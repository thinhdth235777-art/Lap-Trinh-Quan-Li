using QLDCAM.Data_Transfer_Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLDCAM.Data_Access_Layer;
using QLDCAM.Data_Transfer_Object;

namespace QLDCAM.Business_Logic_Layer
{
    internal class TaiKhoanBLL
    {
        TaiKhoanDAL dal = new TaiKhoanDAL();

        public bool CheckLogin(string user, string pass)
        {
           
            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
            {
                return false;
            }
            return dal.KiemTraDangNhap(user, pass);
        }
        public string RegisterAccount(TaiKhoanDTO tk, string confirmPass)
        {
            // 1. Kiểm tra trống
            if (string.IsNullOrEmpty(tk.TenDangNhap) || string.IsNullOrEmpty(tk.MatKhau))
                return "Vui lòng nhập đầy đủ Tài khoản và Mật khẩu!";

            // 2. Kiểm tra khớp mật khẩu
            if (tk.MatKhau != confirmPass)
                return "Mật khẩu xác nhận không khớp!";

            // 3. Gọi xuống DAL để lưu
            if (dal.ThemTaiKhoan(tk))
                return "Thành công";

            return "Tên đăng nhập đã tồn tại hoặc có lỗi hệ thống!";
        }
        public TaiKhoanDTO LayThongTinTaiKhoan(string user, string pass)
        {
            // Đây là cầu nối: BLL gọi xuống hàm bạn vừa chuyển sang DAL lúc nãy
            return dal.LayThongTinTaiKhoan(user, pass);
        }
    }
}
