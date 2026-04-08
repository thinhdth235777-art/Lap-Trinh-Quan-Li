using QLDCAM.Data_Transfer_Object;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace QLDCAM.Data_Access_Layer
{
    internal class ThongKeDAL : DBConnect
    {
        DBConnect db = new DBConnect();

        // Total revenue & invoice count & distinct customer count in a date range
        public DataTable DoanhThuTheoKhoangThoiGian(DateTime from, DateTime to)
        {
            string f = from.ToString("yyyy-MM-dd");
            string t = to.ToString("yyyy-MM-dd");
            string sql = $@"
SELECT 
    ISNULL(SUM(TongTien),0) AS TongDoanhThu,
    COUNT(*) AS SoHoaDon,
    (SELECT COUNT(DISTINCT MaKhachHang) FROM DonHang WHERE NgayLap BETWEEN '{f}' AND '{t}') AS SoKhachHang
FROM DonHang
WHERE NgayLap BETWEEN '{f}' AND '{t}'";
            return db.LayBangDuLieu(sql);
        }

        // Monthly revenue for a given year (Thang, TongDoanhThu)
        public DataTable DoanhThuTheoNam(int year)
        {
            string sql = $@"
SELECT MONTH(NgayLap) AS Thang, ISNULL(SUM(TongTien),0) AS TongDoanhThu
FROM DonHang
WHERE YEAR(NgayLap) = {year}
GROUP BY MONTH(NgayLap)
ORDER BY MONTH(NgayLap)";
            return db.LayBangDuLieu(sql);
        }

        // Top N best selling products by total quantity (from ChiTietDonHang)
        public DataTable TopSanPhamBanChay(int topN)
        {
            string sql = $@"
SELECT TOP({topN}) ct.MaSanPham, sp.TenSanPham, ISNULL(SUM(ct.SoLuong),0) AS TongSoLuong
FROM ChiTietDonHang ct
INNER JOIN SanPham sp ON ct.MaSanPham = sp.MaSanPham
GROUP BY ct.MaSanPham, sp.TenSanPham
ORDER BY TongSoLuong DESC";
            return db.LayBangDuLieu(sql);
        }

        // Products with low stock
        public DataTable SanPhamSapHetHang(int threshold)
        {
            string sql = $@"
SELECT MaSanPham, TenSanPham, SoLuongTon
FROM SanPham
WHERE SoLuongTon < {threshold}
ORDER BY SoLuongTon ASC";
            return db.LayBangDuLieu(sql);
        }

        // Top N customers by total purchased amount
        public DataTable TopKhachHang(int topN)
        {
            string sql = $@"
SELECT TOP({topN}) kh.MaKhachHang, kh.HoTen, ISNULL(SUM(dh.TongTien),0) AS TongMua
FROM DonHang dh
INNER JOIN KhachHang kh ON dh.MaKhachHang = kh.MaKhachHang
GROUP BY kh.MaKhachHang, kh.HoTen
ORDER BY TongMua DESC";
            return db.LayBangDuLieu(sql);
        }
        public int LayTongSoSanPham()
        {
            try
            {
                OpenConn(); // Mở kết nối SQL
                string sql = "SELECT COUNT(*) FROM SanPham";
                SqlCommand cmd = new SqlCommand(sql, conn);

                // ExecuteScalar dùng để lấy 1 giá trị duy nhất (ở đây là con số tổng)
                return (int)cmd.ExecuteScalar();
            }
            catch { return 0; }
            finally { CloseConn(); }
        }
    }
}