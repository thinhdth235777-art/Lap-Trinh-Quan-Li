using ClosedXML.Excel;
using CrystalDecisions.CrystalReports.Engine;
using QLDCAM.Business_Logic_Layer;
using System;
using System.Data;
using System.Windows.Forms;
using ClosedXML.Excel;
using System.IO;

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

        private void btnXuat_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                // 1. Lấy dữ liệu từ BLL
                DataTable dt = bll.LayBaoCao(loaiBaoCao, dtpFrom.Value, dtpTo.Value);

                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo");
                    return;
                }

                // 2. Xác định tên Sheet dựa trên loại báo cáo đang xem
                string sheetName = "";
                if (loaiBaoCao == "DoanhThu") sheetName = "DoanhThuTheoThang";
                else if (loaiBaoCao == "SanPhamBanChay") sheetName = "SanPhambanchay";
                else if (loaiBaoCao == "HangTonKho") sheetName = "Hangtonkho";

                // 3. Thiết lập hộp thoại lưu file (Mặc định một tên file chung)
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "Excel Workbook|*.xlsx";
                    sfd.FileName = "BaoCao_TongHop_NhacCu.xlsx"; // Tên file dùng chung cho cả 3 loại

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        XLWorkbook wb;

                        // 4. Kiểm tra: Nếu file đã tồn tại thì MỞ RA, nếu chưa thì TẠO MỚI
                        if (File.Exists(sfd.FileName))
                        {
                            try
                            {
                                wb = new XLWorkbook(sfd.FileName);
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("File đang mở bởi chương trình khác. Vui lòng đóng file trước khi xuất!", "Lỗi");
                                return;
                            }
                        }
                        else
                        {
                            wb = new XLWorkbook();
                        }

                        // 5. Xử lý Sheet: Nếu Sheet đã tồn tại rồi thì xóa đi để ghi đè dữ liệu mới nhất
                        if (wb.Worksheets.Contains(sheetName))
                        {
                            wb.Worksheet(sheetName).Delete();
                        }

                        // 6. Thêm DataTable vào Sheet với tên tương ứng
                        var ws = wb.Worksheets.Add(dt, sheetName);

                        // 7. Trang trí tiêu đề Sheet cho chuyên nghiệp
                        ws.Columns().AdjustToContents(); // Tự giãn độ rộng cột
                        var header = ws.Row(1);
                        header.Style.Font.Bold = true;
                        header.Style.Fill.BackgroundColor = XLColor.FromHtml("#1D6F42"); // Màu xanh lá Excel
                        header.Style.Font.FontColor = XLColor.White;

                        // 8. Lưu lại file (Lưu đè hoặc Lưu mới đều dùng SaveAs)
                        wb.SaveAs(sfd.FileName);

                        MessageBox.Show($"Dữ liệu đã được xuất vào Sheet '{sheetName}' trong file {Path.GetFileName(sfd.FileName)}",
                                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất Excel: " + ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
}