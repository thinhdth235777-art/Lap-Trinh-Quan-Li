using QLDCAM.Business_Logic_Layer;
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
    public partial class frmDashboard : Form
    {
        ThongKeBLL thongKeBLL = new ThongKeBLL();
        public frmDashboard()
        {
            InitializeComponent();
        }

        private void frmDashboard_Load(object sender, EventArgs e)
        {
            try
            {

                DateTime ngayDauThang = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                DateTime ngayHomNay = DateTime.Now;

                var dtDoanhThu = thongKeBLL.LayTongHopDoanhThu(ngayDauThang, ngayHomNay);
                if (dtDoanhThu.Rows.Count > 0)
                {
                    decimal doanhThu = Convert.ToDecimal(dtDoanhThu.Rows[0]["TongDoanhThu"]);
                    lblTongDoanhThu.Text = string.Format("{0:N0} VNĐ", doanhThu);
                    lblSoHoaDon.Text = dtDoanhThu.Rows[0]["SoHoaDon"].ToString();
                }

                // 2. Lấy tổng số sản phẩm trong kho
                // (Giả sử bạn đã viết hàm LayTongSoSanPham trong BLL)
                lblTongSanPham.Text = thongKeBLL.LayTongSoSanPham().ToString();

                // 3. Lấy số lượng hàng sắp hết (ví dụ dưới 5 món)
                var dtSapHet = thongKeBLL.LaySanPhamSapHet(5);
                lblSapHetHang.Text = dtSapHet.Rows.Count.ToString();

                dgvSapHetHang.DataSource = thongKeBLL.LaySanPhamSapHet(5);

                // Tùy chỉnh tiêu đề cột cho đẹp (Tên cột phải khớp với SQL của bạn)
                if (dgvSapHetHang.Columns.Count > 0)
                {
                    dgvSapHetHang.Columns["MaSanPham"].HeaderText = "Mã SP";
                    dgvSapHetHang.Columns["TenSanPham"].HeaderText = "Tên Sản Phẩm";
                    dgvSapHetHang.Columns["SoLuongTon"].HeaderText = "Tồn Kho";
                    // dgvSapHetHang.Columns["DonGia"].HeaderText = "Đơn Giá";
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show("Lỗi load dữ liệu: " + ex.Message);
            }
        }
    }
}
