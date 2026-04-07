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

        public DataTable LayTatCaDonHang()
        {
            string sql = "SELECT * FROM DonHang";
            return db.LayBangDuLieu(sql);
        }
        public int ThemDonHang(DonHangDTO hd)
        {
            try
            {
                string sql = @"INSERT INTO DonHang(NgayLap, MaKH, TongTien)
                       OUTPUT INSERTED.MaHD
                       VALUES(@NgayLap, @MaKH, @TongTien)";

                SqlCommand cmd = new SqlCommand(sql);

                cmd.Parameters.AddWithValue("@NgayLap", hd.NgayLap);
                cmd.Parameters.AddWithValue("@MaKH", hd.MaKH);
                cmd.Parameters.AddWithValue("@TongTien", hd.TongTien);

                return (int)cmd.ExecuteScalar();
            }
            catch
            {
                return -1;
            }
        }
        public bool ThemChiTietDonHang(ChiTietDonHangDTO ct)
        {
            try
            {
                string sql = @"INSERT INTO ChiTietDonHang(MaDonHang, MaSP, SoLuong, DonGia)
                       VALUES(@MaDH, @MaSP, @SoLuong, @DonGia)";

                SqlCommand cmd = new SqlCommand(sql);

                cmd.Parameters.AddWithValue("@MaDH", ct.MaDonHang);
                cmd.Parameters.AddWithValue("@MaSP", ct.MaSanPham);
                cmd.Parameters.AddWithValue("@SoLuong", ct.SoLuong);
                cmd.Parameters.AddWithValue("@DonGia", ct.DonGia);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
