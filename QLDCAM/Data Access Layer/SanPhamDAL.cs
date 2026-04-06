using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDCAM.Data_Access_Layer
{
    using QLDCAM.Data_Transfer_Object;
    public class SanPhamDAL
    {
        DBConnect db = new DBConnect();

        public DataTable LayTatCaSanPham()
        {
            // Câu lệnh này giúp lấy tên thay vì lấy mã số (đúng nghiệp vụ hình mẫu)
            string sql = @"SELECT sp.MaSanPham, sp.TenSanPham, l.TenLoai, t.TenThuongHieu, 
                                  sp.GiaBan, sp.SoLuongTon, sp.HinhAnh 
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
    }
}
