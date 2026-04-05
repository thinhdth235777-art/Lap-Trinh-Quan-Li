using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDCAM.Data_Transfer_Object
{
    internal class TaiKhoanDTO
    {
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public string Email { get; set; }
        public string Quyen { get; set; }
        public TaiKhoanDTO() { }

        public TaiKhoanDTO(string user, string pass, string email, string quyen)
        {
            this.TenDangNhap = user;
            this.MatKhau = pass;
            this.Email = email;
            this.Quyen = quyen;
        }
    }
}
