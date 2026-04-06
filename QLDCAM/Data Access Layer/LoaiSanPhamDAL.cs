using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDCAM.Data_Access_Layer
{
    public class LoaiSanPhamDAL
    {
        DBConnect db = new DBConnect();

        // Hàm lấy toàn bộ danh sách loại SP
        public DataTable GetLoaiSP()
        {
            string sql = "SELECT * FROM LoaiSanPham";
            return db.LayBangDuLieu(sql);
        }
    }
}
