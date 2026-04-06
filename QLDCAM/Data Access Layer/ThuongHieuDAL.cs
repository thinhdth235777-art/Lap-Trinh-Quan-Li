using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDCAM.Data_Access_Layer
{
    public class ThuongHieuDAL
    {
        DBConnect db = new DBConnect();

        public DataTable GetThuongHieu()
        {
            return db.LayBangDuLieu("SELECT * FROM ThuongHieu");
        }
    }
}
