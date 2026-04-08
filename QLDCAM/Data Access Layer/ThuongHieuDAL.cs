using QLDCAM.Data_Transfer_Object;
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
            string sql = "SELECT * FROM ThuongHieu";
            return db.LayBangDuLieu(sql);
        }

        public bool ThemTH(ThuongHieuDTO th)
        {
            string sql = string.Format("INSERT INTO ThuongHieu (TenThuongHieu) VALUES (N'{0}')", th.TenThuongHieu);
            return db.ThucThiLenh(sql);
        }

        public bool SuaTH(ThuongHieuDTO th)
        {
            string sql = string.Format("UPDATE ThuongHieu SET TenThuongHieu = N'{0}' WHERE MaThuongHieu = {1}",
                                        th.TenThuongHieu, th.MaThuongHieu);
            return db.ThucThiLenh(sql);
        }

        public bool XoaTH(int ma)
        {
            try
            {
                string sql = "DELETE FROM ThuongHieu WHERE MaThuongHieu = " + ma;
                return db.ThucThiLenh(sql);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public DataTable TimKiem(string tuKhoa)
        {
            string sql = string.Format("SELECT * FROM ThuongHieu WHERE TenThuongHieu LIKE N'%{0}%'", tuKhoa);
            return db.LayBangDuLieu(sql);
        }
    }
}
