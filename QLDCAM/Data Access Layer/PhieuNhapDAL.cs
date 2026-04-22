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
    internal class PhieuNhapDAL : DBConnect
    {
        public bool LuuPhieuFull(PhieuNhapDTO pn, DataTable dtChiTiet)
        {
            try
            {
                OpenConn();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. Lưu PhieuNhap và lấy ID
                        string sqlPN = "INSERT INTO PhieuNhap (MaNhanVien, MaNhaCungCap, NgayNhap, TongTien) " +
                                       "OUTPUT INSERTED.MaPhieuNhap VALUES (@manv, @mancc, @ngay, @tong)";
                        SqlCommand cmdPN = new SqlCommand(sqlPN, conn, trans);
                        cmdPN.Parameters.AddWithValue("@manv", pn.MaNhanVien);
                        cmdPN.Parameters.AddWithValue("@mancc", pn.MaNhaCungCap);
                        cmdPN.Parameters.AddWithValue("@ngay", pn.NgayNhap);
                        cmdPN.Parameters.AddWithValue("@tong", pn.TongTien);
                        int maPN = (int)cmdPN.ExecuteScalar();

                        // 2. Duyệt DataTable để lưu Chi tiết và CỘNG KHO
                        foreach (DataRow row in dtChiTiet.Rows)
                        {
                            // Lưu Chi tiết
                            string sqlCT = "INSERT INTO ChiTietPhieuNhap (MaPhieuNhap, MaSanPham, SoLuong, DonGiaNhap) " +
                                           "VALUES (@mapn, @masp, @sl, @gia)";
                            SqlCommand cmdCT = new SqlCommand(sqlCT, conn, trans);
                            cmdCT.Parameters.AddWithValue("@mapn", maPN);
                            cmdCT.Parameters.AddWithValue("@masp", row["MaSP"]);
                            cmdCT.Parameters.AddWithValue("@sl", row["SoLuong"]);
                            cmdCT.Parameters.AddWithValue("@gia", row["DonGiaNhap"]);
                            cmdCT.ExecuteNonQuery();

                            // Cộng tồn kho (Quan trọng nhất để không bị âm)
                            string sqlUpdate = "UPDATE SanPham SET SoLuongTon = SoLuongTon + @sl WHERE MaSanPham = @masp";
                            SqlCommand cmdUp = new SqlCommand(sqlUpdate, conn, trans);
                            cmdUp.Parameters.AddWithValue("@sl", row["SoLuong"]);
                            cmdUp.Parameters.AddWithValue("@masp", row["MaSP"]);
                            cmdUp.ExecuteNonQuery();
                        }

                        trans.Commit();
                        return true;
                    }
                    catch { trans.Rollback(); return false; }
                }
            }
            finally { CloseConn(); }
        }
    }
}
