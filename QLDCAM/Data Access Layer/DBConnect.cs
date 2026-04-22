using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDCAM.Data_Access_Layer
{
    internal class DBConnect
    {
        protected SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=QLCHDungCuAmNhac;Integrated Security=True");

        public void OpenConn()
        {
            if (conn.State == ConnectionState.Closed) conn.Open();
        }

        public void CloseConn()
        {
            if (conn.State == ConnectionState.Open) conn.Close();
        }

        public DataTable LayBangDuLieu(string truyVan)
        {
            DataTable dt = new DataTable();
            try
            {
                if (conn.State == ConnectionState.Closed) conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(truyVan, conn);
                da.Fill(dt);
            }
            catch { /* Lỗi kệ nó, trả về bảng trống */ }
            finally { if (conn.State == ConnectionState.Open) conn.Close(); }
            return dt;
        }

        public bool ThucThiLenh(string truyVan)
        {
            try
            {
                if (conn.State == ConnectionState.Closed) conn.Open();
                SqlCommand cmd = new SqlCommand(truyVan, conn);
                int ketQua = cmd.ExecuteNonQuery();
                return ketQua > 0;
            }
            catch { return false; }
            finally { if (conn.State == ConnectionState.Open) conn.Close(); }
        }
    }
}
