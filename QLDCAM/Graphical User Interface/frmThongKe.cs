using QLDCAM.Business_Logic_Layer;
using System;
using System.Data;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;

namespace QLDCAM.Graphical_User_Interface
{
    public partial class frmThongKe : Form
    {
        ThongKeBLL bll = new ThongKeBLL();
        private string loaiBaoCao;

        public frmThongKe(string loai)
        {
            InitializeComponent();
            this.loaiBaoCao = loai;
        }

        private void frmThongKe_Load(object sender, EventArgs e)
        {
            // Thiết lập ngày mặc định
            dtpFrom.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpTo.Value = DateTime.Now;
            // Đổi tiêu đề form theo loại báo cáo
            if (loaiBaoCao == "DoanhThu") this.Text = "THỐNG KÊ DOANH THU";
            else if (loaiBaoCao == "SanPhamBanChay") this.Text = "THỐNG KÊ SẢN PHẨM BÁN CHẠY";
            else if (loaiBaoCao == "HangTonKho") this.Text = "THỐNG KÊ HÀNG TỒN KHO";
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                // 1. Lấy dữ liệu từ BLL
                DataTable dt = bll.LayBaoCao(loaiBaoCao, dtpFrom.Value, dtpTo.Value);

                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu trong khoảng thời gian này!");
                    crystalReportViewer1.ReportSource = null;
                    return;
                }

                // 2. Xác định file Report (.rpt) cần mở
                ReportDocument rp = null;
                if (loaiBaoCao == "DoanhThu") rp = new rptDoanhThu();
                else if (loaiBaoCao == "SanPhamBanChay") rp = new rptSanPhamBanChay();
                else if (loaiBaoCao == "HangTonKho") rp = new rptHangTonKho();

                // 3. Đổ dữ liệu vào Report
                if (rp != null)
                {
                    rp.SetDataSource(dt);

                    // Gán tham số ngày tháng hiển thị trên báo cáo (nếu có)
                    try
                    {
                        rp.SetParameterValue("pTuNgay", dtpFrom.Value.ToString("dd/MM/yyyy"));
                        rp.SetParameterValue("pDenNgay", dtpTo.Value.ToString("dd/MM/yyyy"));
                    }
                    catch { /* Bỏ qua nếu report không dùng parameter */ }

                    crystalReportViewer1.ReportSource = rp;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị báo cáo: " + ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void btnHien_Click(object sender, EventArgs e)
        {

            // 1. Đặt ngày bắt đầu về một mốc xa trong quá khứ
            dtpFrom.Value = new DateTime(2000, 1, 1);

            // 2. Đặt ngày kết thúc là hiện tại
            dtpTo.Value = DateTime.Now;

            // 3. Gọi lại sự kiện Click của nút Lọc để nạp lại dữ liệu
            btnLoc_Click(sender, e);
        }
    }
}