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
        QLDCAM.Business_Logic_Layer.KhachHangBLL khBLL = new QLDCAM.Business_Logic_Layer.KhachHangBLL();

        // Constructor nhận mã hóa đơn từ Form chính
        public frmCTDonHang(int maHD)
        {
            InitializeComponent();
            this._maHD = maHD;
        }

        private void frmChiTietHoaDon_Load(object sender, EventArgs e)
        {
            txtMaHD.Text = _maHD.ToString();
            LoadCustomers();
            LoadDuLieuChiTiet();
        }

        void LoadCustomers()
        {
            DataTable dt = khBLL.LayDanhSachKH();
            cbKH.DataSource = dt;
            cbKH.DisplayMember = "HoTen";
            cbKH.ValueMember = "MaKhachHang";
            cbKH.SelectedIndexChanged -= cbKH_SelectedIndexChanged;
            cbKH.SelectedIndexChanged += cbKH_SelectedIndexChanged;
        }

        private void cbKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Khi chọn khách hàng, tính phí ship theo TinhThanh của khách
            string tinh = null;
            if (cbKH.SelectedItem is DataRowView drv && drv.Row.Table.Columns.Contains("TinhThanh"))
                tinh = drv["TinhThanh"].ToString();

            decimal phi = bll.TinhPhiShip(tinh);

            // Tổng tiền sản phẩm hiện có
            decimal tong = 0;
            if (dtgHoaDon.DataSource is DataTable dt)
            {
                foreach (DataRow row in dt.Rows)
                {
                    tong += Convert.ToDecimal(row["ThanhTien"]);
                }
            }

            if (txtTienSP != null) txtTienSP.Text = tong.ToString("N0") + " VNĐ";
            if (txtVanChuyen != null) txtVanChuyen.Text = phi.ToString("N0") + " VNĐ";
            txtTong.Text = (tong + phi).ToString("N0") + " VNĐ";
        }

        void LoadDuLieuChiTiet()
        {
            // Gọi BLL để lấy các món hàng của hóa đơn này
            DataTable dt = bll.LayChiTiet(_maHD);
            dtgHoaDon.DataSource = dt;

            // Ẩn cột MaSanPham nếu không muốn hiển thị mã sản phẩm trong lưới
            if (dtgHoaDon.Columns.Contains("MaSanPham"))
                dtgHoaDon.Columns["MaSanPham"].Visible = false;

            // Định dạng các cột số tiền
            if (dtgHoaDon.Columns.Contains("DonGia"))
                dtgHoaDon.Columns["DonGia"].DefaultCellStyle.Format = "N0";
            if (dtgHoaDon.Columns.Contains("ThanhTien"))
                dtgHoaDon.Columns["ThanhTien"].DefaultCellStyle.Format = "N0";

            // Tính tổng tiền sản phẩm
            decimal tong = 0;
            foreach (DataRow row in dt.Rows)
            {
                tong += Convert.ToDecimal(row["ThanhTien"]);
            }

            // Lấy phí vận chuyển đã lưu (nếu có). Theo yêu cầu: An Giang miễn phí, khác thì 30000
            decimal phi = bll.LayPhiVanChuyen(_maHD);

            // Hiển thị các ô tương ứng
            if (txtTienSP != null) txtTienSP.Text = tong.ToString("N0") + " VNĐ";
            if (txtVanChuyen != null) txtVanChuyen.Text = phi.ToString("N0") + " VNĐ";
            txtTong.Text = (tong + phi).ToString("N0") + " VNĐ";
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
