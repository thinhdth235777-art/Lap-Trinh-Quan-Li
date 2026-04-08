using QLDCAM.Data_Transfer_Object;
using System;
using System.Data;

namespace QLDCAM.Data_Access_Layer
{
    public class LoaiSanPhamDAL
    {
        DBConnect db = new DBConnect();

        // 1. Lấy danh sách
        public DataTable GetLoaiSP()
        {
            string sql = "SELECT * FROM LoaiSanPham";
            return db.LayBangDuLieu(sql); // Gọi qua đối tượng db
        }

        // 2. Thêm mới (Dùng DTO để truyền dữ liệu cho chuyên nghiệp)
        public bool ThemLoai(LoaiSanPhamDTO loai)
        {
            string sql = string.Format("INSERT INTO LoaiSanPham (TenLoai) VALUES (N'{0}')", loai.TenLoai);
            return db.ThucThiLenh(sql);
        }

        // 3. Sửa dữ liệu
        public bool SuaLoai(LoaiSanPhamDTO loai)
        {
            string sql = string.Format("UPDATE LoaiSanPham SET TenLoai = N'{0}' WHERE MaLoai = {1}",
                                        loai.TenLoai, loai.MaLoai);
            return db.ThucThiLenh(sql);
        }

        // 4. Xóa dữ liệu
        public bool XoaLoai(int maLoai)
        {
            try
            {
                string sql = "DELETE FROM LoaiSanPham WHERE MaLoai = " + maLoai;
                return db.ThucThiLenh(sql);
            }
            catch (Exception)
            {
                return false;
            }
        }
        public DataTable TimKiem(string tuKhoa)
        {
            // Tìm kiếm theo tên loại, dùng LIKE % để tìm gần đúng
            string sql = string.Format("SELECT * FROM LoaiSanPham WHERE TenLoai LIKE N'%{0}%'", tuKhoa);
            return db.LayBangDuLieu(sql);
        }
    }
}