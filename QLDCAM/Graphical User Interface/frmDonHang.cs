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
        int maHD;
        public frmDonHang()
        {
            InitializeComponent();
        }
        void EnsureLinkColumn()
        {
            // Remove any existing link column named ChiTiet or colChiTiet
            if (dtgHoadon.Columns.Contains("ChiTiet")) dtgHoadon.Columns.Remove("ChiTiet");
            if (dtgHoadon.Columns.Contains("colChiTiet")) dtgHoadon.Columns.Remove("colChiTiet");

            DataGridViewLinkColumn link = new DataGridViewLinkColumn();
            link.Name = "ChiTiet";
            link.HeaderText = "Chi tiết";
            link.Text = "Xem";
            link.UseColumnTextForLinkValue = true;
            link.LinkColor = Color.Blue;
            link.ActiveLinkColor = Color.Red;
            dtgHoadon.Columns.Add(link);

            // Format TongTien column if exists
            if (dtgHoadon.Columns.Contains("TongTien"))
                dtgHoadon.Columns["TongTien"].DefaultCellStyle.Format = "N0";
        }
        private void frmDonHang_Load(object sender, EventArgs e)
        {
            LoadDSHoaDon();
            // After load, ensure link column exists
            EnsureLinkColumn();
        }

        void LoadDSHoaDon()
        {
            // Hiển thị danh sách hóa đơn lên GridView
            dtgHoadon.DataSource = bll.LayDSHoaDon();
        }

        // Sự kiện khi anh nhấn vào nút "Chi tiết" trên lưới
        private void dtgHoadon_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Check if clicked column is our link column
            if (dtgHoadon.Columns[e.ColumnIndex].Name == "ChiTiet")
            {
                int maHD = Convert.ToInt32(dtgHoadon.Rows[e.RowIndex].Cells["MaDonHang"].Value);
                using (frmCTDonHang fChiTiet = new frmCTDonHang(maHD))
                {
                    fChiTiet.ShowDialog();
                }

                // Refresh after closing detail
                LoadDSHoaDon();
            }
        }
        // 1. NÚT THÊM: Mở form bán hàng mới
        private void btnThem_Click(object sender, EventArgs e)
        {
            using (frmCTDonHang chiTiet = new frmCTDonHang(0))
            {
                chiTiet.ShowDialog();
            }

            LoadDSHoaDon();
        }

        // 2. NÚT XÓA: Hủy hóa đơn và hoàn kho
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dtgHoadon.CurrentRow != null)
            {
                int maHD = Convert.ToInt32(dtgHoadon.CurrentRow.Cells["MaDonHang"].Value);
                DialogResult dr = MessageBox.Show($"Bạn có chắc muốn HỦY hóa đơn {maHD}? Hàng sẽ được trả về kho!",
                                                 "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (dr == DialogResult.Yes)
                {
                    if (bll.HuyDonHang(maHD))
                    {
                        MessageBox.Show("Đã hủy hóa đơn thành công!");
                        LoadDSHoaDon();
                    }
                    else MessageBox.Show("Lỗi khi hủy hóa đơn!");
                }
            }
        }
    }
}
