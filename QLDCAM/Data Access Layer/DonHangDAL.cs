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
    internal class DonHangDAL:DBConnect
    {
        // Lấy danh sách hóa đơn hiển thị lên GridView ở Form chính
        public DataTable LayDSHoaDon()
        {
            string sql = "SELECT dh.MaDonHang, kh.HoTen as TenKH, nv.HoTen as TenNV, dh.NgayLap, dh.TongTien FROM DonHang dh JOIN KhachHang kh ON dh.MaKhachHang = kh.MaKhachHang JOIN NhanVien nv ON dh.MaNhanVien = nv.MaNhanVien";
            return LayBangDuLieu(sql);
        }

        public DataTable LayChiTietTheoMa(int maHD)
        {
            string sql = $"SELECT sp.TenSanPham, ct.SoLuong, ct.DonGia, (ct.SoLuong*ct.DonGia) as ThanhTien FROM ChiTietDonHang ct JOIN SanPham sp ON ct.MaSanPham = sp.MaSanPham WHERE ct.MaDonHang = {maHD}";
            return LayBangDuLieu(sql);
        }

        public bool LuuHoaDonFull(DonHangDTO hd, List<ChiTietDonHangDTO> ds)
        {
            try
            {
                OpenConn();
                using (SqlTransaction trans = conn.BeginTransaction()) 
                {
                    try
                    {
                        // 1. Thêm Đơn Hàng
                        string sqlHD = "INSERT INTO DonHang (MaKhachHang, MaNhanVien, NgayLap, TongTien, PhiVanChuyen, TrangThai) OUTPUT INSERTED.MaDonHang VALUES (@makh, @manv, @ngay, @tong, @phi, N'Đã xong')";
                        SqlCommand cmdHD = new SqlCommand(sqlHD, conn, trans);
                        cmdHD.Parameters.AddWithValue("@makh", hd.MaKhachHang);
                        cmdHD.Parameters.AddWithValue("@manv", hd.MaNhanVien);
                        cmdHD.Parameters.AddWithValue("@ngay", DateTime.Now);
                        cmdHD.Parameters.AddWithValue("@tong", hd.TongTien);
                        cmdHD.Parameters.AddWithValue("@phi", hd.PhiVanChuyen);
                        int maHD = (int)cmdHD.ExecuteScalar();

                        // 2. Thêm Chi tiết & Trừ kho
                        foreach (var item in ds)
                        {
                            new SqlCommand($"INSERT INTO ChiTietDonHang VALUES ({maHD}, {item.MaSanPham}, {item.SoLuong}, {item.DonGia})", conn, trans).ExecuteNonQuery();
                            new SqlCommand($"UPDATE SanPham SET SoLuongTon = SoLuongTon - {item.SoLuong} WHERE MaSanPham = {item.MaSanPham}", conn, trans).ExecuteNonQuery();
                        }
                        trans.Commit(); return true;
                    }
                    catch { trans.Rollback(); return false; }
                }
            }
            finally { CloseConn(); }
        }
        public bool XoaHoaDon(int maHD)
        {
            try
            {
                OpenConn();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    // 1. Lấy danh sách chi tiết để hoàn trả kho
                    DataTable dt = LayChiTietTheoMa(maHD);
                    foreach (DataRow row in dt.Rows)
                    {
                        int maSP = Convert.ToInt32(row["MaSanPham"]);
                        int sl = Convert.ToInt32(row["SoLuong"]);
                        string sqlRefund = "UPDATE SanPham SET SoLuongTon = SoLuongTon + @sl WHERE MaSanPham = @masp";
                        SqlCommand cmd = new SqlCommand(sqlRefund, conn, trans);
                        cmd.Parameters.AddWithValue("@sl", sl);
                        cmd.Parameters.AddWithValue("@masp", maSP);
                        cmd.ExecuteNonQuery();
                    }

                    // 2. Xóa chi tiết trước (vì có khóa ngoại)
                    new SqlCommand($"DELETE FROM ChiTietDonHang WHERE MaDonHang = {maHD}", conn, trans).ExecuteNonQuery();

                    // 3. Xóa đơn hàng
                    new SqlCommand($"DELETE FROM DonHang WHERE MaDonHang = {maHD}", conn, trans).ExecuteNonQuery();

                    trans.Commit();
                    return true;
                }
                catch { trans.Rollback(); return false; }
            }
            finally { CloseConn(); }
        }
    }
}

