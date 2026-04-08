using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDCAM.Data_Access_Layer
{
    using QLDCAM.Data_Transfer_Object;
    using System.Data.SqlClient;

    public class SanPhamDAL
    {
        DBConnect db = new DBConnect();

        public DataTable LayTatCaSanPham()
        {
            // Câu lệnh này giúp lấy tên thay vì lấy mã số (đúng nghiệp vụ hình mẫu)
            string sql = @"SELECT sp.MaSanPham, sp.TenSanPham, l.TenLoai, t.TenThuongHieu, 
                                  sp.GiaBan, sp.SoLuongTon,     sp.MoTa, sp.HinhAnh 
                           FROM SanPham sp
                           JOIN LoaiSanPham l ON sp.MaLoai = l.MaLoai
                           JOIN ThuongHieu t ON sp.MaThuongHieu = t.MaThuongHieu";

            return db.LayBangDuLieu(sql);
        }

        public bool ThemSP(SanPhamDTO sp)
        {
            string sql = string.Format(@"INSERT INTO SanPham (TenSanPham, MaLoai, MaThuongHieu, GiaBan, SoLuongTon, MoTa, HinhAnh) 
                                 VALUES (N'{0}', {1}, {2}, {3}, {4}, N'{5}', N'{6}')",
                                         sp.TenSanPham, sp.MaLoai, sp.MaThuongHieu, sp.GiaBan, sp.SoLuongTon, sp.MoTa, sp.HinhAnh);

            return db.ThucThiLenh(sql);
        }
        public bool XoaSP(int maSP)
        {
            // Câu lệnh SQL xóa theo mã sản phẩm (id)
            string sql = string.Format("DELETE FROM SanPham WHERE MaSanPham = {0}", maSP);

            return db.ThucThiLenh(sql);
        }

        public bool SuaSP(SanPhamDTO sp)
        {
            // Câu lệnh UPDATE: Nhớ dùng N'{...}' cho các trường tiếng Việt có dấu
            // Dựa vào MaSanPham để cập nhật đúng dòng
            string sql = string.Format(@"UPDATE SanPham 
                                 SET TenSanPham = N'{0}', 
                                     MaLoai = {1}, 
                                     MaThuongHieu = {2}, 
                                     GiaBan = {3}, 
                                     SoLuongTon = {4}, 
                                     MoTa = N'{5}', 
                                     HinhAnh = N'{6}' 
                                 WHERE MaSanPham = {7}",
                                         sp.TenSanPham,
                                         sp.MaLoai,
                                         sp.MaThuongHieu,
                                         sp.GiaBan,
                                         sp.SoLuongTon,
                                         sp.MoTa,
                                         sp.HinhAnh,
                                         sp.MaSanPham);

            return db.ThucThiLenh(sql);
        }
        public DataTable TimKiemSP(string ten)
        {
            // Sử dụng LIKE N'%{0}%' để tìm gần đúng (gõ "ip" là ra "iPhone")
            string sql = string.Format(@"SELECT sp.MaSanPham, sp.TenSanPham, l.TenLoai, t.TenThuongHieu, 
                                       sp.GiaBan, sp.SoLuongTon, sp.MoTa, sp.HinhAnh 
                                FROM SanPham sp
                                JOIN LoaiSanPham l ON sp.MaLoai = l.MaLoai
                                JOIN ThuongHieu t ON sp.MaThuongHieu = t.MaThuongHieu
                                WHERE sp.TenSanPham LIKE N'%{0}%'", ten);

            return db.LayBangDuLieu(sql);
        }
        public int LaySoLuongTon(int maSP)
        {
            try
            {
                string sql = "SELECT SoLuongTon FROM SanPham WHERE MaSanPham = @maSP";

                SqlCommand cmd = new SqlCommand(sql);
                cmd.Parameters.AddWithValue("@maSP", maSP);

                object result = cmd.ExecuteScalar();

                if (result != null)
                    return Convert.ToInt32(result);

                return 0;
            }
            catch
            {
                return 0;
            }
        }
    }
}
