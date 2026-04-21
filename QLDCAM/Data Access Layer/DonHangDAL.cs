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
                return dt;        
        }
        // Thêm hóa đơn mới
        public bool ThemDH(DonHangDTO dh)
        {
                string sql = "INSERT INTO DonHang (MaNhanVien, MaKhachHang, NgayDatHang, TongTien) VALUES (@manv, @makh, @ngay, @tongtien)";
                SqlCommand cmd = new SqlCommand(sql);
                cmd.Parameters.AddWithValue("@manv", dh.MaNhanVien);
                cmd.Parameters.AddWithValue("@makh", dh.MaKhachHang);
                cmd.Parameters.AddWithValue("@ngay", dh.NgayDatHang);
                cmd.Parameters.AddWithValue("@tongtien", dh.TongTien);
                return cmd.ExecuteNonQuery() > 0;
        }

        // Sửa hóa đơn (thường chỉ sửa ngày hoặc khách hàng, ít khi sửa tổng tiền thủ công)
        public bool SuaDH(DonHangDTO dh)
        {
                string sql = "UPDATE DonHang SET MaKhachHang=@makh, NgayDatHang=@ngay WHERE MaDonHang=@ma";
                SqlCommand cmd = new SqlCommand(sql);
                cmd.Parameters.AddWithValue("@makh", dh.MaKhachHang);
                cmd.Parameters.AddWithValue("@ngay", dh.NgayDatHang);
                cmd.Parameters.AddWithValue("@ma", dh.MaDonHang);
                return cmd.ExecuteNonQuery() > 0;
        }

        // Xóa hóa đơn (Phải xóa ChiTietDonHang trước vì ràng buộc khóa ngoại)
        public bool XoaDH(int maDH)
        {
                try
                {
                    // 1. Xóa chi tiết
                    string sqlCT = "DELETE FROM ChiTietDonHang WHERE MaDonHang = @ma";
                    SqlCommand cmdCT = new SqlCommand(sqlCT);
                    cmdCT.Parameters.AddWithValue("@ma", maDH);
                    cmdCT.ExecuteNonQuery();

                    // 2. Xóa đơn hàng
                    string sqlDH = "DELETE FROM DonHang WHERE MaDonHang = @ma";
                    SqlCommand cmdDH = new SqlCommand(sqlDH);
                    cmdDH.Parameters.AddWithValue("@ma", maDH);
                    cmdDH.ExecuteNonQuery();
                    return true;
                }
                catch
                {
                    return false;
                }
        }
    }
}
