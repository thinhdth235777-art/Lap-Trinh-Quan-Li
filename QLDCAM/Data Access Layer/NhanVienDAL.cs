using QLDCAM.Data_Transfer_Object;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDCAM.Data_Access_Layer
{
    internal class NhanVienDAL
    {
        DBConnect db = new DBConnect();

        // Lấy danh sách
        public DataTable LayTatCaNhanVien()
        {
            string sql = "SELECT * FROM NhanVien";
            return db.LayBangDuLieu(sql);
        }

        // Thêm
        public bool ThemNV(NhanVienDTO nv)
        {
            string sql = string.Format(@"INSERT INTO NhanVien 
                (HoTen, ChucVu, SoDienThoai, TaiKhoan, MatKhau)
                VALUES (N'{0}', N'{1}', '{2}', '{3}', '{4}')",
                nv.HoTen, nv.ChucVu, nv.SoDienThoai, nv.TaiKhoan, nv.MatKhau);

            return db.ThucThiLenh(sql);
        }

        // Sửa
        public bool SuaNV(NhanVienDTO nv)
        {
            string sql = string.Format(@"UPDATE NhanVien 
                SET HoTen = N'{0}',
                    ChucVu = N'{1}',
                    SoDienThoai = '{2}',
                    TaiKhoan = '{3}',
                    MatKhau = '{4}'
                WHERE MaNhanVien = {5}",
                nv.HoTen, nv.ChucVu, nv.SoDienThoai,
                nv.TaiKhoan, nv.MatKhau, nv.MaNhanVien);

            return db.ThucThiLenh(sql);
        }

        // Xóa
        public bool XoaNV(int maNV)
        {
            string sql = string.Format("DELETE FROM NhanVien WHERE MaNhanVien = {0}", maNV);
            return db.ThucThiLenh(sql);
        }

        //  Kiểm tra nhân viên có đơn hàng không
        public bool KiemTraTonTaiDonHang(int maNV)
        {
            string sql = string.Format("SELECT * FROM DonHang WHERE MaNhanVien = {0}", maNV);
            DataTable dt = db.LayBangDuLieu(sql);
            return dt.Rows.Count > 0;
        }

        // Tìm kiếm
        public DataTable TimKiemNV(string ten)
        {
            string sql = string.Format(@"SELECT * FROM NhanVien 
                                        WHERE HoTen LIKE N'%{0}%'", ten);
            return db.LayBangDuLieu(sql);
        }
    }
}
