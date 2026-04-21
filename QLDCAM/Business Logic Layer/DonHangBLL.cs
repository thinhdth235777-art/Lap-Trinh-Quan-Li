using QLDCAM.Data_Access_Layer;
using QLDCAM.Data_Transfer_Object;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLDCAM.Business_Logic_Layer
{
    internal class DonHangBLL
    {
        DonHangDAL dal = new DonHangDAL();
        KhachHangDAL khDAL = new KhachHangDAL();
        SanPhamDAL spDAL = new SanPhamDAL();

        // ========================
        // 1. Lấy danh sách hóa đơn
        // ========================
        public DataTable LayDanhSachDonHang()
        {
            return dal.LayDanhSachHoaDon();
        }

        // ========================
        // 2. Tính phí vận chuyển
        // ========================
        public int TinhPhiVanChuyen(int maKH)
        {
            string tinhThanh = khDAL.LayTinhThanhTheoMaKH(maKH);

            if (tinhThanh == "An Giang")
                return 0;

            return 30000;
        }

        // ========================
        // 3. Kiểm tra tồn kho
        // ========================
        public string KiemTraSoLuong(int maSP, int soLuong)
        {
            if (soLuong <= 0)
                return "Số lượng phải > 0";

            int ton = spDAL.LaySoLuongTon(maSP);

            if (soLuong > ton)
                return "Không đủ hàng trong kho!";

            return "Hợp lệ";
        }

        // ========================
        // 4. Tính thành tiền
        // ========================
        public decimal TinhThanhTien(int soLuong, decimal donGia)
        {
            return soLuong * donGia;
        }

        // ========================
        // 5. Tính tổng tiền
        // ========================
        public decimal TinhTongTien(List<ChiTietDonHangDTO> dsCT)
        {
            decimal tong = 0;

            foreach (var item in dsCT)
            {
                tong += item.SoLuong * item.DonGia;
            }

            return tong;
        }

        // ========================
        // 6. Tổng thanh toán
        // ========================
        public decimal TinhTongThanhToan(decimal tongTien, int phiVC)
        {
            return tongTien + phiVC;
        }

        // ========================
        // 7. Kiểm tra + Lưu hóa đơn
        // ========================
        public string KiemTraVaLuu(DonHangDTO dh, List<ChiTietDonHangDTO> dsCT)
        {
            // 1. Check khách hàng
            if (dh.MaKhachHang<=0)
                return "Vui lòng chọn khách hàng!";

            // 2. Check sản phẩm
            if (dsCT == null || dsCT.Count == 0)
                return "Chưa có sản phẩm nào!";

            // 3. Check tồn kho từng dòng
            foreach (var item in dsCT)
            {
                int ton = spDAL.LaySoLuongTon(item.MaSanPham);

                if (item.SoLuong > ton)
                    return $"SP {item.MaSanPham} không đủ hàng!";
            }

            // 4. Tính tiền
            decimal tongTien = TinhTongTien(dsCT);
            int phiVC = TinhPhiVanChuyen(dh.MaKhachHang);
            decimal tongThanhToan = TinhTongThanhToan(tongTien, phiVC);

            dh.TongTien = tongThanhToan;

            // 5. Lưu hóa đơn
            int maDH = dal.(dh);

            if (maDH <= 0)
                return "Lưu hóa đơn thất bại!";

            // 6. Lưu chi tiết
            foreach (var item in dsCT)
            {
                item.MaDonHang = maDH;

                if (!dal.ThemChiTietDonHang(item))
                    return "Lỗi lưu chi tiết hóa đơn!";
            }

            return "Thành công";
        }
    }
}
