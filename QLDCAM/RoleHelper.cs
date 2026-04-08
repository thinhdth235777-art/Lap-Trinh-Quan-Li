using QLDCAM.Data_Transfer_Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLDCAM
{
    public static class RoleHelper
    {
        /// <summary>
        /// Hàm tự động ẩn/vô hiệu hóa các nút chức năng dựa trên quyền
        /// </summary>
        /// <param name="parent">Thường truyền vào 'this' (là cái Form hiện tại)</param>
        public static void CheckRole(Control parent)
        {
            // Nếu là Admin thì không làm gì cả, cho phép dùng hết
            if (SessionUser.UserHienTai != null && SessionUser.UserHienTai.Quyen == "Admin")
            {
                return;
            }

            // Duyệt qua tất cả các Control có trên Form/Panel
            foreach (Control c in parent.Controls)
            {
                // 1. Nếu là Button, kiểm tra tên để ẩn
                if (c is Button)
                {
                    string name = c.Name.ToLower();
                    // Nếu tên nút có chứa chữ "them", "sua", "xoa", "luu" thì ẩn đi
                    if (name.Contains("them") || name.Contains("sua") || name.Contains("xoa") || name.Contains("luu"))
                    {
                        c.Visible = false;
                        // Hoặc dùng c.Enabled = false; nếu muốn hiện nhưng không cho bấm
                    }
                }

                // 2. Nếu Control này chứa các Control con khác (như Panel, GroupBox)
                // thì gọi lại chính hàm này để "chui" vào bên trong tìm tiếp
                if (c.HasChildren)
                {
                    CheckRole(c);
                }
            }
        }
    }
}
