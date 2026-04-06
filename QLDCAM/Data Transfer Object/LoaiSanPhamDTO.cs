using QLDCAM.Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDCAM.Data_Transfer_Object
{
    public class LoaiSanPhamDTO
    {
        public int MaLoai { get; set; }
        public string TenLoai { get; set; }
    }
}
