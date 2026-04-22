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
    internal class PhieuNhapBLL
    {
        PhieuNhapDAL dal = new PhieuNhapDAL();

        public bool LuuPhieu(PhieuNhapDTO pn, DataTable dt)
        {
            return dal.LuuPhieuFull(pn, dt);
        }
    }
}
