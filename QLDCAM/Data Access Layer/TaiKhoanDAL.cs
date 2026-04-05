using QLDCAM.Data_Transfer_Object;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDCAM.Data_Access_Layer
{
    internal class TaiKhoanDAL : DBConnect
    {
        public bool KiemTraDangNhap(string user, string pass)
        {
            try
            {
                OpenConn(); 
                string sql = "SELECT COUNT(*) FROM TaiKhoan WHERE TenDangNhap = @user AND MatKhau = @pass";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@user", user);
                cmd.Parameters.AddWithValue("@pass", pass);

                int result = (int)cmd.ExecuteScalar();
                return result > 0; 
            }
            catch { return false; }
            finally { CloseConn(); } 
        }
        public bool ThemTaiKhoan(TaiKhoanDTO tk)
        {
            try
            {
                OpenConn();
                string sql = "INSERT INTO TaiKhoan(TenDangNhap, MatKhau, Email, Quyen) VALUES(@u, @p, @e, N'Nhân viên')";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@u", tk.TenDangNhap);
                cmd.Parameters.AddWithValue("@p", tk.MatKhau);
                cmd.Parameters.AddWithValue("@e", tk.Email);
                return cmd.ExecuteNonQuery() > 0;
            }
            catch
            {
                return false;
            }
            finally
            {
                CloseConn();
            }
        }
    }
}
