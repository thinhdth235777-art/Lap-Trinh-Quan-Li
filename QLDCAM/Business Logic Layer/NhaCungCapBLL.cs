using QLDCAM.Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDCAM.Business_Logic_Layer
{
    public class NhaCungCapBLL
    {
        NhaCungCapDAL dal = new NhaCungCapDAL();

        public DataTable LayDanhSach()
        {
            return dal.GetNhaCungCap();
        }
    }
}
