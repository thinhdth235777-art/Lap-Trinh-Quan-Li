using QLDCAM.Data_Transfer_Object;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDCAM.Data_Access_Layer
{
    internal class KhachHangDAL
    {
        DBConnect db = new DBConnect();

        // Lấy danh sách
        public DataTable LayTatCaKhachHang()
        {
            string sql = "SELECT * FROM KhachHang";
            return db.LayBangDuLieu(sql);
        }

        // Thêm
        public bool ThemKH(KhachHangDTO kh)
        {
            string sql = string.Format(@"INSERT INTO KhachHang 
                (HoTen, SoDienThoai, Email, DiaChi, TinhThanh)
                VALUES (N'{0}', '{1}', '{2}', N'{3}', N'{4}')",
                kh.HoTen, kh.SoDienThoai, kh.Email, kh.DiaChi, kh.TinhThanh);

            return db.ThucThiLenh(sql);
        }

        // Sửa
        public bool SuaKH(KhachHangDTO kh)
        {
            string sql = string.Format(@"UPDATE KhachHang 
                SET HoTen = N'{0}',
                    SoDienThoai = '{1}',
                    Email = '{2}',
                    DiaChi = N'{3}',
                    TinhThanh = N'{4}'
                WHERE MaKhachHang = {5}",
                kh.HoTen, kh.SoDienThoai, kh.Email,
                kh.DiaChi, kh.TinhThanh, kh.MaKhachHang);

            return db.ThucThiLenh(sql);
        }

        // Xóa
        public bool XoaKH(int maKH)
        {
            string sql = string.Format("DELETE FROM KhachHang WHERE MaKhachHang = {0}", maKH);
            return db.ThucThiLenh(sql);
        }

        // 🔥 Kiểm tra trùng SĐT
        public bool KiemTraTrungSDT(string sdt)
        {
            string sql = string.Format("SELECT * FROM KhachHang WHERE SoDienThoai = '{0}'", sdt);
            DataTable dt = db.LayBangDuLieu(sql);
            return dt.Rows.Count > 0;
        }

        // Tìm kiếm
        public DataTable TimKiemKH(string ten)
        {
            string sql = string.Format(@"SELECT * FROM KhachHang 
                                        WHERE HoTen LIKE N'%{0}%'", ten);
            return db.LayBangDuLieu(sql);
        }
    }
}
