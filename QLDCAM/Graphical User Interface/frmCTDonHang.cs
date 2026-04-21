using QLDCAM.Business_Logic_Layer;
using QLDCAM.Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLDCAM.Graphical_User_Interface
{
    public partial class frmCTDonHang : Form
    {
        int _maHD;
        DonHangBLL bll = new DonHangBLL();

        // Constructor nhận mã hóa đơn từ Form chính
        public frmCTDonHang(int maHD)
        {
            InitializeComponent();
            this._maHD = maHD;
        }

        private void frmChiTietHoaDon_Load(object sender, EventArgs e)
        {
            txtMaHD.Text = _maHD.ToString();
            LoadDuLieuChiTiet();
        }

        void LoadDuLieuChiTiet()
        {
            // Gọi BLL để lấy các món hàng của hóa đơn này
            DataTable dt = bll.LayChiTiet(_maHD);
            dtgHoaDon.DataSource = dt;

            // Tính tổng tiền hiển thị trên form chi tiết (nếu anh có ô Tổng tiền)
            decimal tong = 0;
            foreach (DataRow row in dt.Rows)
            {
                tong += Convert.ToDecimal(row["ThanhTien"]);
            }
            txtTong.Text = tong.ToString("N0") + " VNĐ";
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
