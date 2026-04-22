using System;
using System.Data;
using QLDCAM.Data_Access_Layer;

namespace QLDCAM.Business_Logic_Layer
{
    internal class ThongKeBLL
    {
        ThongKeDAL dal = new ThongKeDAL();

        // --- PHỤC VỤ DASHBOARD ---
        public DataTable LayTongHopDoanhThu(DateTime from, DateTime to) => dal.LayTongHopDoanhThu(from, to);
        public int LayTongSoSanPham() => dal.LayTongSoSanPham();
        public DataTable LaySanPhamSapHet(int threshold) => dal.SanPhamTonKho(threshold);

        // --- PHỤC VỤ CRYSTAL REPORT ---
        public DataTable LayBaoCao(string loai, DateTime from, DateTime to)
        {
            if (loai == "DoanhThu") return dal.DoanhThuChiTiet(from, to);
            if (loai == "SanPhamBanChay") return dal.TopSanPhamBanChay(from, to);
            if (loai == "HangTonKho") return dal.SanPhamTonKho(10);
            return null;
        }
    }
}