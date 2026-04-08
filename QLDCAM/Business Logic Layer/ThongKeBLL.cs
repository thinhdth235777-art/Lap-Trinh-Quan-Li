using System;
using System.Data;
using QLDCAM.Data_Access_Layer;


namespace QLDCAM.Business_Logic_Layer
{
    internal class ThongKeBLL
    {
        ThongKeDAL dal = new ThongKeDAL();

        // Summary over date range: returns a DataTable with columns TongDoanhThu, SoHoaDon, SoKhachHang
        public DataTable LayTongHopDoanhThu(DateTime from, DateTime to)
        {
            return dal.DoanhThuTheoKhoangThoiGian(from, to);
        }

        // Monthly revenue rows for a year (Thang, TongDoanhThu)
        public DataTable LayDoanhThuTheoNam(int year)
        {
            return dal.DoanhThuTheoNam(year);
        }

        public DataTable LayTopSanPhamBanChay(int topN = 5)
        {
            return dal.TopSanPhamBanChay(topN);
        }

        public DataTable LaySanPhamSapHet(int threshold = 5)
        {
            return dal.SanPhamSapHetHang(threshold);
        }

        public DataTable LayTopKhachHang(int topN = 5)
        {
            return dal.TopKhachHang(topN);
        }

        // Optional small helper: compute percent growth between last two months in a monthly table
        // Expects table with columns: Thang (int), TongDoanhThu (decimal)
        public decimal TinhTyLeTangTruong(DataTable monthly)
        {
            if (monthly == null || monthly.Rows.Count < 2) return 0m;
            // Take last two months by Thang
            DataRow last = monthly.Rows[monthly.Rows.Count - 1];
            DataRow prev = monthly.Rows[monthly.Rows.Count - 2];
            decimal lastVal = Convert.ToDecimal(last["TongDoanhThu"]);
            decimal prevVal = Convert.ToDecimal(prev["TongDoanhThu"]);
            if (prevVal == 0) return lastVal == 0 ? 0m : 100m;
            return Math.Round((lastVal - prevVal) / prevVal * 100m, 2);
        }
        public int LayTongSoSanPham()
        {
            return dal.LayTongSoSanPham();
        }
    }
}