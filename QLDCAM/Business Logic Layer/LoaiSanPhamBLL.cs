using QLDCAM.Data_Access_Layer;
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

        // Hàm này để Form gọi, rồi nó đi hỏi DAL để lấy bảng về
        public DataTable LayDanhSachLoai()
        {
            return dal.GetLoaiSP();
        }
    }
}
