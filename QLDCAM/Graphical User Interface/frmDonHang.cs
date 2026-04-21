using QLDCAM.Business_Logic_Layer;
using QLDCAM.Data_Access_Layer;
using QLDCAM.Data_Transfer_Object;
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
    public partial class frmDonHang : Form
    {
        DonHangBLL bll = new DonHangBLL();

        public frmDonHang()
        {
            InitializeComponent();
        }

        private void frmHoaDon_Load(object sender, EventArgs e)
        {
            LoadDSHoaDon();
        }

        void LoadDSHoaDon()
        {
            // Hiển thị danh sách hóa đơn lên GridView
            dtgHoadon.DataSource = bll.LayDSHoaDon();
        }

        // Sự kiện khi anh nhấn vào nút "Chi tiết" trên lưới
        private void dgvHoaDon_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem có phải nhấn vào cột nút Chi Tiết không
            if (dtgHoadon.Columns[e.ColumnIndex].Name == "colChiTiet" && e.RowIndex >= 0)
            {
                // Lấy Mã Hóa Đơn từ dòng hiện tại
                int maHD = Convert.ToInt32(dtgHoadon.Rows[e.RowIndex].Cells["MaDonHang"].Value);

                // Mở Form Chi Tiết và truyền mã vào
                frmCTDonHang fChiTiet = new frmCTDonHang();
                fChiTiet.ShowDialog();
            }
        }

        // Nút làm mới danh sách
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadDSHoaDon();
        }
    }
}
