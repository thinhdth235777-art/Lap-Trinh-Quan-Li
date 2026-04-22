using System;
using System.Data;

namespace QLDCAM.Data_Access_Layer
{
    internal class ThongKeDAL : DBConnect
    {
        // 1. Báo cáo doanh thu
        public DataTable DoanhThuChiTiet(DateTime from, DateTime to)
        {
            string f = from.ToString("yyyy-MM-dd");
            string t = to.ToString("yyyy-MM-dd");

            string sql = $@"SELECT d.MaDonHang, d.NgayLap, k.HoTen, d.TongTien 
                           FROM DonHang d 
                           INNER JOIN KhachHang k ON d.MaKhachHang = k.MaKhachHang 
                           WHERE d.NgayLap BETWEEN '{f}' AND '{t}'";

            return LayBangDuLieu(sql);
        }

        // 2. Báo cáo sản phẩm bán chạy
        public DataTable TopSanPhamBanChay(DateTime from, DateTime to, int topN = 10)
        {
            string f = from.ToString("yyyy-MM-dd");
            string t = to.ToString("yyyy-MM-dd");

            string sql = $@"SELECT TOP ({topN}) sp.MaSanPham, sp.TenSanPham, 
                           SUM(ct.SoLuong) AS TongSoLuong, SUM(ct.SoLuong * ct.DonGia) AS ThanhTien
                           FROM ChiTietDonHang ct
                           JOIN SanPham sp ON ct.MaSanPham = sp.MaSanPham
                           JOIN DonHang dh ON ct.MaDonHang = dh.MaDonHang
                           WHERE dh.NgayLap BETWEEN '{f}' AND '{t}'
                           GROUP BY sp.MaSanPham, sp.TenSanPham
                           ORDER BY TongSoLuong DESC";

            return LayBangDuLieu(sql);
        }

        // 3. Báo cáo hàng tồn kho
        public DataTable SanPhamTonKho(int threshold = 10)
        {

            string sql = $@"SELECT 
                        s.MaSanPham, 
                        s.TenSanPham, 
                        l.TenLoai,
                        s.GiaBan, 
                        s.SoLuongTon
                    FROM SanPham s
                    INNER JOIN LoaiSanPham l ON s.MaLoai = l.MaLoai
                    WHERE s.SoLuongTon <= {threshold}
                    ORDER BY s.SoLuongTon ASC";

            return LayBangDuLieu(sql);
        }

        public DataTable LayTongHopDoanhThu(DateTime from, DateTime to)
        {
            string f = from.ToString("yyyy-MM-dd");
            string t = to.ToString("yyyy-MM-dd");
            // Lấy tổng tiền, số hóa đơn và số khách hàng trong 1 câu lệnh
            string sql = $@"SELECT 
                    ISNULL(SUM(TongTien),0) AS TongDoanhThu, 
                    COUNT(*) AS SoHoaDon
                    FROM DonHang WHERE NgayLap BETWEEN '{f}' AND '{t}'";
            return LayBangDuLieu(sql);
        }

        public int LayTongSoSanPham()
        {
            string sql = "SELECT COUNT(*) FROM SanPham";
            DataTable dt = LayBangDuLieu(sql);
            if (dt.Rows.Count > 0) return Convert.ToInt32(dt.Rows[0][0]);
            return 0;
        }
    }
}