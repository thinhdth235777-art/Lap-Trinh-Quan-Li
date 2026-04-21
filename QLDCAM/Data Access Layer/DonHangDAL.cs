using QLDCAM.Data_Transfer_Object;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDCAM.Data_Access_Layer
{
    internal class DonHangDAL
    {
        DBConnect db=new DBConnect();

        public DataTable LayDanhSachHoaDon()
        {
                // Join các bảng để lấy tên nhân viên và khách hàng
                string sql = @"SELECT dh.MaDonHang, nv.HoTen as TenNhanVien, kh.HoTen as TenKhachHang, 
                          dh.NgayDatHang, dh.TongTien 
                          FROM DonHang dh
                          JOIN NhanVien nv ON dh.MaNhanVien = nv.MaNhanVien
                          JOIN KhachHang kh ON dh.MaKhachHang = kh.MaKhachHang";
            return db.LayBangDuLieu(sql);
        }

        public DataTable LayChiTietHoaDon(int maHD)
        {
                string sql = @"SELECT ct.MaSanPham, sp.TenSanPham, ct.SoLuong, ct.DonGia, (ct.SoLuong * ct.DonGia) as ThanhTien
                          FROM ChiTietDonHang ct
                          JOIN SanPham sp ON ct.MaSanPham = sp.MaSanPham
                          WHERE ct.MaDonHang = @ma";
                SqlCommand cmd = new SqlCommand(sql);
                cmd.Parameters.AddWithValue("@ma", maHD);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;        }
    }
}
