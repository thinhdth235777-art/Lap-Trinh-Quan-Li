using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDCAM.Data_Access_Layer
{
    public class NhaCungCapDAL
    {
        DBConnect db = new DBConnect();

        public DataTable GetNhaCungCap()
        {
            // Câu lệnh SQL lấy toàn bộ danh sách nhà cung cấp
            string sql = "SELECT * FROM NhaCungCap";

            // Sử dụng hàm LayBangDuLieu có sẵn trong DBConnect của Thịnh
            return db.LayBangDuLieu(sql);
        }
    }
}
