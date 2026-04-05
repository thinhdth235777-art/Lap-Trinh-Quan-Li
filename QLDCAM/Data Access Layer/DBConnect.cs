using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDCAM.Data_Access_Layer
{
    internal class DBConnect
    {
        protected SqlConnection conn = new SqlConnection(@"Data Source=./SQLEXPRESS;Initial Catalog=QLCHDungCuAmNhac;Integrated Security=True");

        public void OpenConn()
        {
            if (conn.State == ConnectionState.Closed) conn.Open();
        }

        public void CloseConn()
        {
            if (conn.State == ConnectionState.Open) conn.Close();
        }
    }
}
