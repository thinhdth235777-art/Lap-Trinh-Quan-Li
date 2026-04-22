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
            // Trả về cả MaSanPham để các thao tác hoàn trả kho có thể dùng
            string sql = "SELECT ct.MaSanPham, sp.TenSanPham, ct.SoLuong, ct.DonGia, (ct.SoLuong*ct.DonGia) as ThanhTien FROM ChiTietDonHang ct JOIN SanPham sp ON ct.MaSanPham = sp.MaSanPham WHERE ct.MaDonHang = @maHD";
            try
            {
                OpenConn();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@maHD", maHD);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
            finally { CloseConn(); }
        }

        // Lấy phí vận chuyển đã lưu cho đơn hàng
        public decimal LayPhiVanChuyen(int maHD)
        {
            try
            {
                OpenConn();
                string sql = "SELECT PhiVanChuyen FROM DonHang WHERE MaDonHang = @maHD";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@maHD", maHD);
                    object o = cmd.ExecuteScalar();
                    if (o != null && o != DBNull.Value)
                        return Convert.ToDecimal(o);
                    return 0m;
                }
            }
            finally { CloseConn(); }
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
                            string insertCT = "INSERT INTO ChiTietDonHang (MaDonHang, MaSanPham, SoLuong, DonGia) VALUES (@mahd, @masp, @sl, @dg)";
                            using (SqlCommand cmd = new SqlCommand(insertCT, conn, trans))
                            {
                                cmd.Parameters.AddWithValue("@mahd", maHD);
                                cmd.Parameters.AddWithValue("@masp", item.MaSanPham);
                                cmd.Parameters.AddWithValue("@sl", item.SoLuong);
                                cmd.Parameters.AddWithValue("@dg", item.DonGia);
                                cmd.ExecuteNonQuery();
                            }

                            string updateStock = "UPDATE SanPham SET SoLuongTon = SoLuongTon - @sl WHERE MaSanPham = @masp";
                            using (SqlCommand cmd2 = new SqlCommand(updateStock, conn, trans))
                            {
                                cmd2.Parameters.AddWithValue("@sl", item.SoLuong);
                                cmd2.Parameters.AddWithValue("@masp", item.MaSanPham);
                                cmd2.ExecuteNonQuery();
                            }
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
                        using (SqlCommand cmd = new SqlCommand(sqlRefund, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@sl", sl);
                            cmd.Parameters.AddWithValue("@masp", maSP);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    // 2. Xóa chi tiết trước (vì có khóa ngoại)
                    using (SqlCommand delCT = new SqlCommand("DELETE FROM ChiTietDonHang WHERE MaDonHang = @maHD", conn, trans))
                    {
                        delCT.Parameters.AddWithValue("@maHD", maHD);
                        delCT.ExecuteNonQuery();
                    }

                    // 3. Xóa đơn hàng
                    using (SqlCommand delHD = new SqlCommand("DELETE FROM DonHang WHERE MaDonHang = @maHD", conn, trans))
                    {
                        delHD.Parameters.AddWithValue("@maHD", maHD);
                        delHD.ExecuteNonQuery();
                    }

                    trans.Commit();
                    return true;
                }
                catch { trans.Rollback(); return false; }
            }
            finally { CloseConn(); }
        }
        public DataTable LayDuLieuInHoaDon(int maHD)
        {
            DataTable dt = new DataTable();
            string chuoiKetNoi = @"Data Source=DESKTOP-2947EDG;Initial Catalog=QLCHDungCuAmNhac;Integrated Security=True";
            // Câu lệnh SQL thực hiện kết nối các bảng để lấy đầy đủ thông tin hóa đơn
            string sql = @"SELECT d.MaDonHang, d.NgayLap, k.HoTen AS TenKhachHang, 
                          s.TenSanPham, ct.SoLuong, ct.DonGia, 
                          (ct.SoLuong * ct.DonGia) AS ThanhTien
                   FROM DonHang d
                   JOIN KhachHang k ON d.MaKhachHang = k.MaKhachHang
                   JOIN ChiTietDonHang ct ON d.MaDonHang = ct.MaDonHang
                   JOIN SanPham s ON ct.MaSanPham = s.MaSanPham
                   WHERE d.MaDonHang = @MaHD";

            // Sử dụng đối tượng kết nối database của bạn (ví dụ: DataProvider hoặc SqlConnection)
            // Dưới đây là cách viết dùng SqlParameter để truyền mã hóa đơn vào câu lệnh SQL
            try
            {
                using (SqlConnection conn = new SqlConnection(chuoiKetNoi)) // Thay 'chuoiKetNoi' bằng biến của bạn
                {
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MaHD", maHD);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                // Ghi log hoặc thông báo lỗi nếu cần
                throw ex;
            }
            return dt;
        }
    }
}

